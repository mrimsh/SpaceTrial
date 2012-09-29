using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
[XmlRoot("LevelsCollection")]
public class LevelsSaveCollection
{
	[XmlArray("Levels")]
	[XmlArrayItem("LevelSaveData")]
	public List<LevelSaveData> levels = new List<LevelSaveData> ();
}

[Serializable]
public class LevelSaveData : ICloneable
{
	public LevelSaveData ()
	{
	}
	
	public LevelSaveData (string _name)
	{
		name = _name;
		spaceObjects.Add ("");
	}
	
	[XmlAttribute("name")]
	public string name = "New Level";
	public string icon = "none";
	public int spaceObjectsInLevel;
	public int spaceObjectsAtScreen;
	public float spaceObjectsGenerationFrequency = 1f;
	public List<string> spaceObjects = new List<string> ();
	// Victory conditions
	public int vc_ShipsNumToDestroy;
	public int vc_DistanceToPass;

	#region ICloneable implementation
	/// <summary>
	/// Clone this instance.
	/// </summary>
	public object Clone ()
	{
		LevelSaveData ret = new LevelSaveData ();
		
		ret.icon = icon;
		ret.name = name;
		ret.spaceObjects = new List<string> (spaceObjects);
		ret.spaceObjectsAtScreen = spaceObjectsAtScreen;
		ret.spaceObjectsGenerationFrequency = spaceObjectsGenerationFrequency;
		ret.spaceObjectsInLevel = spaceObjectsInLevel;
		ret.vc_DistanceToPass = vc_DistanceToPass;
		ret.vc_ShipsNumToDestroy = vc_ShipsNumToDestroy;
		
		return ret;
	}
	#endregion
}