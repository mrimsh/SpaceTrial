using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ShipProperties
{
	/// <summary>
	/// Max weight of cargo that ship can carry. 
	/// </summary>
	public int maxCargoWeight;

	////////////
	/// HULL ///
	////////////

	/// <summary>
	/// Points of damage ship can take out.
	/// </summary>
	public float shipHullPoints;

	/// <summary>
	/// Max quantity of equipment the ship can carry on.
	/// </summary>
	public int cargoCapacity;

	//////////////
	/// ENGINE ///
	//////////////

	/// <summary>
	/// The max speed the character can reach strafe speed (right-left ship movement).
	/// </summary>
	public float strafeMaxSpeed;

	/// <summary>
	/// Min speed the character moves through level.
	/// </summary>
	public float minFlySpeed;

	/// <summary>
	/// Max speed the character moves through level.
	/// </summary>
	public float maxFlySpeed;

	/// <summary>
	/// The speed the character moves up and down on screen.
	/// </summary>
	public float boostSpeed;

	///////////////
	/// REWARDS ///
	///////////////

	/// <summary>
	/// Number of abstract points to reward for killing this ship.
	/// </summary>
	public int scoreForKill;

	/////////////////
	/// EQUIPMENT ///
	/////////////////

	public EquipmentList equipmentList;
}

[System.Serializable]
public class EquipmentList
{
	/// <summary>
	/// Undetrlay of ship to show in EMI 
	/// </summary>
	public Texture underlay;

	/// <summary>
	/// List of slots where weapons can be mounted.
	/// </summary>
	public List<WeaponSlot> weaponSlots;

	/// <summary>
	/// List of slots where hull equipment can be mounted.
	/// </summary>
	public List<HullSlot> hullSlots;

	/// <summary>
	/// List of slots where engines can be mounted.
	/// </summary>
	public List<EngineSlot> engineSlots;

	/// <summary>
	/// List of slots where other equipment can be mounted.
	/// </summary>
	public List<MiscSlot> miscSlots;
	
	/// <summary>
	/// Returns all slots in one collection. 
	/// </summary>
	/// <returns>
	/// A <see cref="List<TypicalSlot>"/> all current slots.
	/// </returns>
	public List<TypicalSlot> GetAllSlotsAndRefresh ()
	{
		List<TypicalSlot> allSlots = new List<TypicalSlot> ();
		foreach (WeaponSlot slot in weaponSlots) {
			allSlots.Add (slot);
		}
		foreach (HullSlot slot in hullSlots) {
			allSlots.Add (slot);
		}
		foreach (EngineSlot slot in engineSlots) {
			allSlots.Add (slot);
		}
		foreach (MiscSlot slot in miscSlots) {
			allSlots.Add (slot);
		}
		return allSlots;
	}
}
