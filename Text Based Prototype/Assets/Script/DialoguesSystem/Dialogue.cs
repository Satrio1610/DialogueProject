using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : Node {
	
	[SerializeField]
	private List<string> ListOfDialogues;

	[SerializeField]
	private Character character;

	[SerializeField]
	private Character.CHARACTER_EXPRESSION expression; 

	public Dialogue() : base(){
		
		this.NodeType = NODE_TYPE.DIALOGUE;
		this.expression = Character.CHARACTER_EXPRESSION.DEFAULT;
		ListOfDialogues = new List<string> ();
	}

	public void setCharacter(Character character){
		this.character = character;
	}

	public void setCharacterExpression(Character.CHARACTER_EXPRESSION expression){
		this.expression = expression; 

	}

	public void setListOfDialogues(List<string> dialogues) {
		this.ListOfDialogues = new List<string> (dialogues);
	}

	public List<string> getListOfDialogues(){
		return this.ListOfDialogues;
	}

	public Character getDialogueCharacter() {
		return this.character;
	}
}
