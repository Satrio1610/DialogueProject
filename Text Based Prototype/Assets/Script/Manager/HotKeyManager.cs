using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeyManager : MonoBehaviour {

	public enum MODE {
		INGAME,
		BINDING_MENU
		}

	public KeyCode skipOrNextDialogue;

	public KeyCode quickSave;

	public KeyCode quickLoad; 

	private DayExecutionManager dayManager; 

	private MODE currentMode; 
	// Use this for initialization
	void Start () {
		dayManager = this.GetComponent<DayExecutionManager> ();		
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentMode) {
		case MODE.INGAME:
			listenToPlayerAction ();
			break;
		}

	}

	void listenToPlayerAction() {
		if (Input.GetKeyDown (skipOrNextDialogue)) {
			Debug.Log("next dialogue");
			this.dayManager.callNextText ();
			return;
		}

		if (Input.GetKeyDown (quickSave)) {
			Debug.Log ("quickSave");
		}

		if (Input.GetKeyDown (quickLoad)) {
			Debug.Log ("quickLoad");
		}
	}

	public void setHotKeyManagerMode(HotKeyManager.MODE mode) {
		this.currentMode = mode; 
	}
}
