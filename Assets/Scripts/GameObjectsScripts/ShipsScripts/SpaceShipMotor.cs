using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpaceShipMotor : SpaceObject
{
	public ShipSaveData ShipOriginData {
		set {
			_shipOriginData = value;
			Sprite.spriteName = "none";
			Sprite.spriteName = value.sprite;
			
			maxHP = _shipOriginData.baseHP;
			maxEP = _shipOriginData.baseEP;
			maxSP = _shipOriginData.baseSP;
			minSpeed = _shipOriginData.minSpeedBonus;
			maxSpeed = _shipOriginData.maxSpeedBonus;
			acceleration = _shipOriginData.accelerationBonus;
			strafe = _shipOriginData.strafeBonus;
			
			engineSlots.Clear ();
			hullSlots.Clear ();
			weaponSlots.Clear ();
			miscSlots.Clear ();
			
			for (int i = 0; i < _shipOriginData.engineSlots.Count; i++) {
				engineSlots.Add (new EquipmentSlot (this));
			}
			for (int i = 0; i < _shipOriginData.hullSlots.Count; i++) {
				hullSlots.Add (new EquipmentSlot (this));
			}
			for (int i = 0; i < _shipOriginData.weaponSlots.Count; i++) {
				weaponSlots.Add (new EquipmentSlot (this));
			}
			for (int i = 0; i < _shipOriginData.miscSlots.Count; i++) {
				miscSlots.Add (new EquipmentSlot (this));
			}
			
			FillShipWithRandomEquipment (this);
			CurrentHP = maxHP;
			CurrentSP = maxSP;
			CurrentEP = maxEP;
			_currentSpeed = minSpeed;
		}
		get {
			return _shipOriginData;
		}
	}

	public UISprite Sprite {
		get {
			if (_sprite == null) {
				_sprite = transform.Find ("sprite").GetComponent<UISprite> ();
			}
			return _sprite;
		}
	}

	public float CurrentSpeed {
		get {
			return _currentSpeed;
		}
		set {
			if (value < minSpeed) {
				_currentSpeed = minSpeed;
			} else if (value > maxSpeed) {
				_currentSpeed = maxSpeed;
			} else {
				_currentSpeed = value;
			}
		}
	}

	public float TargetSpeed {
		get {
			return _targetSpeed;
		}
		set {
			if (value < minSpeed) {
				_targetSpeed = minSpeed;
			} else if (value > maxSpeed) {
				_targetSpeed = maxSpeed;
			} else {
				_targetSpeed = value;
			}
		}
	}
	
	public float CurrentHP {
		get {
			return _currentHP;
		}
		set {
			if (value <= 0) {
				_currentHP = 0;			
				Selfdestruct (lastDamageSource);
			} else if (value > maxHP) {
				_currentHP = maxHP;
			} else {
				_currentHP = value;
			}
		}
	}
	
	public float CurrentEP {
		get {
			return _currentEP;
		}
		set {
			if (value < 0) {
				_currentEP = 0;
			} else if (value > maxEP) {
				_currentEP = maxEP;
			} else {
				_currentEP = value;
			}
		}
	}
	
	public float CurrentSP {
		get {
			return _currentSP;
		}
		set {
			if (value < 0) {
				_currentSP = 0;
			} else if (value > maxSP) {
				_currentSP = maxSP;
			} else {
				_currentSP = value;
			}
		}
	}
	
	public float minSpeed, maxSpeed, acceleration, maxHP, maxSP, maxEP, epRegen, spRegen, spRegenDelay, strafe;
	public List<EquipmentSlot> engineSlots = new List<EquipmentSlot> ();
	public List<EquipmentSlot> hullSlots = new List<EquipmentSlot> ();
	public List<EquipmentSlot> weaponSlots = new List<EquipmentSlot> ();
	public List<EquipmentSlot> miscSlots = new List<EquipmentSlot> ();
	public bool isAutoFire;
	private UISprite _sprite;
	private ShipSaveData _shipOriginData;
	private bool _isShipActive;
	public float _currentSpeed,
	_currentHP, 
	_currentEP, 
	_currentSP,
	_targetSpeed;
	private float lastTimeWasAttacked;
	private DamageSource lastDamageSource;
	
	// Update is called once per frame
	void Update ()
	{
		if (_isShipActive) {
			// Change speed
			if (TargetSpeed != CurrentSpeed) {
				float speedDelta = acceleration * Time.deltaTime * 5f;
				if (Mathf.Abs (TargetSpeed - CurrentSpeed) < speedDelta) {
					CurrentSpeed = TargetSpeed;
				} else {
					if (TargetSpeed < CurrentSpeed) {
						CurrentSpeed -= speedDelta;
					} else if (TargetSpeed > CurrentSpeed) {
						CurrentSpeed += speedDelta;
					}
				}
			}
			// Move forward
			transform.localPosition += transform.localRotation * new Vector3 (0, CurrentSpeed * Time.deltaTime, 0);
			// Energy regen
			if (CurrentEP < maxEP) {
				CurrentEP += epRegen * Time.deltaTime;
			}
			// Shields regen
			if (CurrentSP < maxSP) {
				if (lastTimeWasAttacked + spRegenDelay < Time.time) {
					CurrentSP += spRegen * Time.deltaTime;
				}
			}
			// Auto-attack
			if (isAutoFire) {
				for (int i = 0; i < weaponSlots.Count; i++) {
					weaponSlots [i].Use ();
				}
			}
			// Is ship moved out of screen border
			if (transform.localPosition.x > GameManager.SCREENWIDTH * 0.5f) {
				transform.localPosition = new Vector3 (-GameManager.SCREENWIDTH * 0.5f, transform.localPosition.y, transform.localPosition.z);
			} else if (transform.localPosition.x < -GameManager.SCREENWIDTH * 0.5f) {
				transform.localPosition = new Vector3 (GameManager.SCREENWIDTH * 0.5f, transform.localPosition.y, transform.localPosition.z);
			}
		}
	}
	
	public void ActivateShip (string shipOriginDataName)
	{
		ShipOriginData = MidSceneData.Instance.shipsInLevel.ships.Find (delegate(ShipSaveData ssd)
		{
			return ssd.name == shipOriginDataName;
		});
		
		_sprite.depth = Random.Range (0, 100);
		
		_isShipActive = true;
	}
	
	/// <summary>
	/// Fills the ship with random equipment.
	/// </summary>
	/// <param name='ship'>
	/// Ship to mount with equipment.
	/// </param>
	public void FillShipWithRandomEquipment (SpaceShipMotor ship)
	{
		for (int i = 0; i < engineSlots.Count; i++) {
			List<EquipmentSaveData> engineEquipment = MidSceneData.Instance.equipmentInLevel.equipment.FindAll (delegate(EquipmentSaveData esd)
			{
				return esd.type == EquipmentType.Engine;
			});
			engineSlots [i].MountedEquipment = engineEquipment [Random.Range (0, engineEquipment.Count)];
		}
		for (int i = 0; i < hullSlots.Count; i++) {
			List<EquipmentSaveData> hullEquipment = MidSceneData.Instance.equipmentInLevel.equipment.FindAll (delegate(EquipmentSaveData esd)
			{
				return esd.type == EquipmentType.Hull;
			});
			hullSlots [i].MountedEquipment = hullEquipment [Random.Range (0, hullEquipment.Count)];
		}
		for (int i = 0; i < weaponSlots.Count; i++) {
			List<EquipmentSaveData> weaponEquipment = MidSceneData.Instance.equipmentInLevel.equipment.FindAll (delegate(EquipmentSaveData esd)
			{
				return esd.type == EquipmentType.Weapon;
			});
			weaponSlots [i].MountedEquipment = weaponEquipment [Random.Range (0, weaponEquipment.Count)];
		}
		for (int i = 0; i < miscSlots.Count; i++) {
			List<EquipmentSaveData> miscEquipment = MidSceneData.Instance.equipmentInLevel.equipment.FindAll (delegate(EquipmentSaveData esd)
			{
				return esd.type == EquipmentType.Misc;
			});
			miscSlots [i].MountedEquipment = miscEquipment [Random.Range (0, miscEquipment.Count)];
		}
	}
	
	void BulletWasCollided (BulletMotor bullet)
	{
		SpaceShipMotor playerShip = gm.playerShip;
		if (bullet.sourceShip == playerShip && this != playerShip) {
			DamageShip (bullet.damage, DamageSource.Player);
			bullet.BulletCatched ();
		} else if (bullet.sourceShip != playerShip && this == playerShip) {
			DamageShip (bullet.damage, DamageSource.Player);
			bullet.BulletCatched ();
		}
	}

	public void DamageShip (float damageAmount, DamageSource source)
	{
		DamageShip (damageAmount, source, false);
	}
	
	public void DamageShip (float damageAmount, DamageSource source, bool ignoreShield)
	{	
		if (!ignoreShield) {
			if (CurrentSP > 0) {
				if (damageAmount < CurrentSP) {
					CurrentSP -= damageAmount;
					damageAmount = 0;
				} else {      
					damageAmount -= CurrentSP;
					CurrentSP = 0;
				}
			}
		}
		
		CurrentHP -= damageAmount;
		lastTimeWasAttacked = Time.time;
		lastDamageSource = source;
	}

	public override void OnTriggerStay (Collider otherCollider)
	{
		// Send messages to all rigidbodies, that was collided
		otherCollider.SendMessage ("SOCollided", this, SendMessageOptions.DontRequireReceiver);
	}
	
	public override void SOCollided (SpaceObject collidedObject)
	{
		if (this == gm.playerShip) {
			DamageShip (maxHP * 0.1f, DamageSource.Enviroment, true);
		} else if (collidedObject == gm.playerShip) {
			Selfdestruct (DamageSource.Player);
		}
	}
	
	public override void Selfdestruct (DamageSource source)
	{
		SendMessage ("ShipDestroyed", source, SendMessageOptions.DontRequireReceiver);
		Destroy (gameObject);
	}
}
