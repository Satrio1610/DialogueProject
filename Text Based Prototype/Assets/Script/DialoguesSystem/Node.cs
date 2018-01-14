using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node {
	public enum NODE_TYPE {DIALOGUE,CHOICE};

	[SerializeField]
	protected NODE_TYPE NodeType; 

	[SerializeField]
	protected ConditionalNextNode nextNode;

	public Node(){
	}

	public NODE_TYPE getNodeType() {
		if (this.NodeType != null) {
			return this.NodeType;
		}
	}

	public void setNextNode(ConditionalNextNode cnn) {
		this.nextNode = cnn;
	}

	public int getNextNode(You player){
		return nextNode.obtainTheNextDestination (player);
	}
}
