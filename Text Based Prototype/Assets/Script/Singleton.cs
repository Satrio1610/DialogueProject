﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {
	public Singleton instance; 
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
