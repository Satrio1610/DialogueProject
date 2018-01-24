using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice : Node {

	[SerializeField]
	private List<KeyValuePair<string,ChoiceEffect>> ListOfChoices;

	public Choice(): base() {
		// from parents
		this.NodeType = NODE_TYPE.CHOICE;

		// original
		this.ListOfChoices = new List<KeyValuePair<string,ChoiceEffect>> ();
	}

	public void populateChoices(List<KeyValuePair<string,ChoiceEffect>> choices){
		this.ListOfChoices = choices; 
	}

	public List<KeyValuePair<string,ChoiceEffect>> getChoices() {
		return this.ListOfChoices;
	}
}

public class ChoiceEffect{
	[SerializeField]
	private int nextNodeID; 
	private int affectionPoint; 

	public ChoiceEffect(int nodeID, int affectionPointEarned){
		this.nextNodeID = nodeID; 
		this.affectionPoint = affectionPointEarned; 
	}

	public void setAffectionPointEarned(int points){
		this.affectionPoint = points; 
	}

	public void setDestinationNodeID(int id){
		this.nextNodeID = id; 
	}

	public int getAffectionPointEarned() {
		return this.affectionPoint; 
	}

	public int getDestinationNodeID(){
		return this.nextNodeID; 
	}
}
