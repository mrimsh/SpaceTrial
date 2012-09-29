using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class VictoryCondition
{
	public bool isToCheck = false;
	private Statistics statistics;

	public abstract bool CheckVictoryCondition ();

	public Statistics GetStatistics ()
	{
		if (statistics == null) {
			statistics = GameObject.Find ("GameManager").GetComponent<Statistics> ();
		}
		return statistics;
	}
}
