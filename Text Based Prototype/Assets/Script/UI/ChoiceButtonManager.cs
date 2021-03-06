﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
	
// this script should be the one managing the buttons in term of showing the correct number of buttons + showing the 
public class ChoiceButtonManager : MonoBehaviour {

	[SerializeField]
	ChoiceButton[] choiceButtons; 

	void Awake() {
		this.choiceButtons = this.GetComponentsInChildren<ChoiceButton> (); 
		this.subsribeToOnClickEventButtons (disableAllButtons);

		this.disableAllButtons ();
	}

	public void disableAllButtons() {
		for (int i = 0; i < choiceButtons.Length; i++) {
			Debug.Log ("disable");
			choiceButtons [i].gameObject.SetActive (false);
		}	
	}

	public void showChoices(List<KeyValuePair<string,ChoiceEffect>> options){
		for (int i = 0; i < options.Count; i++) {
			Debug.Log ("emable" + i);
			choiceButtons [i].gameObject.SetActive (true);
			choiceButtons [i].setButtonText (options [i].Key);
			choiceButtons [i].setNewChoiceEffect(options [i].Value);
		}
	}

	public void subscribeToSelectedEffectEventButtons(UnityAction<ChoiceEffect> action){
		for (int i = 0; i < choiceButtons.Length; i++) {
			choiceButtons [i].subscribeToChoiceEffectEvent (action);
		}
	}

	public void subsribeToOnClickEventButtons(UnityAction action){
		for (int i = 0; i < choiceButtons.Length; i++) {
			choiceButtons [i].subscribeToOnClickEvent (action);
		}
	}
}
