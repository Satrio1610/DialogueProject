using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : Node {
	
	[SerializeField]
	public List<string> ListOfDialogues;

	[SerializeField]
	private Character character;

	public Dialogue(int id) : base(id){
		
		this.NodeType = NODE_TYPE.DIALOGUE;
		ListOfDialogues = new List<string> ();
	}
}
