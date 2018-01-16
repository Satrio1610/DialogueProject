﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayExecutionManager : MonoBehaviour {

	[SerializeField]
	private TextDisplayManager textDisplayManager;
	[SerializeField]
	private ChoiceButtonManager choiceButtonManager; 

	[SerializeField]
	private You player; 

	private Day currentDay;
	private Node currentNode; 
	[Header("Testing Purpose")]
	[SerializeField]
	private bool isTesting;

	// Use this for initialization
	void Start () {
		if (isTesting) {
			initializeTestingEnvironment ();
		} else {
		
		}

		// subscribe to finish currentDialogue Event 
		this.textDisplayManager.subscribeToFinishedDialogueEvent(loadNextNode);
		this.choiceButtonManager.subscribeToIntEventButtons (loadNextNodeFromChoice);

		//start
		this.choiceButtonManager.disableAllButtons();
		loadNextNode ();
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			textDisplayManager.showNextCharacter ();
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
		Node.NODE_TYPE currentNodeType = currentNode.getNodeType ();

		if (currentNodeType == Node.NODE_TYPE.CHOICE) {
			Choice newOptions = (Choice)currentNode;
			this.choiceButtonManager.showChoices (newOptions.getChoices ());
		} else {
			Dialogue newDialogue = (Dialogue)currentNode;
			textDisplayManager.supplyNewDialogue (newDialogue.getListOfDialogues (), "test name");
		}
	}

	public void setUpDay() {
		
	}

	#region testing
	void initializeTestingEnvironment() {
		Day testDay = new Day (); 
		testDay.setStartingNode (0);

		Dictionary<int,Node> testNodes = new Dictionary<int, Node> (); 

		List<string> dialogue1List = new List<string> {
			"Hi.",
			"How Are You",
			"It's so nice to see you again",
			"So what do you want to do today?",
			"Me? I'm fine with anything."
		};

		Dialogue dialogue1 = new Dialogue ();
		dialogue1.setListOfDialogues (dialogue1List);

		ConditionalNextNode nextNode1 = new ConditionalNextNode (); 
		nextNode1.setConditional (false);
		nextNode1.setDefaultNextNode (1);

		dialogue1.setNextNode (nextNode1);

		List<string> dialogue2List = new List<string> {
			"halo.",
			"ini cuma test.",
			"semoga ini berjalan dengan baik",
			"lorem ipsum"
		};

		Dialogue dialogue2 = new Dialogue (); 
		dialogue2.setListOfDialogues (dialogue2List);

		ConditionalNextNode nextNode2 = new ConditionalNextNode (); 
		nextNode2.setConditional (false);
		nextNode2.setDefaultNextNode (2);

		dialogue2.setNextNode (nextNode2);

		List<string> dialogue3List = new List<string> {
			"sampai disini dulu",	
			"semoga berhasil",
			"now pick an option to test this buttons"
		};

		Dialogue dialogue3 = new Dialogue ();
		dialogue3.setListOfDialogues (dialogue3List);

		ConditionalNextNode nextNode3 = new ConditionalNextNode (); 
		nextNode3.setConditional (false);

		ProgressionStats progStatsFor3 = new ProgressionStats (2, 0, 0);
		nextNode3.populateDestinationList (new KeyValuePair<ProgressionStats, int> (progStatsFor3, 3));
		nextNode3.setDefaultNextNode (5);

		dialogue3.setNextNode (nextNode3);

		Choice choice1 = new Choice(); 

		KeyValuePair<string,int> c1 = new KeyValuePair<string, int> ("option1", 3);
		KeyValuePair<string,int> c2 = new KeyValuePair<string,int> ("option2", 4);

		List<KeyValuePair<string,int>> listOfChoices1 = new List<KeyValuePair<string,int>> { c1, c2 };

		choice1.populateChoices (listOfChoices1);


		List<string> dialogue4List = new List<string> {
			"this is option1.",
			"if you want to see another option, change your character progression in testing mode"
		};

		Dialogue dialogue4 = new Dialogue ();
		dialogue4.setListOfDialogues (dialogue4List);

		ConditionalNextNode nextNode4 = new ConditionalNextNode (); 
		nextNode4.setConditional (false);

		nextNode4.setDefaultNextNode (-1);

		dialogue4.setNextNode (nextNode4);

		List<string> dialogue5List = new List<string> {
			"this is option2.",
			"if you want to see another option, change your character progression in testing mode"
		};

		Dialogue dialogue5 = new Dialogue ();
		dialogue5.setListOfDialogues (dialogue5List);

		ConditionalNextNode nextNode5 = new ConditionalNextNode (); 
		nextNode5.setConditional (true);

		ProgressionStats progStats5 = new ProgressionStats (1, 0, 0);


		nextNode5.setDefaultNextNode (-1);

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
