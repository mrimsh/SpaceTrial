using UnityEngine;
using System.Collections;

[System.Serializable]
public class DestroyedSpecificShipsNumberCondition : VictoryCondition
{
	public SpecialShipTypeDestroyAimInfo[] shipTypesToDestroyData;

	public override bool CheckVictoryCondition ()
	{
		if (isToCheck) {
			bool result = true;
			ShipsStatistics neededShipsStatistics;
			
			if (GetStatistics ().ShipsStatistics.Count > 0) {
				foreach (SpecialShipTypeDestroyAimInfo shipTypeToDestroyData in shipTypesToDestroyData) {
					neededShipsStatistics = GetStatistics ().ShipsStatistics.Find (delegate(ShipsStatistics ship) { return ship.name == shipTypeToDestroyData.shipName.name; });
					if (neededShipsStatistics.quantity >= shipTypeToDestroyData.shipsToDestroy) {
						result &= true;
					} else {
						return false;
					}
				}
			}
			
			return result;
		} else {
			return true;
		}
	}
}

[System.Serializable]
public class SpecialShipTypeDestroyAimInfo
{
	public SpaceShipMotor shipName;
	public int shipsToDestroy;
}
