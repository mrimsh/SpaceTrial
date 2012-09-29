using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

public class WeaponSlot : TypicalSlot
{

	/// <summary>
	/// Position where from bullet starts moving 
	/// </summary>
	public Vector3 bulletSpawnPosition;

	/// <summary>
	/// Fires a weapon, if it mounted on slot.
	/// </summary>
	/// <param name="isForced">Forces fire, even if weapon isn't active.</param>
	public void Fire (bool isForced)
	{
		if (mountedEquipmentUnit != null) {
			if (((TypicalWeapon)mountedEquipmentUnit).isActive || isForced) {
				((TypicalWeapon)mountedEquipmentUnit).Fire ();
			}
		}
	}
}
