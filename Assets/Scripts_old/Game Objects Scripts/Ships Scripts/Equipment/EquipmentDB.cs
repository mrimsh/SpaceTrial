using UnityEngine;
using System.Collections;

public class EquipmentDB : MonoBehaviour
{
	public TypicalWeapon[] weaponsList;
	public TypicalHull[] hullsList;
	public TypicalEngine[] enginesList;
	public TypicalMiscEquipment[] miscList;
	
	/// <summary>
	/// Returns random equipment of any type.
	/// </summary>
	/// <param name="ilvlMin">
	/// A <see cref="System.Int32"/> min level of equipment
	/// </param>
	/// <param name="ilvlMax">
	/// A <see cref="System.Int32"/> max level of equipment
	/// </param>
	/// <returns>
	/// A <see cref="EquipmentUnit"/>
	/// </returns>
	public EquipmentUnit GetRandomItem (int ilvlMin, int ilvlMax)
	{
		return null;
	}

	public TypicalWeapon GetRandomWeapon ()
	{
		return weaponsList[Random.Range(0, weaponsList.Length)];
	}

	public TypicalHull GetRandomHull ()
	{
		return hullsList[Random.Range(0, hullsList.Length)];
	}

	public TypicalEngine GetRandomEngine ()
	{
		return enginesList[Random.Range(0, enginesList.Length)];
	}

	public TypicalMiscEquipment GetRandomMisc ()
	{
		return miscList[Random.Range(0, miscList.Length)];
	}

	public TypicalWeapon GetRandomWeapon (int ilvl)
	{
		return null;
	}

	public TypicalHull GetRandomHull (int ilvl)
	{
		return null;
	}

	public TypicalEngine GetRandomEngine (int ilvl)
	{
		return null;
	}

	public TypicalMiscEquipment GetRandomMisc (int ilvl)
	{
		return null;
	}

	public TypicalWeapon GetRandomWeapon (int ilvlMin, int ilvlMax)
	{
		return null;
	}

	public TypicalHull GetRandomHull (int ilvlMin, int ilvlMax)
	{
		return null;
	}

	public TypicalEngine GetRandomEngine (int ilvlMin, int ilvlMax)
	{
		return null;
	}

	public TypicalMiscEquipment GetRandomMisc (int ilvlMin, int ilvlMax)
	{
		return null;
	}
}
