using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this script should contain the player's progression and their current stats on the game(affection, etc)
public class You : MonoBehaviour {
	public enum NAME
	{
		SHIO,
		GRAHAM
	}

	public NAME yourCharacter; 
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

	//public void setCurrentDayID

}

// the following class will be used by You object and conditional Next Node to cross check condition 
[System.Serializable]
public class ProgressionStats {
	[SerializeField]
	private You.NAME playerCharacter; 
	[SerializeField]
	private int affectionlevel; 
	[SerializeField]
	private int currentDay; 
	[SerializeField]
	private int currentNode;

	private List<int> currentNodeList; 
	// key is day, list is nodes for each day
	private Dictionary<int,List<int>> nodesCompleted; 

	public ProgressionStats( int aLevel, int cDay, int cNode){
		this.affectionlevel = aLevel; 
		this.currentDay = cDay;
		this.currentNode = cNode; 

		nodesCompleted = new Dictionary<int,List<int>> ();
	}

	public void setCurrentDay(int day){
		this.currentDay = day; 

		this.nodesCompleted.Add (this.currentDay, new List<int> ());
	}

	public void setCurrentNode(int cDay, int cNode){
		if (cDay != this.currentDay) {
			return; 
		}
		Debug.Log ("setting node");
		this.currentNode = cNode; 	
		this.nodesCompleted [cDay].Add(this.currentNode);
		Debug.Log ("current day: " + cDay + ", currentNode: " + cNode + ", current stored node length: " + this.nodesCompleted [cDay].Count); 
	}

	public void setPlayerCharacter( You.NAME characterName){
		this.playerCharacter =  characterName;
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
