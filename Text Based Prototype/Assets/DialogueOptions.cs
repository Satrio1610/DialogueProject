using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOptions : Node {

	[SerializeField]
	private List<string> ListOfChoices;

	public DialogueOptions() {
		this.NodeType = NODE_TYPE.CHOICE;
		this.ListOfChoices = new List<string> ();
	}
}
