using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {

	private class ChoiceButtonIntEvent: UnityEvent<int>{};

	private Button button;
	private Text buttonText; 
	private int nextNodeID; 
	private ChoiceButtonIntEvent intEvent; 

	void Awake() {
		this.button = this.GetComponent<Button> (); 
		this.buttonText = this.GetComponentInChildren<Text> (); 
		this.nextNodeID = -1; 

		this.intEvent = new ChoiceButtonIntEvent ();
		this.button.onClick.AddListener (delegate{this.intEvent.Invoke(nextNodeID);});
	}

	// Use this for initialization
	void Start () {
		//this.gameObject.SetActive (false);
	}
	
	public void subscribeToOnClickEvent(UnityAction action) {
		this.button.onClick.AddListener (action);
	}

	public void setButtonText(string text){
		this.buttonText.text = text; 
	}

	public void setNextNodeID(int nodeID){
		this.nextNodeID = nodeID; 
	}

	public void subscribeToIntEvent(UnityAction<int> intAction){
		this.intEvent.AddListener (intAction);
	}
		

}
