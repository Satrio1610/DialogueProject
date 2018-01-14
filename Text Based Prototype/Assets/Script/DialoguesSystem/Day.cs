using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day{
	// for fun
	private uint day; 
	private uint month; 
	private uint year; 

	private DateTime dayDate; 

	private int startingNode;
	private Dictionary<int,Node> conversationNodes;
	private int nextDay;

	public Day(){

		this.dayDate = new DateTime ();

		this.conversationNodes = new Dictionary<int,Node> (); 
		this.nextDay = -1; 
		this.startingNode = -1; 
	}

	public void setDate(uint dd, uint mm, uint yy){
		
		this.dayDate = new DateTime ((int)dd, (int)mm, (int)yy);
	} 

	public void populateConversationnodes(Dictionary<int,Node> convoNodes){
		this.conversationNodes = convoNodes;
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

	public Dictionary<int,Node> getConversationNodes(){
		return this.conversationNodes; 
	}

	public int getStartingNode(){
		return this.startingNode;
	}


}
