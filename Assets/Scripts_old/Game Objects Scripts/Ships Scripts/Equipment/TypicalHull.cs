using UnityEngine;
using System.Collections;

public class TypicalHull : EquipmentUnit
{
	
	private float nextShieldRegenTimeStamp;
	
	void Start ()
	{
	}
	
	void Update ()
	{
		if (nextShieldRegenTimeStamp < Time.time) {
			parentMotor.currentSP += equipmentProperties.shieldRegen * Time.deltaTime;
			if (parentMotor.currentSP > parentMotor.currentMaxSP) {
				parentMotor.currentSP = parentMotor.currentMaxSP;
			}
		}
	}
	
	public void ResetShieldRegenTimeStamp ()
	{
		nextShieldRegenTimeStamp = Time.time + equipmentProperties.shieldRegenDelay;
	}
	
	/*
	
	/// <summary>
	/// Harms this hull unit. First the shield get damage. If it has zero capacity, additional hull gets damage. If additional hull also at zero value, rest damage returned.
	/// </summary>
	/// <returns>
	/// The rest damage that was not absorbed by hull unit.
	/// </returns>
	/// <param name='damageAmount'>
	/// Damage amount on this hull unit.
	/// </param>
	public float GetDamage (float damageAmount)
	{
		nextShieldRegenTimeStamp = Time.time;
		
		if (damageAmount < currentShieldsPoints) {
			currentShieldsPoints =- damageAmount;
			return 0;
		} else {
			damageAmount -= currentShieldsPoints;
			currentShieldsPoints = 0;
		}
		
		if (damageAmount < parentMotor.currentHP) {
			parentMotor.currentHP =- damageAmount;
			return 0;
		} else {
			damageAmount -= currentShieldsPoints;
			currentShieldsPoints = 0;
		}
		
	
		
		return damageAmount;
	}
	
	*/
}
