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

	private SoundManagerScript.SFX_TYPE soundEffect;

	private int nextNodeID; 
	private DialoguePreAction preAction; 

	public Dialogue() : base(){
			
		this.NodeType = NODE_TYPE.DIALOGUE;
		this.expression = Character.CHARACTER_EXPRESSION.DEFAULT;
		ListOfDialogues = new List<string> ();
		this.nextNodeID = -1; 
		this.preAction = new DialoguePreAction ();
	}

	public void setCharacter(Character character){
		this.character = character;
	}

	public void setCharacterExpression(Character.CHARACTER_EXPRESSION expression){
		this.expression = expression; 

	}

	public void setSoundEffectPlayed(SoundManagerScript.SFX_TYPE sfx){
		this.soundEffect = sfx;
	}

	public void setNextNodeID(int nodeID) {
		this.nextNodeID = nodeID; 
	}

	public void setListOfDialogues(List<string> dialogues) {
		this.ListOfDialogues = new List<string> (dialogues);
	}

	public void setPreAction(DialoguePreAction newPreAction) {
		this.preAction = newPreAction;
	}

	public List<string> getListOfDialogues(){
		return this.ListOfDialogues;
	}

	public Character getDialogueCharacter() {
		return this.character;
	}

	public int getNextNode(){
		return this.nextNodeID;
	}

	public DialoguePreAction getPreAction() {
		return this.preAction;
	}

	public bool isEmptyDialogue(){
		return this.ListOfDialogues.Count == 0;
	}
}
