using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node {

	[SerializeField]
	protected int _id;
	public enum NODE_TYPE {DIALOGUE,CHOICE};

	[SerializeField]
	protected NODE_TYPE NodeType; 

	public Node(int id){
		this._id = id; 
	}

	public NODE_TYPE getNodeType() {
		if (this.NodeType != null) {
			return this.NodeType;
		}
	}
}
