using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindingManager : MonoBehaviour {

	[SerializeField]
	private KeyCode skipOrNextDialogue;
	[SerializeField]
	private KeyCode quickSave;
	[SerializeField]
	private KeyCode quickLoad; 

	private DayExecutionManager dayManager; 

	// Use this for initialization
	void Start () {
		dayManager = this.GetComponent<DayExecutionManager> ();		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (skipOrNextDialogue)) {
			this.dayManager.callNextText ();
		}
	}
}
