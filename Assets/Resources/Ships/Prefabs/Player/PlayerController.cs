using UnityEngine;
using System.Collections;

[AddComponentMenu("Ships Components/Player Controller")]

public class PlayerController : ShipController
{

	public string shipName;
	public float viewOffset;

	[HideInInspector]
	public GameObject ship;
	[HideInInspector]
	public SpaceShipMotor shipMotor;

	private GameObject view;
	
	
	
	// Use this for initialization
	void Start ()
	{
		if (levelProperties == null) {
			levelProperties = GameObject.Find ("GameManager").GetComponent<LevelProperties> ();
		}
		
		ShipAssembler shipAssembler = gameObject.AddComponent<ShipAssembler> ();
		shipMotor = shipAssembler.AssembleShip (shipName, shipName);
		ship = shipMotor.gameObject;
		
		ship.transform.Rotate (0, 0, 180);
		
		view = transform.Find ("View").gameObject;
		
		/*
		foreach (WeaponSlot slot in shipMotor.properties.equipmentList.weaponSlots) {
			((WeaponSlot)levelProperties.equipmentCellMap.AddEquipmentCell (slot).linkToSlot).MountWeapon ((TypicalWeapon)levelProperties.availableEquipmentList.weaponsList[0]);
		}
		*/
		foreach (WeaponSlot slot in shipMotor.properties.equipmentList.weaponSlots) {
			slot.MountEquipment (levelProperties.availableEquipmentList.weaponsList[0]);
		}
		shipMotor.properties.equipmentList.hullSlots[0].MountEquipment (levelProperties.availableEquipmentList.hullsList[1]);
		shipMotor.properties.equipmentList.engineSlots[0].MountEquipment (levelProperties.availableEquipmentList.enginesList[0]);
		shipMotor.properties.equipmentList.miscSlots[0].MountEquipment (levelProperties.availableEquipmentList.miscList[0]);
		shipMotor.properties.equipmentList.miscSlots[1].MountEquipment (levelProperties.availableEquipmentList.miscList[1]);
		
		levelProperties.equipmentCellMap.ToggleEquipmentWindowVisibility ();
		levelProperties.equipmentCellMap.ToggleEquipmentWindowVisibility ();
	}

	// Update is called once per frame
	void Update ()
	{
		
		//Debug.Log ("currentHP: " + shipMotor.currentHP + "; currentEP: " + shipMotor.currentEP + "; currentEPRegen: " + shipMotor.currentEPRegen + "; currentSP: " + shipMotor.currentSP);
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("MainMenu");
		}
		if (Input.GetButtonDown ("EMI")) {
			levelProperties.equipmentCellMap.ToggleEquipmentWindowVisibility ();
		}
		if (Input.GetButtonDown ("Statistics"))
		{
			levelProperties.guiManager.isToShowStatistics = !levelProperties.guiManager.isToShowStatistics;
		}
		
		shipMotor.direction = new Vector3 (Input.GetAxis ("Strafe"), Input.GetAxis ("Vertical"), 0);
		view.transform.position = new Vector3 (0, ship.transform.position.y + viewOffset, ship.transform.position.z);
		shipMotor.ChangeFlySpeed (Input.GetAxis ("Accelerate"));
		
		if (Input.GetButtonDown ("Fire") && Input.GetButton ("Shifts")) {
			shipMotor.SwitchAutoFire ();
		} else if (Input.GetButtonDown ("Fire")) {
			shipMotor.Fire ();
		}
		
		for (int i = 0; i < shipMotor.properties.equipmentList.weaponSlots.Count; i++) {
			if (Input.GetKeyDown ((i + 1).ToString ())) {
				shipMotor.properties.equipmentList.weaponSlots[i].Fire (true);
			}
			
			if (Input.GetButton ("Shifts")) {
				if (Input.GetKeyDown ((i + 1).ToString ()) && shipMotor.properties.equipmentList.weaponSlots[i].mountedEquipmentUnit != null) {
					((TypicalWeapon) shipMotor.properties.equipmentList.weaponSlots[i].mountedEquipmentUnit).isActive = !((TypicalWeapon) shipMotor.properties.equipmentList.weaponSlots[i].mountedEquipmentUnit).isActive;
				}
			}
		}
	}

	public override void DestroyShip (DamageSource source)
	{
		levelProperties.levelGenerator.CurrentGameState = GameState.Lose;
		base.DestroyShip (source);
	}
}
