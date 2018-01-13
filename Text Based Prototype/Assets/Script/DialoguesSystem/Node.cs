using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node {
	public enum NODE_TYPE {DIALOGUE,CHOICE};

	[SerializeField]
	protected int _id;

	[SerializeField]
	protected NODE_TYPE NodeType; 

	[SerializeField]
	protected ConditionalNextNode nextNode;

	public Node(int id){
		this._id = id; 
	}

	public NODE_TYPE getNodeType() {
		if (this.NodeType != null) {
			return this.NodeType;
		}
	}
}
