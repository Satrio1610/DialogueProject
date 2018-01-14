using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
	
// this script should be the one managing the buttons in term of showing the correct number of buttons + showing the 
public class ChoiceButtonManager : MonoBehaviour {
	[SerializeField]
	ChoiceButton[] choiceButtons; 

	void Start() {
		this.disableAllButtons ();
		//List<string> testString = new List<string>{ "1", "2","3" };
		//this.showChoices (testString);
	}

	public void disableAllButtons() {
		for (int i = 0; i < choiceButtons.Length; i++) {
			choiceButtons [i].gameObject.SetActive (false);
		}	
	}

	public void showChoices(List<KeyValuePair<string,int>> options){
		for (int i = 0; i < options.Count; i++) {
			choiceButtons [i].gameObject.SetActive (true);
			choiceButtons [i].setButtonText (options [i].Key);
			choiceButtons [i].setNextNodeID (options [i].Value);
		}
	}
}
