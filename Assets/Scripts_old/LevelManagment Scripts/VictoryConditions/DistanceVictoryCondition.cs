using UnityEngine;
using System.Collections;

[System.Serializable]
public class DistanceVictoryCondition : VictoryCondition
{
	public float distanceToAchieve = 300;

	public override bool CheckVictoryCondition ()
	{
		if (isToCheck) {
			return GetStatistics ().playerDistanceMoved >= distanceToAchieve;
		} else {
			return true;
		}
	}
}
