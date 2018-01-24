using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

public class TextParser : MonoBehaviour
{
	public enum ROUTE
	{
		SHIO,
		GRAHAM
	}

	public ROUTE routeToBeParsed;
	public string TargetDirectory;
	public string ShioScriptFileName;
	public string GrahamScriptFileName;

	[SerializeField]
	TextAsset dialogueText;
	[SerializeField]
	Character Shio;
	[SerializeField]
	Character Graham;
	[SerializeField]
	Character Narrator;

	string content;
	string[] contentDivided;

	[Header ("Parts Indicator")]
	public string newDay_String;
	public string setBackground_String;
	public string playSFX_Regex;
	private Regex sfxRegex;

	public string characterSpeaking_String;
	public string grahamSpeaking_String;
	public string shioSpeaking_String;

	public string showingSprite_String;
	public string noSprite_String;

	public string showTutorialBox_String;


	private GameRouteScript newScript;

	private Dictionary<string,Character> characterArchive;

	private bool isFirstNodeCreated;
	// Use this for initialization
	void Awake ()
	{
		sfxRegex = new Regex (playSFX_Regex);
		characterArchive = new Dictionary<string,Character> ();

		isFirstNodeCreated = false;
		readFile ();
	}

	void Start ()
	{
		this.nodesDictionary = new NodeDictionary ();
		AssetDatabase.SaveAssets ();
	}

	void Update ()
	{
		parseTextToScriptObject ();
		i++;
	}


	void readFile ()
	{
		content = dialogueText.text; 

		newScript = createNewScriptAsset ();
		contentDivided = content.Split (new char[] { '\n' });
	}

	public enum TEXT_CONTENT
	{
		DAY,
		BACKGROUND,
		SFX,
		SPRITE_HIDE,
		SPRITE_SHOW,
		SPEAKING,
		SPEAKING_NO_NAME,
		TUTORIAL_BOX
	}

	 
	// Day component
	private Day currentDay;
	private NodeDictionary nodesDictionary;
	private DialogueDictionary conversationDictionary;
	private ChoiceDictionary choiceDictionary;
	private int dayIndex = 0;

	// node component
	private int nodeIndex = 0;
	private Node currentNode;
	private Choice currentChoice;
	private Dialogue currentDialogue;
	private List<string> stringTextDialogues;
	private Character lastUsedCharacter;
	private int i = 0;

	void parseTextToScriptObject ()
	{

		if (i >= contentDivided.Length) {
			return;
		}



		string currentString = this.contentDivided [i];
		// get type of the text 
		TEXT_CONTENT currentContentType = getContentType (currentString);
		Debug.Log ("current type is: " + currentContentType.ToString () + "for index: " + i);
		switch (currentContentType) {
		case TEXT_CONTENT.DAY:
				// create new day 
				// if there is previous day, store it then create new day 
				// try to parse date 
				// store data of the new day
				// go to next string
			if (currentDay != null) {
				this.currentDay.populateConversationnodes (conversationDictionary.cloneDictionary ());
				this.currentDay.populateChoiceNodes (choiceDictionary.cloneDictionary ());
				this.currentDay.setNextDay (this.dayIndex + 1);
				this.newScript.DaysList.Add (currentDay);
			}

			this.currentDay = new Day (); 
			this.currentDay.setStartingNode (0);

			this.conversationDictionary = new DialogueDictionary (); 
			this.choiceDictionary = new ChoiceDictionary (); 

			string date = currentString.Split (':') [1].Trim ();

			if (date != "NA") {
				Debug.Log (date);
				string[] dateArray = date.Split (' ');
				Debug.Log (dateArray.Length);
				this.currentDay.setDate (int.Parse (dateArray [0]), int.Parse (dateArray [1]), int.Parse (dateArray [2]));

			}
			break; 
		case TEXT_CONTENT.BACKGROUND:
				//if no node, create new dialogue, keep 
				//if there is node, store node, create new dialogue, keep
			if (isFirstNodeCreated) {
				Debug.Log ("current content index: " + i);
				addNodeToDictionary ();
			}

			reinitializeDialogue ();

			break; 
		case TEXT_CONTENT.SFX:
				// if no node, create new dialogue, keep 
				// if there is node, store node, create new dialogue, keep
			if (isFirstNodeCreated) {
				addNodeToDictionary ();
			}

			reinitializeDialogue ();

			Match matchingResult = this.sfxRegex.Match (currentString);
				//obtain sfx -- Not supported yet
			string sfxName = matchingResult.Groups [1].ToString ();
			Debug.Log ("sound effect name is : " + sfxName);
				//sfx might have additionalDialogue 
			string additionalDialogue = matchingResult.Groups [2].ToString ();
			additionalDialogue = additionalDialogue.TrimStart ();

			if (additionalDialogue.Length > 0) {
				Debug.Log ("adding dialogue to list of string");
				this.currentDialogue.setCharacter (Narrator);
				this.stringTextDialogues.Add (additionalDialogue);
			} else {
				Debug.Log ("not adding");
			}

			break;
		case TEXT_CONTENT.SPRITE_HIDE:
			break;
		case TEXT_CONTENT.SPRITE_SHOW:
			break;
		case TEXT_CONTENT.SPEAKING:
				
			string[] nameAndDialogue = currentString.Split (':');
			string name = nameAndDialogue [0];
			string dialogue = nameAndDialogue [1];

			Character currentlySpeakingCharacter; 

			if (name == shioSpeaking_String) {
				currentlySpeakingCharacter = Shio;
			} else if (name == grahamSpeaking_String) {
				currentlySpeakingCharacter = Graham;
			} else {
				if (!characterArchive.TryGetValue (name, out currentlySpeakingCharacter)) {
					currentlySpeakingCharacter = new Character ();
					currentlySpeakingCharacter.setName (name);
					this.characterArchive.Add (name, currentlySpeakingCharacter);
				}
			}

			if (lastUsedCharacter != null && !lastUsedCharacter.Equals (currentlySpeakingCharacter)) {
				addNodeToDictionary ();
				reinitializeDialogue ();
			}

			if (!isFirstNodeCreated) {
				reinitializeDialogue ();
			}

			this.stringTextDialogues.Add (dialogue);
			this.currentDialogue.setCharacter (currentlySpeakingCharacter);
			this.lastUsedCharacter = currentlySpeakingCharacter;
			break; 
		case TEXT_CONTENT.SPEAKING_NO_NAME:
			if (lastUsedCharacter != null && !lastUsedCharacter.Equals (Narrator)) {
				addNodeToDictionary ();
				reinitializeDialogue ();
			}

			if (!isFirstNodeCreated) {
				reinitializeDialogue ();
			}

			this.stringTextDialogues.Add (currentString);
			lastUsedCharacter = Narrator;
			this.currentDialogue.setCharacter (Narrator);
			break;
		case TEXT_CONTENT.TUTORIAL_BOX:
			break;
		}


	}

	void addNodeToDictionary ()
	{
		if (isFirstNodeCreated) {
			if (currentNode.getNodeType () == Node.NODE_TYPE.DIALOGUE) {
				currentDialogue.setListOfDialogues (new List<string> (stringTextDialogues));
				this.conversationDictionary.Add (nodeIndex++, currentDialogue);
			} else if (currentNode.getNodeType () == Node.NODE_TYPE.CHOICE) {
				this.choiceDictionary.Add (nodeIndex++, currentChoice);
			}
		}
	}

	void OnDisable ()
	{
		AssetDatabase.SaveAssets ();
	}

	void reinitializeDialogue ()
	{
		currentDialogue = new Dialogue (); 
		currentNode = currentDialogue;
		stringTextDialogues = new List<string> ();

		isFirstNodeCreated = true; 
	}

	TEXT_CONTENT getContentType (string submittedText)
	{
		if (submittedText.Contains (newDay_String)) {
			return TEXT_CONTENT.DAY;
		} else if (submittedText.Contains (setBackground_String)) {
			return TEXT_CONTENT.BACKGROUND;
		} else if (sfxRegex.IsMatch (submittedText)) {
			return TEXT_CONTENT.SFX;
		} else if (submittedText.Contains (showingSprite_String)) {
			return TEXT_CONTENT.SPRITE_SHOW;
		} else if (submittedText.Contains (noSprite_String)) {
			return TEXT_CONTENT.SPRITE_HIDE;
		} else if (submittedText.Contains (characterSpeaking_String)) {
			return TEXT_CONTENT.SPEAKING;	
		} else if (submittedText.Contains (showTutorialBox_String)) {
			return TEXT_CONTENT.TUTORIAL_BOX;
		} else {
			return TEXT_CONTENT.SPEAKING_NO_NAME;
		}
	}

	private GameRouteScript createNewScriptAsset ()
	{
		string finalFileName = setFileName ();

		GameRouteScript newScriptAsset = ScriptableObject.CreateInstance<GameRouteScript> ();
		newScriptAsset.DaysList = new List<Day> ();
		AssetDatabase.CreateAsset (newScriptAsset, finalFileName);
		AssetDatabase.SaveAssets ();

		return newScriptAsset;
	}

	private string setFileName ()
	{
		if (routeToBeParsed == ROUTE.SHIO) {
			return this.TargetDirectory + this.ShioScriptFileName;
		} else {
			return this.TargetDirectory + this.GrahamScriptFileName;
		}
	}
}
