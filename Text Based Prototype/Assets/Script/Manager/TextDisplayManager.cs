using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextDisplayManager : MonoBehaviour {

	[Header("TEXT COMPONENT")]
	[SerializeField]
	private Text dialogueDisplay; 
	[SerializeField]
	private Text nameDisplay; 

	[Header("LETTER/CHARACTER ADDITION COOLDOWN")]
	[SerializeField]
	private float letterAdditionCooldown; 

	[Header("IMAGE COMPONENT")]
	[SerializeField]
	private Image dialogueBox; 
	[SerializeField]
	[Tooltip("Dialogue box wihout name box attached")]
	private Sprite noNameSprite; 
	[SerializeField]
	[Tooltip("Dialogue box with name box attached")]
	private Sprite nameSprite;

	private List<string> listOfDialogues; 
	private char[] currentText; 
	private string displayedText; 
	private int currentDialogueIndex;

	public enum STATE {STAND_BY_TO_NEXT_TEXT, STAND_BY_FINISH_DIALOGUE, DISPLAYING_TEXT, IDLE};
	private STATE currentState;

	private UnityEvent finishCurrentDialogueEvent;

	#region set up 
	// Use this for initialization
	void Awake () {
		this.currentState = STATE.IDLE; 
		this.currentDialogueIndex = 0; 
		finishCurrentDialogueEvent = new UnityEvent ();

		//obtain dialogueBox Image 
		this.dialogueBox = this.GetComponent<Image>();
	}

	// should be called by the DayExecutionManager
	public void setDialogueSprites(Sprite noNameDialogueBox, Sprite namedDialogueBox){
		this.noNameSprite = noNameDialogueBox;
		this.nameSprite = namedDialogueBox;
	}

	public void setTextDisplayCooldown(float coolDown_Second){
		this.letterAdditionCooldown = coolDown_Second; 
	}


	#endregion
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
					finishCurrentDialogueEvent.Invoke();

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

	#region letter update

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

	#endregion

	#region event subscription
	public void subscribeToFinishedDialogueEvent(UnityAction action){
		this.finishCurrentDialogueEvent.AddListener (action);
	}
	#endregion


	public void showNextCharacter() {
		if (this.currentState == STATE.STAND_BY_TO_NEXT_TEXT) {
			changeToNextText = true;
			return;
		}

		if(this.currentState == STATE.DISPLAYING_TEXT){
			this.currentWaitingTime = 0.0f; 
			dialogueDisplay.text = new string(this.currentText);
			this.currentState = STATE.STAND_BY_TO_NEXT_TEXT;
			return;
		}
	}
}
