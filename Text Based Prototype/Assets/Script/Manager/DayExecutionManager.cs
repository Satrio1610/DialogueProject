using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayExecutionManager : MonoBehaviour {


	[SerializeField]
	private InGameUIManager uiManager;

	[SerializeField]
	private You player; 

	private Day currentDay;
	private int currentDayID; 

	private Node currentNode; 

	[Header("Testing Purpose")]
	[SerializeField]
	private bool isTesting;
	[SerializeField]
	private string testCharacterName; 
	[SerializeField]
	private GameRouteScript testingScript;

	private bool hasSubscribeToUiDisplayEvent = false;
	// Use this for initialization
	void Start () {
		if (isTesting) {
			initializeTestingEnvironment ();
			/*
			this.currentDay = this.testingScript.DaysList[0];
			Debug.Log (this.currentDay.getConversationNodes ().Count);
			Day aDay = this.currentDay;
			var a = new List<int>(aDay.getConversationNodes ().Keys);

			for (int i = 0; i < a.Count; i++) {
				Debug.Log (a[i]);
			}
			this.player.yourProgression.setCurrentDay (this.currentDayID);
			*/
		} else {
		
		}
		// set the ui

		// subscribe to finish currentDialogue Event 
		if(hasSubscribeToUiDisplayEvent == false){
			this.uiManager.subscribeToChoiceButtonsManager_EffectEvent(this.resolveChoiceEffect);
			this.uiManager.subscribeToTextManagers_FinishedDialogueEvent (this.loadNextNode);	

			this.hasSubscribeToUiDisplayEvent = true; 
		}

		this.uiManager.setActiveUIComponent( player.yourCharacter);

		loadNextNode ();
	}

	public enum EXECUTOR_STATE {
		IDLE, 
		NODE_TRANSITION,
        DAY_TRANSITION
	}

	private EXECUTOR_STATE currentState; 
	private DialoguePreAction currentPreAction; 
	private int preActionIndex;
	private bool hasFinishedCurrentPreAction;
	void Update() {
		switch (currentState) {
		case EXECUTOR_STATE.IDLE:
			break;
            case EXECUTOR_STATE.DAY_TRANSITION:
                break;
		case EXECUTOR_STATE.NODE_TRANSITION:
			if (preActionIndex < currentPreAction.obtainedListOfPreAction ().Count) {



				if (hasFinishedCurrentPreAction) {
					KeyValuePair<DialoguePreAction.ACTION, int> nextAction = currentPreAction.obtainedListOfPreAction () [preActionIndex++];

					DialoguePreAction.ACTION actionType = nextAction.Key; 
					int pointer = nextAction.Value;

					switch (actionType) {
					case DialoguePreAction.ACTION.CHANGE_BACKGROUND:
						BackgroundManager.BACKGROUND_IMAGE bImage = (BackgroundManager.BACKGROUND_IMAGE)pointer;
						this.uiManager.setBackgroundImage (bImage);
						break;
					case DialoguePreAction.ACTION.SHOW_SPRITE:
						break;
					case DialoguePreAction.ACTION.HIDE_SPRITE:
						break;
					case DialoguePreAction.ACTION.FADE_IN:
						Debug.Log ("fading in");
						this.uiManager.startFadeIn ();
						this.hasFinishedCurrentPreAction = false;
						break;
					case DialoguePreAction.ACTION.FADE_OUT:
						this.uiManager.startFadeOut ();
						this.hasFinishedCurrentPreAction = false;
						break;
					case DialoguePreAction.ACTION.PLAY_BGM:
						SoundManagerScript.BGM_TYPE bgmType = (SoundManagerScript.BGM_TYPE)pointer;
						SoundManagerScript.instance.playBGM (bgmType, true);
						break;
					case DialoguePreAction.ACTION.PLAY_SFX:
						Debug.Log ("playing sfx");
						SoundManagerScript.SFX_TYPE sfxType = (SoundManagerScript.SFX_TYPE)pointer; 
						Debug.Log (sfxType);
						SoundManagerScript.instance.playSFX (sfxType);
						break;
					case DialoguePreAction.ACTION.SHOW_DIALOGUE_BAR:
						Debug.Log ("showing dialogue bar");
						this.uiManager.setDialogueBoxAnimation (DialogueAnimationController.BAR_MOVEMENT.BRING_UP);
						break;
					case DialoguePreAction.ACTION.SHOW_DIALOGUE_BAR_INSTANT:
						this.uiManager.setDialogueBoxAnimation (DialogueAnimationController.BAR_MOVEMENT.UP_INSTANT);
						break; 
					case DialoguePreAction.ACTION.HIDE_DIALOGUE_BAR:
						this.uiManager.setDialogueBoxAnimation (DialogueAnimationController.BAR_MOVEMENT.BRING_DOWN);
						break;
					case DialoguePreAction.ACTION.HIDE_DIALOGUE_BAR_INSTANT:
						this.uiManager.setDialogueBoxAnimation (DialogueAnimationController.BAR_MOVEMENT.DOWN_INSTANT);
						break;
					}
				}
				Debug.Log ("yellow");
				hasFinishedCurrentPreAction = this.uiManager.hasFinishedFading ();
			}

			if (this.uiManager.hasFinishedDialogueAnimation () && preActionIndex >= currentPreAction.obtainedListOfPreAction ().Count) {
				currentState = EXECUTOR_STATE.IDLE;
				this.uiManager.updateDisplayWithCurrentNode (this.currentNode);
			}
			break;
		}
	}
		
	void loadNextNode(){
		if (currentNode == null) {
			Debug.Log (currentDay.getStartingNode ());

			currentNode = currentDay.getNextNode (currentDay.getStartingNode ());
			this.player.yourProgression.setCurrentNode (this.currentDayID,currentDay.getStartingNode());
		} else {
			// on finish traversing, log player history 
			int a = -1;

			if (currentNode.getNodeType () == Node.NODE_TYPE.BRANCH) {
				Branch currentBranch = (Branch)currentNode;
				a = currentBranch.getNextNode (player);
			} else if(currentNode.getNodeType() == Node.NODE_TYPE.DIALOGUE){
				Dialogue currentDialogue = (Dialogue)currentNode;
				a = currentDialogue.getNextNode ();
			}

			this.player.yourProgression.setCurrentNode (this.currentDayID,a);

			currentNode = currentDay.getNextNode (a);
			Debug.Log ("next node: " + a);

		}

		displayNode ();
	}

	void resolveChoiceEffect(ChoiceEffect selectedChoiceEffect){
		this.player.yourProgression.addAffectionLevel (selectedChoiceEffect.getAffectionPointEarned ());
		currentNode = (Node)currentDay.getNextNode (selectedChoiceEffect.getDestinationNodeID());
		displayNode ();
	}

	void displayNode() {
		if (currentNode.getNodeType() == Node.NODE_TYPE.BRANCH) {
			loadNextNode ();
		}

		if (currentNode.getNodeType() == Node.NODE_TYPE.DIALOGUE) {
			Dialogue currentDialogue = (Dialogue)currentNode; 
			Debug.Log ("number of detected preaction: " + currentDialogue.getPreAction ().obtainedListOfPreAction ().Count);
			if (!currentDialogue.getPreAction ().isEmpty ()) {
				//switchToTransition 
				this.currentState = EXECUTOR_STATE.NODE_TRANSITION;
				Debug.Log ("initiating preaction");
				this.currentPreAction = currentDialogue.getPreAction (); 
				this.preActionIndex = 0;
				hasFinishedCurrentPreAction = true;
				return; 
			}
		}
		this.uiManager.updateDisplayWithCurrentNode (this.currentNode);
	}

	public void setUpDay() {
		
	}

	public void callNextText() {
		this.uiManager.showNextText ();
	}

	#region testing
	void initializeTestingEnvironment() {
		
		Character testCharacter = new Character ();
		testCharacter.setName (this.testCharacterName);

		Character narratorCharacter = new Character ();
		narratorCharacter.setName (string.Empty);

		Day testDay = new Day (); 
		testDay.setStartingNode (10);

		DialogueDictionary testDialogue = new DialogueDictionary(); 
		ChoiceDictionary testChoice = new ChoiceDictionary (); 
		BranchDictionary testBranch = new BranchDictionary ();

		//// list of dialogues nodes////
		DialoguePreAction preAction0 = new DialoguePreAction();
		preAction0.addNewPreAction (new KeyValuePair<DialoguePreAction.ACTION, int> (DialoguePreAction.ACTION.FADE_IN, -1));
		preAction0.addNewPreAction( new KeyValuePair<DialoguePreAction.ACTION,int> (DialoguePreAction.ACTION.SHOW_DIALOGUE_BAR,0));
		preAction0.addNewPreAction(new KeyValuePair<DialoguePreAction.ACTION,int> (DialoguePreAction.ACTION.PLAY_SFX,(int)SoundManagerScript.SFX_TYPE.PLANE_SFX));
		preAction0.addNewPreAction(new KeyValuePair<DialoguePreAction.ACTION, int>(DialoguePreAction.ACTION.PLAY_BGM,(int)SoundManagerScript.BGM_TYPE.BGM_WHATEVER));
			
		List<string> dialogue0List = new List<string> {
			"> Airport, Food Court.",
			"> You've finished checking in and have your boarding pass with you.",
			"> You are now waiting for Graham to come."
		};

		Dialogue dialogue0 = new Dialogue ();
		dialogue0.setListOfDialogues (dialogue0List);
		dialogue0.setPreAction (preAction0);
		dialogue0.setCharacter (narratorCharacter);

		dialogue0.setNextNodeID (0);

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

		dialogue1.setNextNodeID (1);

		List<string> dialogue2List = new List<string> {
			"uuu... I am nervous...",
			"I hope this time around it will be better than our last date...",
			"Does he even actually consider it a date? Or is he just being friendly and offer to help me find the figurine that I want..",
			"Am I getting ahead of myself? is it just me that think he is interested in me?"
		};

		Dialogue dialogue2 = new Dialogue (); 
		dialogue2.setListOfDialogues (dialogue2List);
		 
		dialogue2.setCharacter (testCharacter);
		dialogue2.setNextNodeID (2);

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

		dialogue3.setCharacter (testCharacter);
		dialogue3.setNextNodeID (5);

		Choice choice1 = new Choice(); 

		ChoiceEffect c10 = new ChoiceEffect (3, 0);
		ChoiceEffect c20 = new ChoiceEffect (4, 0);
		KeyValuePair<string,ChoiceEffect> c1 = new KeyValuePair<string, ChoiceEffect> ("Don't answer immediately", c10);
		KeyValuePair<string,ChoiceEffect> c2 = new KeyValuePair<string,ChoiceEffect> ("Tell him where you currently are", c20);

		List<KeyValuePair<string,ChoiceEffect>> listOfChoices1 = new List<KeyValuePair<string,ChoiceEffect>> { c1, c2 };

		choice1.populateChoices (listOfChoices1);


		List<string> dialogue4List = new List<string> {
			"Calm down... Calm Down...",
			"Now, where were we?"
		};
			
		Dialogue dialogue4 = new Dialogue ();
		dialogue4.setListOfDialogues (dialogue4List);

		ConditionalNextNode nextNode4 = new ConditionalNextNode (); 


		dialogue4.setCharacter (testCharacter);
		dialogue4.setNextNodeID (5);

		List<string> dialogue5List = new List<string> {
			"Hummm... I hope I look okay...",
			"But... He does like to wear fancy stuff.",
			"No matter what I do, I'll probably look like a plebian next to him."
		};

		Dialogue dialogue5 = new Dialogue ();
		dialogue5.setListOfDialogues (dialogue5List);


		dialogue5.setCharacter (testCharacter);
		dialogue5.setNextNodeID (-1);

		testDialogue.Add (10,dialogue0);
		testDialogue.Add (0, dialogue1);
		testDialogue.Add (1, dialogue2);
		testDialogue.Add (2, dialogue3);
		testDialogue.Add (3, dialogue4);
		testDialogue.Add (4, dialogue5);

		testChoice.Add (5, choice1);
		testDay.populateConversationnodes (testDialogue);
		testDay.populateBranchNodes (testBranch);
		testDay.populateChoiceNodes (testChoice);

		this.currentDay = testDay;
		this.currentDayID = 0; 

		this.player.yourProgression.setCurrentDay (this.currentDayID);

	}

	#endregion
}
