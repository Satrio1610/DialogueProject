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

	[SerializeField]
	private DialogueAnimationController ShioDialogueBoxAnimationController; 

	[Header("GRAHAM UI ELEMENTS")]
	[SerializeField]
	private TextDisplayManager GrahamTextDisplay;

	[SerializeField]
	private ChoiceButtonManager GrahamChoiceButtons; 

	[SerializeField]
	private DialogueAnimationController GrahamDialogueBoxAnimationController; 


	[Header("BACKGROUND MANAGER")]
	[SerializeField]
	private BackgroundManager ImageBackgroundManager; 


	[Header("TRANSITION MANAGER")]
	[SerializeField]
	private TransitionManager ImageTransitionManager; 

	[Header("TEXT DISPLAY COOLDOWN")]
	[SerializeField]
	private float textDisplayCooldown; 

	private TextDisplayManager activeTextDisplayManager; 
	private ChoiceButtonManager activeChoiceButtonManager; 
	private DialogueAnimationController activeDialogueAnimationController; 


	#region Initialization
	public void setActiveUIComponent(You.NAME activeCharacter){
		if (activeCharacter == You.NAME.GRAHAM) {
			this.activeTextDisplayManager = GrahamTextDisplay;

			this.activeChoiceButtonManager = GrahamChoiceButtons;

			this.activeDialogueAnimationController = GrahamDialogueBoxAnimationController;

			this.ShioTextDisplay.gameObject.SetActive (false);
			this.ShioChoiceButtons.gameObject.SetActive (false);

		} else {
			this.activeTextDisplayManager = ShioTextDisplay;

			this.activeChoiceButtonManager = ShioChoiceButtons;

			this.activeDialogueAnimationController = ShioDialogueBoxAnimationController;

			this.GrahamTextDisplay.gameObject.SetActive (false); 
			this.GrahamChoiceButtons.gameObject.SetActive (false);
		}

		this.activeTextDisplayManager.gameObject.SetActive (true); 
		this.activeChoiceButtonManager.gameObject.SetActive (true);

		resetUIInitialState ();
	}
	#endregion

	#region UpdateTextDisplay
	public void showNextText() {
		this.activeTextDisplayManager.showNextCharacter ();
	}
	
	// Update is called once per frame
	public void updateDisplayWithCurrentNode (Node currentNode) {
		Node.NODE_TYPE currentNodeType = currentNode.getNodeType ();

		if (currentNodeType == Node.NODE_TYPE.CHOICE) {
			Choice newOptions = (Choice)currentNode;
			this.activeChoiceButtonManager.showChoices (newOptions.getChoices ());
		} else if (currentNodeType == Node.NODE_TYPE.DIALOGUE){
			Dialogue newDialogue = (Dialogue)currentNode;
			this.activeTextDisplayManager.supplyNewDialogue (newDialogue.getListOfDialogues (), newDialogue.getDialogueCharacter().name());
		}
	}
	#endregion

	#region subscription
	public void subscribeToTextManagers_FinishedDialogueEvent(UnityAction action) {
		this.ShioTextDisplay.subscribeToFinishedDialogueEvent (action);
		this.GrahamTextDisplay.subscribeToFinishedDialogueEvent (action);
	}

	public void subscribeToChoiceButtonsManager_EffectEvent(UnityAction<ChoiceEffect> action){
		this.ShioChoiceButtons.subscribeToSelectedEffectEventButtons (action);
		this.GrahamChoiceButtons.subscribeToSelectedEffectEventButtons (action);
	}
	#endregion

	#region dialogueBoxAnimation 
	public void setDialogueBoxAnimation(DialogueAnimationController.BAR_MOVEMENT instruction){
		this.activeDialogueAnimationController.playDialogueBarAnimation (instruction);
	}

	public bool hasFinishedDialogueAnimation() {
		return this.activeDialogueAnimationController.hasFinishedAnimation ();
	}
	#endregion

	#region backgroundManager
	public void setBackgroundImage(BackgroundManager.BACKGROUND_IMAGE image) {
		this.ImageBackgroundManager.updateBackgroundImage (image);
	}
	#endregion

	#region TransitionManager
	public void startFadeOut() {
		this.ImageTransitionManager.fadeOutTransition ();
	}

	public void startFadeIn() {
		this.ImageTransitionManager.fadeInTransition ();
	}

	public bool hasFinishedFading() {
		Debug.Log ("has finished fading: " + this.ImageTransitionManager.hasFinishedFading ());
		return this.ImageTransitionManager.hasFinishedFading ();
	}
	#endregion

	public void resetUIInitialState() {
		//Hide DialogueBar
		this.activeDialogueAnimationController.playDialogueBarAnimation(DialogueAnimationController.BAR_MOVEMENT.DOWN_INSTANT);

		//Turn FadeOutImage completeBlack 
		this.ImageTransitionManager.instantCompleteFadeOut();

		this.activeTextDisplayManager.showNoNameBoxDialogueBar ();
		this.activeTextDisplayManager.clearText ();
	}
}
