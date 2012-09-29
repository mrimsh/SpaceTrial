using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class MainMenu : MonoBehaviour
{
	public GameObject midSceneDataPrefab;
	public GameObject area_main,
	area_newGame;
	public UIPopupList poplst_levels,
	poplst_ship;
	LevelsSaveCollection levelSaveCollection;
	ShipsSaveCollection shipSaveCollection;
	EquipmentSaveCollection equipmentSaveCollection;
	AmmoSaveCollection ammoSaveCollection;

	// Use this for initialization
	void Start ()
	{
		if (GameObject.Find ("MidSceneData") == null) {
			Instantiate (midSceneDataPrefab).name = "MidSceneData";
		}
		
		string storageDir;
#if UNITY_IPHONE && !UNITY_EDITOR
		storageDir = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Game Editor Resources/");
#elif UNITY_EDITOR
		storageDir = Path.Combine (Application.dataPath, "Resources/Game Editor Resources/");
#else
		storageDir = Path.Combine (Application.dataPath, "Game Editor Resources/");
#endif
		ResourceManager.Instance.SetStoragePath (storageDir);
		
		levelSaveCollection = ResourceManager.Instance.Load ("levels.xml", typeof(LevelsSaveCollection)) as LevelsSaveCollection;
		shipSaveCollection = ResourceManager.Instance.Load ("ships.xml", typeof(ShipsSaveCollection)) as ShipsSaveCollection;
		equipmentSaveCollection = ResourceManager.Instance.Load ("equipment.xml", typeof(EquipmentSaveCollection)) as EquipmentSaveCollection;
		ammoSaveCollection = ResourceManager.Instance.Load ("ammo.xml", typeof(AmmoSaveCollection)) as AmmoSaveCollection;
		
		if (levelSaveCollection.levels.Count == 0 || shipSaveCollection.ships.Count == 0) {
			Application.LoadLevel ("GameEditor");
		} else {
			poplst_levels.items.Clear ();
			for (int i = 0; i < levelSaveCollection.levels.Count; i++) {
				poplst_levels.items.Add (levelSaveCollection.levels [i].name);
			}
			poplst_levels.selection = poplst_levels.items [0];
		
			poplst_ship.items.Clear ();
			for (int i = 0; i < shipSaveCollection.ships.Count; i++) {
				poplst_ship.items.Add (shipSaveCollection.ships [i].name);
			}
			poplst_ship.selection = poplst_ship.items [0];
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnNewGameBtnClick ()
	{
		area_main.transform.localPosition = new Vector3 (1000, 0, 0);
		area_newGame.transform.localPosition = Vector3.zero;
	}
	
	void OnBackToMenuBtnClick ()
	{
		area_newGame.transform.localPosition = new Vector3 (1000, 0, 0);
		area_main.transform.localPosition = Vector3.zero;
	}
	
	void OnEditorBtnClick ()
	{
		Application.LoadLevel ("GameEditor");
	}
	
	void OnStartBtnClick ()
	{
		// Save selected level
		MidSceneData.Instance.selectedLevel = new LevelSaveData ();
		LevelSaveData selectedLevelFromCollection = levelSaveCollection.levels.Find (delegate(LevelSaveData lsd)
		{
			return lsd.name == poplst_levels.selection;
		});
		MidSceneData.Instance.selectedLevel = selectedLevelFromCollection.Clone () as LevelSaveData;
		// Save used in level ships and selected initial ship
		MidSceneData.Instance.shipsInLevel = shipSaveCollection;
		MidSceneData.Instance.selectedInitialShip = poplst_ship.selection;
		// Save used in level equipment
		MidSceneData.Instance.equipmentInLevel = equipmentSaveCollection;
		// Save used in level bullets
		MidSceneData.Instance.ammoInLevel = ammoSaveCollection;
		
		// Start Game Level
		Application.LoadLevel ("Game");
	}
}




