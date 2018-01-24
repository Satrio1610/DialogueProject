using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {

	private class ChoiceButtonEffectEvent: UnityEvent<ChoiceEffect>{};

	private Button button;
	private Text buttonText; 
	//TODO: REMOVE
	private int nextNodeID; 

	private ChoiceEffect cEffect; 
	private ChoiceButtonEffectEvent intEvent; 

	void Awake() {
		this.button = this.GetComponent<Button> (); 
		this.buttonText = this.GetComponentInChildren<Text> (); 

		this.nextNodeID = -1; 

		this.intEvent = new ChoiceButtonEffectEvent ();
		this.button.onClick.AddListener (delegate{this.intEvent.Invoke(cEffect);});
	}

	// Use this for initialization
	void Start () {
		//this.gameObject.SetActive (false);
	}
	
	public void subscribeToOnClickEvent(UnityAction action) {
		if (this.button == null) {
			this.button = this.GetComponent<Button> ();
		}
		this.button.onClick.AddListener (action);
	}

	public void setButtonText(string text){
		this.buttonText.text = text; 
	}

	public void setNextNodeID(int nodeID){
		this.nextNodeID = nodeID; 
	}

	public void setNewChoiceEffect(ChoiceEffect newEffect){
		this.cEffect = newEffect;
	}

	public void subscribeToChoiceEffectEvent(UnityAction<ChoiceEffect> Action){
		if (this.intEvent == null) {
			this.intEvent = new ChoiceButtonEffectEvent ();
		}
		this.intEvent.AddListener (Action);
	}
		

}
