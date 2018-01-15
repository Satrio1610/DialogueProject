using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice : Node {

	[SerializeField]
	private List<KeyValuePair<string,int>> ListOfChoices;

	public Choice(): base() {
		// from parents
		this.NodeType = NODE_TYPE.CHOICE;

		// original
		this.ListOfChoices = new List<KeyValuePair<string,int>> ();
	}

	public void populateChoices(List<KeyValuePair<string,int>> choices){
		this.ListOfChoices = choices; 
	}

	public List<KeyValuePair<string,int>> getChoices() {
		return this.ListOfChoices;
	}
}
