using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character {
	string name;
	// determine the number of image later 
	Sprite[] spriteSheet; 

	public Sprite getDefaultSprite(){
		return spriteSheet [0];
	}

	public Sprite getHappySprite() {
		return spriteSheet [1];
	}

	public Sprite getSadSprite() {
		return spriteSheet [2];
	}
}
