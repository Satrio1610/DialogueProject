using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScriptForDataStructure : MonoBehaviour {

	public List<Node> DialogueNodes;

	// Use this for initialization
	void Start () {
		this.DialogueNodes = new List<Node> (); 

		var thisIsDialogue = new Dialogue (); 
		var thisIsOption = new Choice ();
		this.DialogueNodes.Add (thisIsDialogue);
		this.DialogueNodes.Add (thisIsOption);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
