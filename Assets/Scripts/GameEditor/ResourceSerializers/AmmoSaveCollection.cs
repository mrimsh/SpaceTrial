using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
[XmlRoot("AmmoCollection")]
public class AmmoSaveCollection
{
	[XmlArray("Ammo")]
	[XmlArrayItem("AmmoSaveData")]
	public List<AmmoSaveData> ammo = new List<AmmoSaveData> ();
}

[Serializable]
public class AmmoSaveData
{
	public AmmoSaveData ()
	{
	}
	
	public AmmoSaveData (string _name)
	{
		name = _name;
	}
	
	[XmlAttribute("name")]
	public string name = "New Ammo";
	public string icon = "none";
	public string sprite = "none";
	public float damage;
	public float splashRadius;
	public float cooldownMultiplier;
	public float cooldownBonus;
	public float lifetime;
	public float speed;
	public bool isHoming;
	public bool isSelfDestruct;
}