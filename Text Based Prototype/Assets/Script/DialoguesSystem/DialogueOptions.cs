using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOptions : Node {

	[SerializeField]
	private List<string> ListOfChoices;

	public DialogueOptions(int id): base(id) {
		// from parents
		this._id = id; 
		this.NodeType = NODE_TYPE.CHOICE;

		// original
		this.ListOfChoices = new List<string> ();
	}
}
