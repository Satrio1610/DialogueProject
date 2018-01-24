using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimationController : MonoBehaviour {
	public Animator activeDialogueBarAnimator; 

	public enum BAR_MOVEMENT{
		NONE,
		UP_INSTANT, 
		DOWN_INSTANT, 
		BRING_UP,	
		BRING_DOWN
	}
		 
	private bool hasDoneAnimation; 

	void Awake() {
		this.activeDialogueBarAnimator = this.GetComponent<Animator> ();

	}

	void Start() {
		playDialogueBarAnimation (BAR_MOVEMENT.DOWN_INSTANT);
	}

	public void invokeEndAnimation(){
		this.hasDoneAnimation = true; 
	}

	public void invokeEndAnimationForInverse() {
		if (this.activeDialogueBarAnimator.GetCurrentAnimatorStateInfo (0).IsName ("dialogueOut")) {
			Debug.Log ("current state is dialogue out");
			invokeEndAnimation (); 
		}
	}

	public bool hasFinishedAnimation() {
		return this.hasDoneAnimation;
	}

	public void playDialogueBarAnimation(BAR_MOVEMENT instruction){
		switch (instruction) {
		case BAR_MOVEMENT.UP_INSTANT:
			this.setDialogueBarUpInstantly ();
			this.hasDoneAnimation = true;
			break;
		case BAR_MOVEMENT.DOWN_INSTANT:
			this.setDialogueBarDownInstantly ();
			this.hasDoneAnimation = true; 
			break; 
		case BAR_MOVEMENT.BRING_UP:
			this.bringDialogueBarUp ();
			this.hasDoneAnimation = false; 
			break;
		case BAR_MOVEMENT.BRING_DOWN:
			this.bringDialogueBarDown ();
			this.hasDoneAnimation = false;
			break;
		}
	}

	void setDialogueBarDownInstantly(){
		//shioDialogueBarAnimator.Play ("raiseIn");
		activeDialogueBarAnimator.Play ("down");
	}

	void setDialogueBarUpInstantly() {
		activeDialogueBarAnimator.Play ("up");
	}

	void bringDialogueBarUp() {
		activeDialogueBarAnimator.Play ("dialogueIn");
	}

	void bringDialogueBarDown() {
		activeDialogueBarAnimator.Play ("dialogueOut");
	}
}
