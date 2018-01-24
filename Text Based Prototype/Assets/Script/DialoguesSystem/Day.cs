using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Day{
	// for fun
	private uint day; 
	private uint month; 
	private uint year; 

	private DateTime dayDate; 

	private int startingNode;
	[SerializeField]
	private DialogueDictionary conversationNodes;
	[SerializeField]
	private ChoiceDictionary choiceNodes;
	[SerializeField]
	private BranchDictionary branchNodes; 
	private int nextDay;

	public Day(){

		this.dayDate = new DateTime ();

		//this.conversationNodes = new NodeDictionary (); 
		this.nextDay = -1; 
		this.startingNode = -1; 
	}

	public void setDate(int dd, int mm, int yy){
		
		this.dayDate = new DateTime ((int)yy, (int)mm, (int)dd);
	} 

	public void populateConversationnodes(DialogueDictionary convoNodes){
		this.conversationNodes = convoNodes;
	}

	public void populateChoiceNodes(ChoiceDictionary choiceNodes){
		this.choiceNodes = choiceNodes; 
	}

	public void populateBranchNodes(BranchDictionary branchNodes){
		this.branchNodes = branchNodes;
	}

	public Node getNextNode(int index) {
		if (conversationNodes.ContainsKey (index)) {
			return (Node)this.conversationNodes [index];
		} 

		if (choiceNodes.ContainsKey (index)) {
			return (Node)this.choiceNodes [index];
		}

		if (branchNodes.ContainsKey (index)) {
			return(Node)this.branchNodes [index];
		}

		Debug.LogError ("NODES NOT FOUND");
		return new Node ();
	}

	public void setNextDay(int day){
		this.nextDay = day; 
	}

	public void setStartingNode(int id){
		this.startingNode = id; 
	}

	public string getDateString() {
		return this.dayDate.ToString ("dd/mm");
	}

	public DialogueDictionary getConversationNodes(){
		return this.conversationNodes; 
	}

	public int getStartingNode(){
		return this.startingNode;
	}


}
