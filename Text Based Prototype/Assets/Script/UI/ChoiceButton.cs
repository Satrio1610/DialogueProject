using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {
	private Button button;
	private Text buttonText; 
	private int nextNodeID; 

	void Awake() {
		this.button = this.GetComponent<Button> (); 
		this.buttonText = this.GetComponentInChildren<Text> (); 
		this.nextNodeID = -1; 

	}

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (false);
	}
	
	public void setOnActiveEvent(UnityAction action) {
		this.button.onClick.AddListener (action);
	}

	public void setButtonText(string text){
		this.buttonText.text = text; 
	}

	public void setNextNodeID(int nodeID){
		this.nextNodeID = nodeID; 
	}
}
