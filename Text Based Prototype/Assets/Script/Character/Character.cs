using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character: MonoBehaviour {
	[SerializeField]
	private string characterName;

	public enum CHARACTER_EXPRESSION
	{
		DEFAULT,HAPPY,SAD
	}
	// determine the number of image later 
	public Sprite[] spriteSheet; 

	private Sprite getDefaultSprite(){
		return spriteSheet [0];
	}

	private Sprite getHappySprite() {
		return spriteSheet [1];
	}

	public Sprite getSadSprite() {
		return spriteSheet [2];
	}

	public void setName(string name) {
		this.characterName = name; 
	}

	public string name() {
		return this.characterName; 
	}

	public Sprite getCharacterExpression(CHARACTER_EXPRESSION expression){
	
		switch (expression) {
		case CHARACTER_EXPRESSION.DEFAULT:
			return spriteSheet [0];
		case CHARACTER_EXPRESSION.HAPPY:
			return spriteSheet [1];
		case CHARACTER_EXPRESSION.SAD:
			return spriteSheet [2];
		default:
			return spriteSheet [0];
		}
	}
}
