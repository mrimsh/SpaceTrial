using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
[XmlRoot("EquipmentCollection")]
public class EquipmentSaveCollection
{
	
	[XmlArray("Equipment")]
	[XmlArrayItem("EquipmentSaveData")]
	public List<EquipmentSaveData> equipment = new List<EquipmentSaveData> ();
}

[Serializable]
public class EquipmentSaveData : ICloneable
{
	public EquipmentSaveData ()
	{
	}
	
	public EquipmentSaveData (string _name)
	{
		name = _name;
		ammo.Add ("none");
	}
	
	[XmlAttribute("name")]
	public string name = "New Equipment";
	public string icon = "none";
	public EquipmentType type = EquipmentType.Misc;
	public float weight;
	public float ep;
	public float epRegen;
	public float hp;
	public float hpRegen;
	public float sp;
	public float spRegen;
	public float spRegenDelay;
	public float minSpeed;
	public float maxSpeed;
	public float strafe;
	public float acceleration;
	public float damageMultiplier;
	public float damageBonus;
	public float cooldown;
	public float energyCost;
	public List<String> ammo = new List<String> ();

	#region ICloneable implementation
	public object Clone ()
	{
		EquipmentSaveData ret = new EquipmentSaveData ();
		
		ret.icon = icon;
		ret.name = name;
		ret.acceleration = acceleration;
		ret.ammo = new List<string> (ammo);
		ret.cooldown = cooldown;
		ret.damageBonus = damageBonus;
		ret.damageMultiplier = damageMultiplier;
		ret.energyCost = energyCost;
		ret.ep = ep;
		ret.epRegen = epRegen;
		ret.hp = hp;
		ret.hpRegen = hpRegen;
		ret.maxSpeed = maxSpeed;
		ret.minSpeed = minSpeed;
		ret.sp = sp;
		ret.spRegen = spRegen;
		ret.spRegenDelay = spRegenDelay;
		ret.strafe = strafe;
		ret.type = type;
		ret.weight = weight;
		
		return ret;
	}
	#endregion
}

public enum EquipmentType
{
	Hull = 0,
	Engine = 1,
	Weapon = 2,
	Misc = 3
}