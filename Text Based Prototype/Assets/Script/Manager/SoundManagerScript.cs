using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
	[SerializeField]
	AudioSource BGMSource;
	[SerializeField]
	AudioSource SFXSource;

	[Header("BGM")]
	[SerializeField]
	AudioClip firstBGM;

	[Header("SFX")]
	[SerializeField]
	AudioClip phoneBeepSFX; 
	[SerializeField]
	AudioClip planeSFX;

	public enum SFX_TYPE {
		PHONE_BEEP,
		PLANE_SFX
	}

	public enum BGM_TYPE {
		BGM_WHATEVER
	}
	// Use this for initialization
	public static SoundManagerScript instance; 
	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this; 
		} else {
			if (instance != this) {
				Destroy (this.gameObject);
			}
		}

		DontDestroyOnLoad (this);
	}

	void Start () {
		this.BGMSource.Stop ();
		this.SFXSource.Stop ();
	}

	public void playBGM(BGM_TYPE bgmType, bool trueOrFalse) {
		switch (bgmType) {
		case BGM_TYPE.BGM_WHATEVER:
			this.BGMSource.clip = this.firstBGM;
			break;
		}

		this.BGMSource.loop = trueOrFalse;
		this.BGMSource.Play ();
	}
	
	public void playSFX(SFX_TYPE sfxType){

		switch(sfxType){
		case SFX_TYPE.PHONE_BEEP:
			this.SFXSource.clip = this.phoneBeepSFX;
			break;
		case SFX_TYPE.PLANE_SFX:
			this.SFXSource.clip = this.planeSFX;
			break;
		}

		this.SFXSource.Play ();
	}
}
