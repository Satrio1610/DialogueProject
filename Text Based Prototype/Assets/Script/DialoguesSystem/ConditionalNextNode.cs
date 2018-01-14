using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// this class should store and resolve the next node that is going to be travelled by the player
[System.Serializable]
public class ConditionalNextNode {

	private bool isConditional;

	// Should be from more specific requirement to less requirement, default should be last.
	// if there is no conditional, or for default destination, use default destination
	private List<KeyValuePair<ProgressionStats,int>> possibleDestinationList; 
	private int defaultDestination;
	// Constructor
	public ConditionalNextNode(){
		isConditional = false; 
		possibleDestinationList = new List<KeyValuePair<ProgressionStats, int>> ();
	}

	public void setConditional(bool trueOrFalse){
		this.isConditional = false; 
	}

	public void setDefaultNextNode(int nextNode){
		this.defaultDestination = nextNode;
	}

	public void populateDestinationList(KeyValuePair<ProgressionStats,int> nextPossibleDestination){
		this.possibleDestinationList.Add (nextPossibleDestination);
	}

	// get the next valid destination for player
	public int obtainTheNextDestination(You player){

		if (this.isConditional == false) {
			Debug.Log (this.defaultDestination);
			return this.defaultDestination;
		}

		ProgressionStats currentPlayerProgression = player.yourProgression;
		ProgressionStats nextProgressionCondition; 

		// go through the list of possible destination one by one, the first match is the next destination 
		// NOTE: list of possible destination need to be in descending order of more requirement to less requirement
		for (int i = 0; i < this.possibleDestinationList.Count; i++) {

			nextProgressionCondition = this.possibleDestinationList [i].Key;

			if (isValidDestination (currentPlayerProgression, nextProgressionCondition)) {
				return this.possibleDestinationList [i].Value;
			}

		}

		return defaultDestination;


	}

	public static bool isValidDestination(ProgressionStats playerProgressionStats, ProgressionStats progressionCondition){
		if (playerProgressionStats.getAffectionLevel () <= progressionCondition.getAffectionLevel ()) {
			return false; 
		}

		// get the required node
		Dictionary<int,List<int>> playerCompletedNodes = playerProgressionStats.getCompletedNodes();
		Dictionary<int,List<int>> requiredNodes = progressionCondition.getCompletedNodes();
		Dictionary<int,List<int>>.KeyCollection requiredDays = requiredNodes.Keys;
		int numberOfDaysInRequirement = requiredDays.Count;

		bool hasAllRequiredNodes = true; 

		foreach (int day in requiredDays) {
			
			// check if day exist in player progression stats
			if(!playerCompletedNodes.ContainsKey(day)){
				hasAllRequiredNodes = false; 
				break; 
			}

			List<int> requiredNodesInDay = new List<int>(); 
			List<int> playerAccumulatedNodesInDay = new List<int>();

			//obtain the required dialogues component
			playerCompletedNodes.TryGetValue (day,out requiredNodesInDay);
			requiredNodes.TryGetValue (day,out playerAccumulatedNodesInDay);

			if (requiredNodesInDay.Count == 0) {
				continue;
			}

			// Solution to check sublist gotten from stackoverflow, using system.linq
			hasAllRequiredNodes = !requiredNodes.Except(playerCompletedNodes).Any();

			if (!hasAllRequiredNodes) {
				break;
			}

		}

		return hasAllRequiredNodes;
	}

}
