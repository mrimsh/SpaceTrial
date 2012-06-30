using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Level Components/Level Properties")]

public class LevelProperties : MonoBehaviour
{
	[HideInInspector]
	/// <summary>
	/// Current level game mode.
	/// </summary>
	public GameMode gameMode = GameMode.Hardcore;
	
	/// <summary>
	/// A <see cref="EquipmentDB"/> object with list of equipment that available in this level.
	/// </summary>
	public EquipmentDB availableEquipmentList;
	
	/// <summary>
	/// Time between generation of two objects.
	/// </summary>
	public float objectsGenerationFrequency;

	/// <summary>
	/// Maximal number of objects on screen.
	/// </summary>
	public int objectsInGameMax;

	/// <summary>
	/// Maximal number of background objects on screen.
	/// </summary>
	public int bgObjectsAtOneScreen;

	/// <summary>
	///	List of objects to generate at background. 
	/// </summary>
	public BackgroundObjects[] bgObjects;

	/// <summary>
	/// Types of objects to generate.
	/// </summary>
	public SpaceObject[] spaceObjects;

	/// <summary>
	/// Qeueu of types of boss ships generating after each wave.
	/// Number of bosses equals number of waves.
	/// If size = 0, then there will be only one wave, without bosses.
	/// </summary>
	public SpaceShipMotor[] bossesQueue;

	/// <summary>
	/// List of aims to achieve to pass the current level.
	/// </summary>
	public VictoryConditions levelVictoryConditions;

	public GameObject levelHelpers, leftBorder, rightBorder, spawnZone;
	public LevelGenerator levelGenerator;
	public PlayerController playerController;
	public GUIManager guiManager;
	public EquipmentCellMap equipmentCellMap;
	public Statistics statistics;

	void Start ()
	{
		if (levelHelpers == null) {
			levelHelpers = GameObject.Find ("Level Helpers");
		}
		
		if (leftBorder == null) {
			leftBorder = levelHelpers.transform.Find ("Left Border").gameObject;
		}
		if (rightBorder == null) {
			rightBorder = levelHelpers.transform.Find ("Right Border").gameObject;
		}
		if (spawnZone == null) {
			spawnZone = levelHelpers.transform.Find ("Spawn Zone").gameObject;
		}
		if (levelGenerator == null) {
			levelGenerator = GetComponent<LevelGenerator> ();
		}
		if (statistics == null) {
			statistics = GetComponent<Statistics> ();
		}
		if (playerController == null) {
			playerController = GameObject.Find ("Player Container").GetComponent<PlayerController> ();
		}
		if (guiManager == null) {
			guiManager = GameObject.Find ("GUI").GetComponent<GUIManager> ();
		}
		if (equipmentCellMap == null) {
			equipmentCellMap = guiManager.transform.Find ("GUI Camera").transform.Find ("Equipment").GetComponent<EquipmentCellMap> ();
		}
	}
}

/// <summary>
/// Game modes. Casual - simplier than a Hardcore. Thats not the difficulty! It's exactly gamemodes.
/// </summary>
public enum GameMode
{
	Casual,
	Hardcore
}

[System.Serializable]
public class VictoryConditions
{

	public DistanceVictoryCondition distanceVictoryCondition;
	public DestroyedShipsNumberCondition destroyedShipsNumberCondition;
	public DestroyedSpecificShipsNumberCondition destroyedSpecificShipsNumberCondition;
}
