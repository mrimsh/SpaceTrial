using UnityEngine;
using System.Collections;

[AddComponentMenu("Ships Components/SpaceShipMotor")]

public class SpaceShipMotor_old : SpaceObject
{
	LevelProperties levelProperties;
	[HideInInspector]
	public Vector3 direction;
	[HideInInspector]
	public GameObject shipMeshContainer;
	[HideInInspector]
	public float currentMaxHP, currentMaxSP, currentMaxEP, currentHP, currentSP, currentEP, currentEPRegen, currentFlySpeed;
	public ShipProperties properties;
	private bool isAutoFire = false;

	// Use this for initialization
	void Start ()
	{
		if (levelProperties == null) {
			levelProperties = GameObject.Find ("GameManager").GetComponent<LevelProperties> ();
		}
		
		foreach (TypicalSlot slot in properties.equipmentList.engineSlots) {
			slot.shipParent = this;
		}
		foreach (TypicalSlot slot in properties.equipmentList.hullSlots) {
			slot.shipParent = this;
		}
		foreach (TypicalSlot slot in properties.equipmentList.miscSlots) {
			slot.shipParent = this;
		}
		foreach (TypicalSlot slot in properties.equipmentList.weaponSlots) {
			slot.shipParent = this;
		}
		
		InitSpaceShip ();
	}

	public virtual void InitSpaceShip ()
	{
		currentMaxHP = CalculateMaxHP ();
		currentHP = currentMaxHP;
		currentMaxEP = CalculateMaxEP ();
		currentEP = currentMaxEP;
		currentEPRegen = CalculateCurrentEPRegen ();
		currentMaxSP = CalculateMaxSP ();
		
		currentFlySpeed = properties.minFlySpeed;
	}

	public float CalculateMaxHP ()
	{
		float summaryHP = properties.shipHullPoints;
		foreach (HullSlot slot in properties.equipmentList.hullSlots) {
			if (slot.mountedEquipmentUnit != null) {
				summaryHP += slot.mountedEquipmentUnit.equipmentProperties.hullPointsBonus;
			}
		}
		return summaryHP;
	}

	public float CalculateMaxSP ()
	{
		float summarySP = 0;
		foreach (HullSlot slot in properties.equipmentList.hullSlots) {
			if (slot.mountedEquipmentUnit != null) {
				summarySP += slot.mountedEquipmentUnit.equipmentProperties.shieldCapacity;
			}
		}
		return summarySP;
	}

	public float CalculateMaxEP ()
	{
		float epSummary = 0;
		foreach (TypicalSlot slot in properties.equipmentList.miscSlots) {
			if (slot.mountedEquipmentUnit != null) {
				epSummary += slot.mountedEquipmentUnit.equipmentProperties.energyCapacity;
			}
		}
		return epSummary;
	}

	public float CalculateCurrentEPRegen ()
	{
		float epRegenSummary = 0;
		foreach (TypicalSlot slot in properties.equipmentList.miscSlots) {
			if (slot.mountedEquipmentUnit != null) {
				epRegenSummary += slot.mountedEquipmentUnit.equipmentProperties.energyRegen;
			}
		}
		return epRegenSummary;
	}

	// Update is called once per frame
	void Update ()
	{
		currentEP += currentEPRegen * Time.deltaTime;
		if (currentEP > currentMaxEP) {
			currentEP = currentMaxEP;
		}
		
		rigidbody.velocity = Vector3.zero;
		if (isAutoFire) {
			Fire ();
		}
	}

	void FixedUpdate ()
	{
		rigidbody.AddForce (direction.x * properties.strafeMaxSpeed, -currentFlySpeed * transform.up.y, 0, ForceMode.VelocityChange);
	}

	public void ChangeFlySpeed (float value)
	{
		currentFlySpeed += value;
		checkFlySpeed ();
	}

	public void SetFlySpeed (float value)
	{
		currentFlySpeed = value;
		checkFlySpeed ();
	}

	private void checkFlySpeed ()
	{
		if (currentFlySpeed > properties.maxFlySpeed) {
			currentFlySpeed = properties.maxFlySpeed;
		} else if (currentFlySpeed < properties.minFlySpeed) {
			currentFlySpeed = properties.minFlySpeed;
		}
	}
	/*
	public override void CatchBullet (BulletProperties bulletProperties)
	{
		if (bulletProperties.master == levelProperties.playerController.shipMotor) {
			DamageShip (bulletProperties.baseDamage, DamageSource.Player);
		} else if (bulletProperties.master == levelProperties.playerController.shipMotor) {
			DamageShip (bulletProperties.baseDamage, DamageSource.Enviroment);
		}
	}*/

	public void DamageShip (float damageAmount, DamageSource source)
	{
		foreach (HullSlot slot in properties.equipmentList.hullSlots) {
			if (slot.mountedEquipmentUnit != null) {
				((TypicalHull)slot.mountedEquipmentUnit).ResetShieldRegenTimeStamp ();
			}
		}
		if (currentSP > 0) {
			if (damageAmount < currentSP) {
				currentSP -= damageAmount;
				damageAmount = 0;
			} else {      
				damageAmount -= currentSP;
				currentSP = 0;
			}
		}
		currentHP -= damageAmount;
		if (currentHP <= 0) {
			transform.parent.GetComponent<ShipController> ().DestroyShip (source);
		}
	}

	internal void Fire ()
	{
		foreach (WeaponSlot weaponSlot in properties.equipmentList.weaponSlots) {
			weaponSlot.Fire (false);
		}
	}

	internal void SwitchAutoFire ()
	{
		isAutoFire = !isAutoFire;
	}
}
