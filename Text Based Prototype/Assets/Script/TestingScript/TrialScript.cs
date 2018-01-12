using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialScript : MonoBehaviour {
	[SerializeField]
	string sampleText; 
	[SerializeField]
	Text textContainer; 
	[SerializeField]
	float additionCharacterCoolDown;

	char[] currentText; 
	char[] shownText;
	float waitingTime;
	bool hasFinishedShowingText; 

	// Use this for initialization
	void Start () {
		textContainer.text = string.Empty;	
		currentText = sampleText.ToCharArray(); 
		shownText = new char[currentText.Length];
		waitingTime = 0.0f; 
		hasFinishedShowingText = false; 
	}
	
	// Update is called once per frame

	void Update () {
		if (hasFinishedShowingText) {
			return; 
		}
		if (waitingTime < additionCharacterCoolDown) {
			waitingTime += Time.deltaTime;
			return; 
		} else {
			updateShownText(); 
		}


	}

	int currentCharacterPointer = 0; 
	void updateShownText(){

			if (currentCharacterPointer == currentText.Length) {
				hasFinishedShowingText = true; 
				return;
			}

			shownText [currentCharacterPointer] = currentText [currentCharacterPointer++];
			textContainer.text = new string (shownText);
			waitingTime = 0.0f; 
	}

}
