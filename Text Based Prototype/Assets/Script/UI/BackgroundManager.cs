using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundManager : MonoBehaviour {

	[SerializeField] 
	private Image backgroundImage;

	public enum BACKGROUND_IMAGE {
		BLACK, 
		PHONE,
	}

	[SerializeField]
	private Sprite blackImage; 

	[SerializeField]
	private Sprite phoneImage; 
	// Use this for initialization
	void Awake () {
		this.backgroundImage = this.GetComponent<Image> ();	
	}
	
	public void updateBackgroundImage(BACKGROUND_IMAGE image) {
		switch (image) {
		case BACKGROUND_IMAGE.BLACK:
			this.backgroundImage.sprite = blackImage;
			break;
		case BACKGROUND_IMAGE.PHONE:
			this.backgroundImage.sprite = phoneImage;
			break;
		}
	}
}
