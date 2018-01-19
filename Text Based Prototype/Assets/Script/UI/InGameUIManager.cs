using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour {

	[Header("SHIO TEXT ELEMENTS")]
	[SerializeField]
	private TextDisplayManager ShioTextDisplay;

	[SerializeField]
	private ChoiceButtonManager ShioChoiceButtons;

	[Header("GRAHAM UI ELEMENTS")]
	[SerializeField]
	private TextDisplayManager GrahamTextDisplay;

	[SerializeField]
	private ChoiceButtonManager GrahamChoiceButtons; 

	[Header("TEXT DISPLAY COOLDOWN")]
	private float textDisplayCooldown; 

	private TextDisplayManager activeTextDisplayManager; 
	private ChoiceButtonManager activeChoiceButtonManager; 



	// Use this for initialization
	public void setActiveUIComponent(You.NAME activeCharacter){
		if (activeCharacter == You.NAME.GRAHAM) {
			this.activeTextDisplayManager = GrahamTextDisplay;
			this.activeChoiceButtonManager = GrahamChoiceButtons;



			this.ShioTextDisplay.gameObject.SetActive (false);
			this.ShioChoiceButtons.gameObject.SetActive (false); 
		} else {
			this.activeTextDisplayManager = ShioTextDisplay;
			this.activeChoiceButtonManager = ShioChoiceButtons;

			this.GrahamTextDisplay.gameObject.SetActive (false); 
			this.GrahamChoiceButtons.gameObject.SetActive (false);
		}

		this.activeTextDisplayManager.gameObject.SetActive (true); 
		this.activeChoiceButtonManager.gameObject.SetActive (true);
	}

	public void showNextText() {
		this.activeTextDisplayManager.showNextCharacter ();
	}
	
	// Update is called once per frame
	public void updateDisplayWithCurrentNode (Node currentNode) {
		Node.NODE_TYPE currentNodeType = currentNode.getNodeType ();

		if (currentNodeType == Node.NODE_TYPE.CHOICE) {
			Choice newOptions = (Choice)currentNode;
			this.activeChoiceButtonManager.showChoices (newOptions.getChoices ());
		} else {
			Dialogue newDialogue = (Dialogue)currentNode;
			this.activeTextDisplayManager.supplyNewDialogue (newDialogue.getListOfDialogues (), newDialogue.getDialogueCharacter().name());
		}
	}

	public void subscribeToTextManagers_FinishedDialogueEvent(UnityAction action) {
		this.ShioTextDisplay.subscribeToFinishedDialogueEvent (action);
		this.GrahamTextDisplay.subscribeToFinishedDialogueEvent (action);
	}

	public void subscribeToChoiceButtonsManager_IntEvent(UnityAction<int> actionInt){
		this.ShioChoiceButtons.subscribeToIntEventButtons (actionInt);
		this.GrahamChoiceButtons.subscribeToIntEventButtons (actionInt);
	}
}
