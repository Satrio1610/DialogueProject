using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayExecutionManager : MonoBehaviour {

	[SerializeField]
	private TextDisplayManager textDisplayManager;

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
		textDisplayManager.subscribeToFinishedDialogueEvent(loadNextNode);

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

		Node.NODE_TYPE currentNodeType = currentNode.getNodeType ();

		if (currentNodeType == Node.NODE_TYPE.CHOICE) {
		
		} else {
			Dialogue newDialogue = (Dialogue)currentNode;
			Debug.Log ("printing");
			textDisplayManager.supplyNewDialogue (newDialogue.getListOfDialogues (), "hahahaha");
		}
	}

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
			"talk to you later"
		};

		Dialogue dialogue3 = new Dialogue ();
		dialogue3.setListOfDialogues (dialogue3List);


		ConditionalNextNode nextNode3 = new ConditionalNextNode (); 
		nextNode3.setConditional (false);
		nextNode3.setDefaultNextNode (-1);

		dialogue3.setNextNode (nextNode3);

		testNodes.Add (0, (Node)dialogue1);
		testNodes.Add (1, (Node)dialogue2);
		testNodes.Add (2, (Node)dialogue3);

		testDay.populateConversationnodes (testNodes);
		this.currentDay = testDay;
	}
}
