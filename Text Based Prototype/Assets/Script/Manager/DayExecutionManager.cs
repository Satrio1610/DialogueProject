using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayExecutionManager : MonoBehaviour {


	[SerializeField]
	private InGameUIManager uiManager;

	[SerializeField]
	private You player; 

	private Day currentDay;
	private Node currentNode; 

	[Header("Testing Purpose")]
	[SerializeField]
	private bool isTesting;
	[SerializeField]
	private string testCharacterName; 

	private bool hasSubscribeToUiDisplayEvent = false;
	// Use this for initialization
	void Start () {
		if (isTesting) {
			initializeTestingEnvironment ();
		} else {
		
		}
		// set the ui

		// subscribe to finish currentDialogue Event 
		if(hasSubscribeToUiDisplayEvent == false){
			this.uiManager.subscribeToChoiceButtonsManager_IntEvent(this.loadNextNodeFromChoice);
			this.uiManager.subscribeToTextManagers_FinishedDialogueEvent (this.loadNextNode);	

			this.hasSubscribeToUiDisplayEvent = true; 
		}

		this.uiManager.setActiveUIComponent( player.yourCharacter);

		loadNextNode ();
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			this.uiManager.showNextText ();
		}
	}	
		
	void loadNextNode(){
		if (currentNode == null) {
			currentDay.getConversationNodes ().TryGetValue (currentDay.getStartingNode (), out currentNode);
		} else {
			// on finish traversing, log player history 
			int a = currentNode.getNextNode (player);	
			currentDay.getConversationNodes ().TryGetValue (a, out currentNode);
			Debug.Log ("next node: " + a);
		}

		displayNode ();
	}

	void loadNextNodeFromChoice(int nodeId){
		this.currentDay.getConversationNodes ().TryGetValue (nodeId, out this.currentNode);
		displayNode ();
	}

	void displayNode() {
		this.uiManager.updateDisplayWithCurrentNode (this.currentNode);
	}

	public void setUpDay() {
		
	}

	#region testing
	void initializeTestingEnvironment() {
		Character testCharacter = new Character ();
		testCharacter.setName (this.testCharacterName);

		Day testDay = new Day (); 
		testDay.setStartingNode (0);

		Dictionary<int,Node> testNodes = new Dictionary<int, Node> (); 

		List<string> dialogue1List = new List<string> {
			"Hmm... I still can't believe that he is really going to come see me off...",
			"I mean, it's not a bad thing.",

		};

		Dialogue dialogue1 = new Dialogue ();
		dialogue1.setListOfDialogues (dialogue1List);

		ConditionalNextNode nextNode1 = new ConditionalNextNode (); 
		nextNode1.setConditional (false);
		nextNode1.setDefaultNextNode (1);

		dialogue1.setCharacter (testCharacter);

		dialogue1.setNextNode (nextNode1);

		List<string> dialogue2List = new List<string> {
			"uuu... I am nervous...",
			"I hope this time around it will be better than our last date...",
			"Does he even actually consider it a date? Or is he just being friendly and offer to help me find the figurine that I want..",
			"Am I getting ahead of myself? is it just me that think he is interested in me?"
		};

		Dialogue dialogue2 = new Dialogue (); 
		dialogue2.setListOfDialogues (dialogue2List);

		ConditionalNextNode nextNode2 = new ConditionalNextNode (); 
		nextNode2.setConditional (false);
		nextNode2.setDefaultNextNode (2);

		dialogue2.setCharacter (testCharacter);
		dialogue2.setNextNode (nextNode2);

		List<string> dialogue3List = new List<string> {
			"What if he think I am weird after that outing.", 
			"He did say he enjoy our time together though...",
			"But, what if he was just being kind... What if he actually think otherwise!?",
			"Shio... Why did you even decide to get a kid's seat for lunch!",
			"Stupid! STUPID!",
			"...",
			"Huh?",
			"Oh no! He's already here!?",
			"What should I tell him..."
		};

		Dialogue dialogue3 = new Dialogue ();
		dialogue3.setListOfDialogues (dialogue3List);

		ConditionalNextNode nextNode3 = new ConditionalNextNode (); 
		nextNode3.setConditional (false);

		ProgressionStats progStatsFor3 = new ProgressionStats (2, 0, 0);
		nextNode3.populateDestinationList (new KeyValuePair<ProgressionStats, int> (progStatsFor3, 3));
		nextNode3.setDefaultNextNode (5);

		dialogue3.setCharacter (testCharacter);
		dialogue3.setNextNode (nextNode3);

		Choice choice1 = new Choice(); 

		KeyValuePair<string,int> c1 = new KeyValuePair<string, int> ("Don't answer immediately", 3);
		KeyValuePair<string,int> c2 = new KeyValuePair<string,int> ("Tell him where you currently are", 4);

		List<KeyValuePair<string,int>> listOfChoices1 = new List<KeyValuePair<string,int>> { c1, c2 };

		choice1.populateChoices (listOfChoices1);


		List<string> dialogue4List = new List<string> {
			"Calm down... Calm Down...",
			"Now, where were we?"
		};

		Dialogue dialogue4 = new Dialogue ();
		dialogue4.setListOfDialogues (dialogue4List);

		ConditionalNextNode nextNode4 = new ConditionalNextNode (); 
		nextNode4.setConditional (false);

		nextNode4.setDefaultNextNode (5);

		dialogue4.setCharacter (testCharacter);
		dialogue4.setNextNode (nextNode4);

		List<string> dialogue5List = new List<string> {
			"Hummm... I hope I look okay...",
			"But... He does like to wear fancy stuff.",
			"No matter what I do, I'll probably look like a plebian next to him."
		};

		Dialogue dialogue5 = new Dialogue ();
		dialogue5.setListOfDialogues (dialogue5List);

		ConditionalNextNode nextNode5 = new ConditionalNextNode (); 
		nextNode5.setConditional (true);

		ProgressionStats progStats5 = new ProgressionStats (1, 0, 0);


		nextNode5.setDefaultNextNode (-1);

		dialogue5.setCharacter (testCharacter);
		dialogue5.setNextNode (nextNode5);


		testNodes.Add (0, (Node)dialogue1);
		testNodes.Add (1, (Node)dialogue2);
		testNodes.Add (2, (Node)dialogue3);
		testNodes.Add (5, (Node)choice1);
		testNodes.Add (3, (Node)dialogue4);
		testNodes.Add (4, (Node)dialogue5);
		testDay.populateConversationnodes (testNodes);
		this.currentDay = testDay;
	}

	#endregion
}
