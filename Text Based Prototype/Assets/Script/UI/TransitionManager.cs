using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour {
	[SerializeField]
	private Image blackTransitionImage; 

	private bool isFading; 
	private bool doneFading; 

	[SerializeField]
	private float fadingSpeed_inSecond; 

	[SerializeField]
	[Tooltip("Should be below 1.0f")]
	private float fadeOutTargetAlpha; 
	[SerializeField]
	[Tooltip("Should be above 0.001f")]
	private float fadeInTargetAlpha; 

	private float targetAlpha; 

	private static float FULL_ALPHA = 1.0f; 
	private static float NO_ALPHA = 0.0F;

	// Use this for initialization
	void Start () {
		blackTransitionImage = this.GetComponent<Image> ();
		isFading = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isFading && hasReachedTargetAlpha()) {
			setImageToFinalTargetAlpha ();
			isFading = false;
			doneFading = true; 
			Debug.Log ("HII");
		}
	}

	bool hasReachedTargetAlpha() {
		if (targetAlpha == fadeOutTargetAlpha) {
			return this.blackTransitionImage.canvasRenderer.GetAlpha () >= targetAlpha;
		} else {
			Debug.Log ("fading iiiinnn");
			Debug.Log ("current alpha: " + this.blackTransitionImage.canvasRenderer.GetAlpha ());
			return this.blackTransitionImage.canvasRenderer.GetAlpha () <= targetAlpha;
		}
	}

	public bool hasFinishedFading() {
		return doneFading; 
	}

	void setImageToFinalTargetAlpha() {
		if (targetAlpha == fadeOutTargetAlpha) {
			instantCompleteFadeOut ();
		} else {
			instantCompleteFadeIn();
		}
	}

	public void instantCompleteFadeOut() {
		this.blackTransitionImage.CrossFadeAlpha (FULL_ALPHA, 0.0f, false);
	}

	public void instantCompleteFadeIn(){
		this.blackTransitionImage.CrossFadeAlpha (NO_ALPHA, 0.0f, false);
	}

	public void fadeOutTransition() {
		this.blackTransitionImage.canvasRenderer.SetAlpha (0.0001f);
		this.blackTransitionImage.CrossFadeAlpha (FULL_ALPHA, fadingSpeed_inSecond, false);
		targetAlpha = this.fadeOutTargetAlpha;
		isFading = true; 
		doneFading = false; 
	}

	public void fadeInTransition() {
		Debug.Log ("FADING IN YO");
		this.blackTransitionImage.CrossFadeAlpha (NO_ALPHA, fadingSpeed_inSecond, false);
		targetAlpha = this.fadeInTargetAlpha;
		isFading = true;
		doneFading = false; 
	}

}
