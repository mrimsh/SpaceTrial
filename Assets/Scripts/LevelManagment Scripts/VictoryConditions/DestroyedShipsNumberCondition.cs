using UnityEngine;
using System.Collections;

[System.Serializable]
public class DestroyedShipsNumberCondition : VictoryCondition
{
	public float shipsToDestroy = 10;

	public override bool CheckVictoryCondition ()
	{
		if (isToCheck) {
			return GetStatistics ().numberOfDestroyedShips >= shipsToDestroy;
		} else {
			return true;
		}
	}
}
