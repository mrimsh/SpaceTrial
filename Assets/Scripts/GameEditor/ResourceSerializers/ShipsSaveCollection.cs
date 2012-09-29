using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
[XmlRoot("ShipsCollection")]
public class ShipsSaveCollection
{
	[XmlArray("Ships")]
	[XmlArrayItem("ShipSaveData")]
	public List<ShipSaveData> ships = new List<ShipSaveData> ();
}

[Serializable]
public class ShipSaveData : ICloneable
{
	public ShipSaveData ()
	{
	}
	
	public ShipSaveData (string _name)
	{
		name = _name;
		hullSlots.Add (Vector2.zero);
		engineSlots.Add (Vector2.zero);
		weaponSlots.Add (Vector2.zero);
		miscSlots.Add (Vector2.zero);
	}
	
	[XmlAttribute("name")]
	public string name = "New Ship";
	public string icon = "none";
	public string sprite = "none";
	public float maxCargoWeight;
	public float baseHP;
	public float baseEP;
	public float baseEPRegen;
	public float baseSP;
	public float baseSPRegen;
	public float baseSPRegenDelay;
	public float award;
	public float minSpeedBonus;
	public float maxSpeedBonus;
	public float strafeBonus;
	public float accelerationBonus;
	[XmlArray("HullSlots")]
	[XmlArrayItem("Vector2")]
	public List<Vector2> hullSlots = new List<Vector2> ();
	[XmlArray("EngineSlots")]
	[XmlArrayItem("Vector2")]
	public List<Vector2> engineSlots = new List<Vector2> ();
	[XmlArray("WeaponSlots")]
	[XmlArrayItem("Vector2")]
	public List<Vector2> weaponSlots = new List<Vector2> ();
	[XmlArray("MiscSlots")]
	[XmlArrayItem("Vector2")]
	public List<Vector2> miscSlots = new List<Vector2> ();

	#region ICloneable implementation
	public object Clone ()
	{
		ShipSaveData ret = new ShipSaveData ();
		
		ret.icon = icon;
		ret.name = name;
		ret.accelerationBonus = accelerationBonus;
		ret.award = award;
		ret.baseHP = baseHP;
		ret.baseEP = baseEP;
		ret.baseEPRegen = baseEPRegen;
		ret.baseSP = baseSP;
		ret.baseSPRegen = baseSPRegen;
		ret.baseSPRegenDelay = baseSPRegenDelay;
		ret.maxCargoWeight = maxCargoWeight;
		ret.maxSpeedBonus = maxSpeedBonus;
		ret.minSpeedBonus = minSpeedBonus;
		ret.sprite = sprite;
		ret.strafeBonus = strafeBonus;
		ret.engineSlots = new List<UnityEngine.Vector2> (engineSlots);
		ret.hullSlots = new List<UnityEngine.Vector2> (hullSlots);
		ret.weaponSlots = new List<UnityEngine.Vector2> (weaponSlots);
		ret.miscSlots = new List<UnityEngine.Vector2> (miscSlots);
		
		return ret;
	}
	#endregion
}