using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

[AddComponentMenu("Menu Components/Editor Menu")]

public class EditorMenu : MonoBehaviour
{
	#region Singletone
	private static EditorMenu _instance;
	
	public static EditorMenu Instance {
		get {
			return _instance;
		}
		set {
			_instance = value;
		}
	}
	#endregion
	
	public UIPanel area_ammoPanel,
	area_equipmentPanel,
	area_levelPanel,
	area_menuPanel,
	area_shipPanel,
	area_dialogSelectorPanel;
	public AmmoSaveCollection ammoSaveCollection;
	public EquipmentSaveCollection equipmentSaveCollection;
	public ShipsSaveCollection shipSaveCollection;
	public LevelsSaveCollection levelSaveCollection;
	public ResourceManager resourceManager;
	public GameObject TableItemPrefab;
	public UIAtlas iconAtlas,
	shipSpritesAtlas,
	bulletSpriteAtlas;
	
	void Awake ()
	{
		EditorMenu._instance = this;
		resourceManager = ResourceManager.Instance;
		string storageDir;
#if UNITY_IPHONE && !UNITY_EDITOR
		storageDir = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Game Editor Resources/");
#elif UNITY_EDITOR
		storageDir = Path.Combine (Application.dataPath, "Resources/Game Editor Resources/");
#else
		storageDir = Path.Combine (Application.dataPath, "Game Editor Resources/");
#endif
		Debug.Log (storageDir);
		resourceManager.SetStoragePath (storageDir);
	}
	
	void Start ()
	{
		ammoSaveCollection = resourceManager.Load ("ammo.xml", typeof(AmmoSaveCollection)) as AmmoSaveCollection;
		equipmentSaveCollection = resourceManager.Load ("equipment.xml", typeof(EquipmentSaveCollection)) as EquipmentSaveCollection;
		shipSaveCollection = resourceManager.Load ("ships.xml", typeof(ShipsSaveCollection)) as ShipsSaveCollection;
		levelSaveCollection = resourceManager.Load ("levels.xml", typeof(LevelsSaveCollection)) as LevelsSaveCollection;
	}
	
	public static void FlipPanels (UIPanel source, UIPanel target)
	{
		source.transform.localPosition += new Vector3 (1000, 0, 0);
		target.transform.localPosition -= new Vector3 (1000, 0, 0);
		source.enabled = false;
		target.enabled = true;
		source.SendMessage ("PanelFlipOut", target, SendMessageOptions.DontRequireReceiver);
		target.SendMessage ("PanelFlipIn", source, SendMessageOptions.DontRequireReceiver);
	}
	
	void PanelFlipIn (UIPanel source)
	{
		if (source.Equals (area_ammoPanel)) {
		} else if (source.Equals (area_equipmentPanel)) {
			equipmentSaveCollection = resourceManager.Load ("equipment.xml", typeof(EquipmentSaveCollection)) as EquipmentSaveCollection;
		} else if (source.Equals (area_levelPanel)) {
			levelSaveCollection = resourceManager.Load ("levels.xml", typeof(LevelsSaveCollection)) as LevelsSaveCollection;
		} else if (source.Equals (area_shipPanel)) {
			shipSaveCollection = resourceManager.Load ("ships.xml", typeof(ShipsSaveCollection)) as ShipsSaveCollection;
		}
	}
	
	public void OnBackToMenuBtnClick ()
	{
		Application.LoadLevel ("MainMenu");
	}
	
	public void OnEditAmmoBtnClick ()
	{
		ShowAmmoSelectionDialog(true, ammoSaveCollection.ammo, "AmmoSelectedInDialog", gameObject, area_menuPanel);
	}
	
	public void AmmoSelectedInDialog (string name)
	{
		// Get ammo with name same as selected element (or null if there is no)
		AmmoSaveData selectedAmmo = ammoSaveCollection.ammo.Find (delegate(AmmoSaveData asd)
		{
			return asd.name == name;
		}
		);
		
		// Open AmmoEditor window filled with properties of selected ammo or blank for new
		if (selectedAmmo == null || string.IsNullOrEmpty (name)) {
			int r = UnityEngine.Random.Range (1000, 9999);
			area_ammoPanel.GetComponent<AmmoEditor> ().Load (new AmmoSaveData ("Ammo" + r));
		} else {
			area_ammoPanel.GetComponent<AmmoEditor> ().Load (selectedAmmo);
		}
		FlipPanels (area_menuPanel, area_ammoPanel);
	}
	
	public void OnEditEquipmentBtnClick ()
	{
		// Dialog element for a new equipment element
		List<ResourceDialogElement> equipmentDialogElements = new List<ResourceDialogElement> ();
		equipmentDialogElements.Add (new ResourceDialogElement ("none", "-New Equipment-", "Create blank new equipment element from a scratch"));
		
		// Other elements for existing equipment elements
		foreach (EquipmentSaveData equipmentElement in equipmentSaveCollection.equipment) {
			equipmentDialogElements.Add (new ResourceDialogElement (
				equipmentElement.icon,
				equipmentElement.name,
				"Type:" + equipmentElement.type +
				"\\nWeight: " + equipmentElement.weight + 
				"\\nAcceleration" + equipmentElement.acceleration
			));
		}
		
		// Show dialog
		area_dialogSelectorPanel.GetComponent<DialogSelector> ().
			FillTableWithElements (equipmentDialogElements, gameObject, "EquipmentSelectedInDialog");
		FlipPanels (area_menuPanel, area_dialogSelectorPanel);
	}
	
	public void EquipmentSelectedInDialog (string name)
	{
		// Get equipment with name same as selected element (or null if there is no)
		EquipmentSaveData selectedEquipment = equipmentSaveCollection.equipment.Find (delegate(EquipmentSaveData esd)
		{
			return esd.name == name;
		}
		);
		
		// Open EquipmentEditor window filled with properties of selected equipment or blank for new
		if (selectedEquipment == null || string.IsNullOrEmpty (name)) {
			int r = UnityEngine.Random.Range (1000, 9999);
			area_equipmentPanel.GetComponent<EquipmentEditor> ().Load (new EquipmentSaveData ("Equipment" + r));
		} else {
			area_equipmentPanel.GetComponent<EquipmentEditor> ().Load (selectedEquipment);
		}
		FlipPanels (area_menuPanel, area_equipmentPanel);
	}
	
	public void OnEditLevelBtnClick ()
	{
		// Dialog element for a new level
		List<ResourceDialogElement> levelDialogElements = new List<ResourceDialogElement> ();
		levelDialogElements.Add (new ResourceDialogElement ("none", "-New Level-", "Create blank new level from a scratch"));
		
		// Other elements for levels
		foreach (LevelSaveData levelElement in levelSaveCollection.levels) {
			levelDialogElements.Add (new ResourceDialogElement (
				levelElement.icon,
				levelElement.name,
				"SO's:" + levelElement.spaceObjectsInLevel.ToString () +
				"\\nvc_Distance: " + levelElement.vc_DistanceToPass.ToString () + 
				"\\nvc_Enemies: " + levelElement.vc_ShipsNumToDestroy.ToString ()
			));
		}
		
		// Show dialog
		area_dialogSelectorPanel.GetComponent<DialogSelector> ().
			FillTableWithElements (levelDialogElements, gameObject, "LevelSelectedInDialog");
		FlipPanels (area_menuPanel, area_dialogSelectorPanel);
	}
	
	public void LevelSelectedInDialog (string name)
	{
		// Get level with name same as selected element (or null if there is no)
		LevelSaveData selectedLevel = levelSaveCollection.levels.Find (delegate(LevelSaveData lsd)
		{
			return lsd.name == name;
		}
		);
		
		// Open LevelEditor window filled with properties of selected level or blank for new
		if (selectedLevel == null || string.IsNullOrEmpty (name)) {
			int r = UnityEngine.Random.Range (1000, 9999);
			area_levelPanel.GetComponent<LevelEditor> ().Load (new LevelSaveData ("Level" + r));
		} else {
			area_levelPanel.GetComponent<LevelEditor> ().Load (selectedLevel);
		}
		FlipPanels (area_menuPanel, area_levelPanel);
	}
	
	public void OnEditShipBtnClick ()
	{
		// Dialog element for a new ship
		List<ResourceDialogElement> shipDialogElements = new List<ResourceDialogElement> ();
		shipDialogElements.Add (new ResourceDialogElement ("none", "-New Ship-", "Create blank new ship from a scratch"));
		
		// Other elements for ships
		foreach (ShipSaveData shipElement in shipSaveCollection.ships) {
			shipDialogElements.Add (new ResourceDialogElement (
				shipElement.icon,
				shipElement.name,
				"HP:" + shipElement.baseHP +
				"\\nMax. Weight: " + shipElement.maxCargoWeight + 
				"\\nEngines: " + shipElement.engineSlots.Count.ToString () + 
				"\\nHulls: " + shipElement.hullSlots.Count.ToString () + 
				"\\nWeapons: " + shipElement.weaponSlots.Count.ToString () + 
				"\\nMisc: " + shipElement.miscSlots.Count.ToString ()
			));
		}
		
		// Show dialog
		area_dialogSelectorPanel.GetComponent<DialogSelector> ().
			FillTableWithElements (shipDialogElements, gameObject, "ShipSelectedInDialog");
		FlipPanels (area_menuPanel, area_dialogSelectorPanel);
	}
	
	public void ShipSelectedInDialog (string name)
	{
		// Get ship with name same as selected element (or null if there is no)
		ShipSaveData selectedShip = shipSaveCollection.ships.Find (delegate(ShipSaveData ssd)
		{
			return ssd.name == name;
		}
		);
		
		// Open ShipEditor window filled with properties of selected ship or blank for new
		if (selectedShip == null || string.IsNullOrEmpty (name)) {
			int r = UnityEngine.Random.Range (1000, 9999);
			area_shipPanel.GetComponent<ShipEditor> ().Load (new ShipSaveData ("Ship" + r));
		} else {
			area_shipPanel.GetComponent<ShipEditor> ().Load (selectedShip);
		}
		FlipPanels (area_menuPanel, area_shipPanel);
	}
	
	public void OnResetBtnClick ()
	{
		string storageDir;
#if UNITY_IPHONE && !UNITY_EDITOR
		storageDir = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Game Editor Resources/");
#elif UNITY_EDITOR
		storageDir = Path.Combine (Application.dataPath, "Resources/Game Editor Resources/");
#else
		storageDir = Path.Combine (Application.dataPath, "Game Editor Resources/");
#endif
		if (!Directory.Exists (storageDir)) {
			Directory.CreateDirectory (storageDir);
		}
		
		
		TextAsset ta = Resources.Load ("Game Editor Resources/ammo") as TextAsset;
		using (StreamWriter outfile = new StreamWriter(Path.Combine (storageDir, "ammo.xml"))) {
			outfile.Write (ta.text);
		}
		
		ta = Resources.Load ("Game Editor Resources/equipment") as TextAsset;
		using (StreamWriter outfile = new StreamWriter(Path.Combine (storageDir, "equipment.xml"))) {
			outfile.Write (ta.text);
		}
		
		ta = Resources.Load ("Game Editor Resources/levels") as TextAsset;
		using (StreamWriter outfile = new StreamWriter(Path.Combine (storageDir, "levels.xml"))) {
			outfile.Write (ta.text);
		}
		
		ta = Resources.Load ("Game Editor Resources/ships") as TextAsset;
		using (StreamWriter outfile = new StreamWriter(Path.Combine (storageDir, "ships.xml"))) {
			outfile.Write (ta.text);
		}
		
		ammoSaveCollection = resourceManager.Load ("ammo.xml", typeof(AmmoSaveCollection)) as AmmoSaveCollection;
		equipmentSaveCollection = resourceManager.Load ("equipment.xml", typeof(EquipmentSaveCollection)) as EquipmentSaveCollection;
		shipSaveCollection = resourceManager.Load ("ships.xml", typeof(ShipsSaveCollection)) as ShipsSaveCollection;
		levelSaveCollection = resourceManager.Load ("levels.xml", typeof(LevelsSaveCollection)) as LevelsSaveCollection;
	
	}
	
	public void ShowSpriteSelectionDialog (UIAtlas selectedAtlas, string recieverFunctionName, GameObject recieverGO, UIPanel swappingPanel)
	{
		List<ResourceDialogElement> iconDialogElements = new List<ResourceDialogElement> ();
		foreach (UIAtlas.Sprite sprite in selectedAtlas.spriteList) {
			iconDialogElements.Add (new ResourceDialogElement (sprite.name, sprite.name, sprite.name));
		}
		
		// Show dialog
		area_dialogSelectorPanel.GetComponent<DialogSelector> ().
			FillTableWithElements (iconDialogElements, recieverGO, recieverFunctionName);
		FlipPanels (swappingPanel, EditorMenu.Instance.area_dialogSelectorPanel);
	}
	
	public void ShowAmmoSelectionDialog (bool isToShowNew, List<AmmoSaveData> ammoCollection, string recieverFunctionName, GameObject recieverGO, UIPanel swappingPanel)
	{
		List<ResourceDialogElement> ammoDialogElements = new List<ResourceDialogElement> ();
		
		if (isToShowNew) {
			// Dialog element for a new ammo
			ammoDialogElements.Add (new ResourceDialogElement ("none", "-New Ammo-", "Create blank new ammo from a scratch"));
		}
		
		// Other elements for existing ammo
		foreach (AmmoSaveData ammo in ammoCollection) {
			ammoDialogElements.Add (new ResourceDialogElement (
				ammo.icon,
				ammo.name,
				"Damage:" + ammo.damage +
				"\\nSplash: " + ammo.splashRadius + 
				"\\nLifetime" + ammo.lifetime
			));
		}
		
		// Show dialog
		area_dialogSelectorPanel.GetComponent<DialogSelector> ().
			FillTableWithElements (ammoDialogElements, recieverGO, recieverFunctionName);
		FlipPanels (swappingPanel, area_dialogSelectorPanel);
	}
	
	/*
	string newShipName = "New Ship";

	public enum GameEditorState
	{
		GameEditorMainMenu,
		CreateNewShip,
		EditShip
	}
	Rect[] windowsSizes = { new Rect (50, 50, 200, 80) };

	GameEditorState currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
	public GameObject newShipTemplate;

	public GUISkin guiskin;

	void OnGUI ()
	{
		GUI.skin = guiskin;
		DrawMenuElements (currentGameEditorState);
	}

	void DrawMenuElements (GameEditorState gameEditorState)
	{
		switch (gameEditorState) {
		
		///////////////////
		/// MAIN SCREEN ///
		///////////////////
		case GameEditorState.GameEditorMainMenu:
			
			GUILayout.BeginVertical ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create New Ship")) {
				currentGameEditorState = EditorMenu.GameEditorState.CreateNewShip;
			}
			if (GUILayout.Button ("Edit Custom Ship")) {
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create New Ship Mesh")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			if (GUILayout.Button ("Edit Custom Ship Mesh")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create New Weapon")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			if (GUILayout.Button ("Edit Custom Weapon")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create New Bullet")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			if (GUILayout.Button ("Edit Custom Bullet")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Back")) {
				Application.LoadLevel ("MainMenu");
			}
			
			GUILayout.EndVertical ();
			break;
		
		/////////////////////
		/// NEW SHIP EDIT ///
		/////////////////////
		case GameEditorState.CreateNewShip:
			
			windowsSizes[0] = GUI.Window (0, windowsSizes[0], DrawInGameMenuWindow, "Enter new ship name");
			
			break;
		
		/////////////////
		/// SHIP EDIT ///
		/////////////////
		case GameEditorState.EditShip:
			GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
			
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			
			if (GUILayout.Button ("Cancel")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Create")) {
				currentGameEditorState = EditorMenu.GameEditorState.EditShip;
			}
			GUILayout.EndHorizontal ();
			GUILayout.EndVertical ();
			break;
		}
	}

	private void DrawInGameMenuWindow (int id)
	{
		switch (id) {
		case 0:
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Name");
			newShipName = GUILayout.TextField (newShipName, 32, GUILayout.Width (120));
			GUILayout.EndHorizontal ();
			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Cancel")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Create")) {
				currentGameEditorState = EditorMenu.GameEditorState.EditShip;
			}
			GUILayout.EndHorizontal ();
			GUI.DragWindow ();
			break;
		}
	}
	*/
}
