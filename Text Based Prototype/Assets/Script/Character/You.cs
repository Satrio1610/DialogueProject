using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this script should contain the player's progression and their current stats on the game(affection, etc)
public class You : MonoBehaviour {

	public ProgressionStats yourProgression;
	[SerializeField]
	public bool isTesting;
	// Use this for initialization
	void Start () {
		if (isTesting) {
			yourProgression = new ProgressionStats (0, 0, 0);
		}
		// load the latest stats 
		// Set to not destroyable
	}

	void saveProgression() {}
	void loadProgression() {}

}

// the following class will be used by You object and conditional Next Node to cross check condition 
[System.Serializable]
public class ProgressionStats {
	private int affectionlevel; 
	private int currentDay; 
	private int currentNode;

	// key is day, list is nodes for each day
	private Dictionary<int,List<int>> nodesCompleted; 

	public ProgressionStats(int aLevel, int cDay, int cNode){
		this.affectionlevel = aLevel; 
		this.currentDay = cDay;
		this.currentNode = cNode; 

		nodesCompleted = new Dictionary<int,List<int>> ();
	}

	// accessor 
	public int getAffectionLevel() {
		return this.affectionlevel; 
	}

	public int getCurrentDay() {
		return this.currentDay; 
	}

	public int getCurrentNode() {
		return this.currentNode; 
	}

	public Dictionary<int,List<int>> getCompletedNodes() {
		return this.nodesCompleted;
	}


}
