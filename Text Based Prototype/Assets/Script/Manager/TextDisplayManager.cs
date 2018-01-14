using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayManager : MonoBehaviour {

	[SerializeField]
	private Text dialogueDisplay; 
	[SerializeField]
	private Text nameDisplay; 

	[SerializeField]
	private float letterAdditionCooldown; 

	private List<string> listOfDialogues; 
	private char[] currentText; 
	private string displayedText; 
	private int currentDialogueIndex;

	public enum STATE {STAND_BY_TO_NEXT_TEXT, STAND_BY_FINISH_DIALOGUE, DISPLAYING_TEXT, IDLE};
	private STATE currentState;

	// Use this for initialization
	void Awake () {
		this.currentState = STATE.IDLE; 
		this.currentDialogueIndex = 0; 
	}

	// Update is called once per frame
	bool changeToNextText = false; 

	void Update () {
		switch (currentState) {
		case STATE.IDLE:
			break;
		case STATE.STAND_BY_TO_NEXT_TEXT:
			if (changeToNextText) {
				changeToNextText = false;
				if (currentDialogueIndex >= this.listOfDialogues.Count) {
					// inform the Day Manager that current dialogue is done
					// move to idle
					this.currentState = STATE.IDLE;
					return; 
				} else {
					moveToNextDialogueSegment ();
				}

			}
			break;
		case STATE.DISPLAYING_TEXT:
			displayText ();
			break;
		}
	}

	float currentWaitingTime = 0.0f;
	int currentLetterPointer = 0;
	string textToDisplay;
	void moveToNextDialogueSegment() {
		this.currentText = this.listOfDialogues [currentDialogueIndex++].ToCharArray();

		this.currentLetterPointer = 0; 
		this.textToDisplay = string.Empty;

		this.currentState = STATE.DISPLAYING_TEXT;
	}
		
	void displayText() {
		if (currentLetterPointer == currentText.Length) {
			this.currentState = STATE.STAND_BY_TO_NEXT_TEXT;

			return; 
		}

		if (currentWaitingTime < this.letterAdditionCooldown) {
			currentWaitingTime += Time.deltaTime;
			return; 
		} else {
			char nextCharacter = currentText [currentLetterPointer++];
			textToDisplay += nextCharacter; 
			dialogueDisplay.text = textToDisplay;

			this.currentWaitingTime = 0.0f; 
		}
	}

	public void supplyNewDialogue(List<string> newDialogues, string name) {
		// should not update the dialogue while the current list of text is not exhausted yet
		if (this.currentState != STATE.IDLE) {
			return;
		}	

		this.listOfDialogues = newDialogues;
		this.nameDisplay.text = name; 

		this.currentDialogueIndex = 0; 
		moveToNextDialogueSegment ();
	}
}
