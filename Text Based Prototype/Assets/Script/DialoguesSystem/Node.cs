using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node {
	public enum NODE_TYPE {
		NONE,
		DIALOGUE,
		CHOICE,
		BRANCH
	};

	[SerializeField]
	protected NODE_TYPE NodeType; 


	public Node(){
		this.NodeType = NODE_TYPE.NONE;

	}

	public NODE_TYPE getNodeType() {
		if (this.NodeType != null) {
			return this.NodeType;
		}
	}


		
}
