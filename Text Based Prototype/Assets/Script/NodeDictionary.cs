using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeDictionary : SerializableDictionary<int, Node> {

}

[System.Serializable]
public class IntDictionary: SerializableDictionary<int,int>{}

[System.Serializable]
public class DialogueDictionary: SerializableDictionary<int, Dialogue>{
	public DialogueDictionary cloneDictionary() {
		DialogueDictionary clonedDictionary = new DialogueDictionary (); 

		foreach (KeyValuePair<int, Dialogue> pair in this) {
			clonedDictionary[pair.Key] = pair.Value; 
		}

		return clonedDictionary;
	}
}

[System.Serializable]
public class ChoiceDictionary: SerializableDictionary<int, Choice>{

	public ChoiceDictionary cloneDictionary() {
		ChoiceDictionary clonedDictionary = new ChoiceDictionary (); 

		foreach (KeyValuePair<int, Choice> pair in this) {
			clonedDictionary[pair.Key] = pair.Value; 
		}

		return clonedDictionary;
	}
};

[System.Serializable]
public class BranchDictionary: SerializableDictionary<int, Branch>{

	public BranchDictionary cloneDictionary() {
		BranchDictionary clonedDictionary = new BranchDictionary (); 

		foreach (KeyValuePair<int, Branch> pair in this) {
			clonedDictionary[pair.Key] = pair.Value; 
		}

		return clonedDictionary;
	}
};

