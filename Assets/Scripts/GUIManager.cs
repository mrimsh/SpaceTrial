using UnityEngine;
using System.Collections;

[AddComponentMenu("Menu Components/GUI Manager")]

public class GUIManager : MonoBehaviour
{

	private GUIText flySpeed;
	public LevelProperties levelProperties;
	public Transform healthBar, energyBar, shieldBar;
	public GUISkin skin;

	/// <summary>
	/// Game Interface Elements
	/// </summary>
	[HideInInspector]
	public bool isToShowGIE = true;

	/// <summary>
	/// Equipment Management Interface
	/// </summary>
	[HideInInspector]
	public bool isToShowEMI;

	/// <summary>
	/// Shows or gides statistics windows.
	/// </summary>
	//[HideInInspector]
	public bool isToShowStatistics = false;
	Rect[] windowsSizes = { new Rect (0, 0, 426, 640) };
	Rect statisticsWindowSize = new Rect (30, 50, 300, 580);

	// Use this for initialization
	void Start ()
	{
		if (levelProperties == null) {
			levelProperties = GameObject.Find ("GameManager").GetComponent<LevelProperties> ();
		}
		flySpeed = transform.Find ("Fly Speed").GetComponent<GUIText> ();
	}

	// Update is called once per WaitForEndOfFrame
	void Update ()
	{
		flySpeed.text = levelProperties.playerController.shipMotor.currentFlySpeed.ToString ();
		SetHealth (levelProperties.playerController.shipMotor.currentHP == 0 ? 0 : levelProperties.playerController.shipMotor.currentHP / levelProperties.playerController.shipMotor.currentMaxHP * 100);
		SetEnergy (levelProperties.playerController.shipMotor.currentMaxEP == 0 ? 0 : levelProperties.playerController.shipMotor.currentEP / levelProperties.playerController.shipMotor.currentMaxEP * 100);
		SetShield (levelProperties.playerController.shipMotor.currentMaxSP == 0 ? 0 : levelProperties.playerController.shipMotor.currentSP / levelProperties.playerController.shipMotor.currentMaxSP * 100);
	}

	void OnGUI ()
	{
		GUI.skin = skin;
		
		if (isToShowGIE) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Menu (Esc)")) {
				Application.LoadLevel ("MainMenu");
			}
			if (GUILayout.Button ("EMI (Tab)")) {
				//levelProperties.equipmentCellMap.ToggleEquipmentWindowVisibility ();
				isToShowEMI = true;
				isToShowGIE = false;
			}	
			if (GUILayout.Button ("Stats (F1)")) {
				isToShowStatistics = !isToShowStatistics;
			}
			GUILayout.EndHorizontal ();
		}
		
		if (isToShowEMI) {
			GUI.Window (0, windowsSizes [0], DrawInGameMenuWindow, "Equipment Management", "STWindow");
			
		}
		
		if (isToShowStatistics) {
			statisticsWindowSize = GUI.Window (1, statisticsWindowSize, DrawInGameMenuWindow, "Level Statistics");
		}
	}

	private void DrawInGameMenuWindow (int id)
	{
		switch (id) {
		case 0:
			Texture i = levelProperties.playerController.shipMotor.properties.equipmentList.underlay;
			GUI.DrawTexture (new Rect ((426 - i.width) * 0.5f, (707 - i.height) * 0.5f, i.width, i.height), i);
			foreach (TypicalSlot slot in levelProperties.playerController.shipMotor.properties.equipmentList.weaponSlots) {
				if (GUI.Button (new Rect (slot.iconPosition.x - 45 * 0.5f, slot.iconPosition.y, 45, 45), "", skin.GetStyle ("EquipmentCell"))) {
					//OpenEquipmentSelectionDialog();
					Debug.Log (slot.type.ToString ());
				}
			}

			foreach (TypicalSlot slot in levelProperties.playerController.shipMotor.properties.equipmentList.hullSlots) {
				if (GUI.Button (new Rect (slot.iconPosition.x - 45 * 0.5f, slot.iconPosition.y, 45, 45), "", skin.GetStyle ("EquipmentCell"))) {
					Debug.Log (slot.type.ToString ());
				}
			}

			foreach (TypicalSlot slot in levelProperties.playerController.shipMotor.properties.equipmentList.engineSlots) {
				if (GUI.Button (new Rect (slot.iconPosition.x - 45 * 0.5f, slot.iconPosition.y, 45, 45), "", skin.GetStyle ("EquipmentCell"))) {
					Debug.Log (slot.type.ToString ());
				}
			}

			foreach (TypicalSlot slot in levelProperties.playerController.shipMotor.properties.equipmentList.miscSlots) {
				if (GUI.Button (new Rect (slot.iconPosition.x - 45 * 0.5f, slot.iconPosition.y, 45, 45), "", skin.GetStyle ("EquipmentCell"))) {
					Debug.Log (slot.type.ToString ());
				}
			}
			if (GUI.Button (new Rect (27, 582, 150, 30), "Previous", skin.GetStyle ("Button2"))) {
				Debug.Log ("dsf");
			}
			if (GUI.Button (new Rect (177, 582, 70, 30), "OK", skin.GetStyle ("Button2"))) {
				isToShowEMI = false;
				isToShowGIE = true;
			}
			if (GUI.Button (new Rect (247, 582, 150, 30), "Next", skin.GetStyle ("Button2"))) {
				Debug.Log ("dsf");
			}
			break;
		case 1:
			GUILayout.BeginHorizontal ();
			switch (levelProperties.levelGenerator.CurrentGameState) {
			case GameState.Game:
				GUILayout.Label ("GAME IN PROGRESS.");
				break;
			case GameState.Lose:
				GUILayout.Label ("FAILURE...");
				break;
			case GameState.Win:
				GUILayout.Label ("VICTORY!!!");
				break;
			}

			
			GUILayout.EndHorizontal ();
			
			GUILayout.Space (30f);
			GUILayout.Label ("Level Aims");
			
			GUILayout.BeginHorizontal ();
			
			GUILayout.BeginVertical ();
			GUILayout.Label ("Name", "Label2");
			GUILayout.Label ("Ships destroyed", "Label2");
			GUILayout.Label ("Types to kill", "Label2");
			GUILayout.Label ("Distance passed", "Label2");
			GUILayout.EndVertical ();
			
			GUILayout.BeginVertical ();
			GUILayout.Label ("Aim", "Label2");
			GUILayout.Label (levelProperties.levelVictoryConditions.destroyedShipsNumberCondition.shipsToDestroy.ToString (), "Label2");
			GUILayout.Label (levelProperties.levelVictoryConditions.destroyedSpecificShipsNumberCondition.shipTypesToDestroyData.Length.ToString () + "types", "Label2");
			GUILayout.Label (levelProperties.levelVictoryConditions.distanceVictoryCondition.distanceToAchieve.ToString (), "Label2");
			GUILayout.EndVertical ();
			
			GUILayout.BeginVertical ();
			GUILayout.Label ("Passed?", "Label2");
			GUILayout.Label (levelProperties.levelVictoryConditions.destroyedShipsNumberCondition.CheckVictoryCondition ().ToString (), "Label2");
			GUILayout.Label (levelProperties.levelVictoryConditions.destroyedSpecificShipsNumberCondition.CheckVictoryCondition ().ToString (), "Label2");
			GUILayout.Label (levelProperties.levelVictoryConditions.distanceVictoryCondition.CheckVictoryCondition ().ToString (), "Label2");
			GUILayout.EndVertical ();
			
			GUILayout.EndHorizontal ();
			
			GUILayout.Space (30f);
			GUILayout.Label ("Level Statistics");
			
			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();
			GUILayout.Label ("Name", "Label2");
			GUILayout.Label ("Ships destroyed", "Label2");
			GUILayout.Label ("Distance passed", "Label2");
			GUILayout.EndVertical ();
			
			GUILayout.BeginVertical ();
			GUILayout.Label ("Result", "Label2");
			GUILayout.Label (levelProperties.statistics.numberOfDestroyedShips.ToString (), "Label2");
			GUILayout.Label (((int)levelProperties.statistics.playerDistanceMoved).ToString (), "Label2");
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
			
			GUILayout.Space (15f);
			GUILayout.Label ("Destroy list");
			
			GUILayout.BeginHorizontal ();
			
			GUILayout.BeginVertical ();
			foreach (ShipsStatistics ship in levelProperties.statistics.ShipsStatistics) {
				GUILayout.Label (ship.name, "Label2");
			}

			
			GUILayout.EndVertical ();
			
			GUILayout.BeginVertical ();
			foreach (ShipsStatistics ship in levelProperties.statistics.ShipsStatistics) {
				GUILayout.Label (ship.quantity.ToString (), "Label2");
			}

			
			GUILayout.EndVertical ();
			
			GUILayout.EndHorizontal ();
			
			GUILayout.FlexibleSpace ();
			
			GUILayout.BeginHorizontal ();
			if (!levelProperties.statistics.IsLevelPassed ()) {
				if (GUILayout.Button ("Return (F1)")) {
					isToShowStatistics = false;
				}
			}
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Quit (Esc)")) {
				Application.LoadLevel ("MainMenu");
			}
			GUILayout.EndHorizontal ();
			break;
		}
		GUI.DragWindow ();
	}

	public void SetHealth (float percentage)
	{
		healthBar.localScale = new Vector3 (percentage * 0.13f, 1, 0.5f);
	}

	public void SetEnergy (float percentage)
	{
		energyBar.localScale = new Vector3 (percentage * 0.13f, 1, 0.5f);
	}

	public void SetShield (float percentage)
	{
		shieldBar.localScale = new Vector3 (percentage * 0.13f, 1, 0.5f);
	}
}
