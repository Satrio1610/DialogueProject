using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePreAction{

	public enum ACTION {
		CHANGE_BACKGROUND,
		HIDE_SPRITE,
		PLAY_SFX,
		SHOW_SPRITE,
		FADE_IN, 
		FADE_OUT,
		PLAY_BGM,
		HIDE_DIALOGUE_BAR_INSTANT, 
		HIDE_DIALOGUE_BAR,
		SHOW_DIALOGUE_BAR_INSTANT,
		SHOW_DIALOGUE_BAR
	}

	private List<KeyValuePair<ACTION,int>> listOfAction; 

	public DialoguePreAction() {
		this.listOfAction = new List<KeyValuePair<ACTION,int>> ();
	}

	public void addNewPreAction(KeyValuePair<ACTION, int> preAction){
		this.listOfAction.Add(preAction);
	}

	public List<KeyValuePair<ACTION,int>> obtainedListOfPreAction() {
		return this.listOfAction;
	}

	public bool isEmpty(){
		return this.listOfAction.Count == 0;
	}
}
