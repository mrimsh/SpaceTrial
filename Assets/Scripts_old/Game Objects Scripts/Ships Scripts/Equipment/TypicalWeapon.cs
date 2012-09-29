using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Ships Components/Equipment/Weapons/Typical Weapon")]

public class TypicalWeapon : EquipmentUnit
{
	public WeaponProperties properties;
	public bool isActive = false;

	private float nextShotTime;

	public void Fire ()
	{
		if (Time.time - nextShotTime > 0 && parentMotor.currentEP > properties.energyCost) {
			properties.availableBullets[properties.activeBulletIndex].properties.damage = properties.availableBullets[properties.activeBulletIndex].properties.baseDamage * properties.damageMultiplier + properties.damageBonus;
			TypicalBullet_old newBullet = (TypicalBullet_old)Instantiate (properties.availableBullets[properties.activeBulletIndex].gameObject.GetComponent<TypicalBullet_old> ());
			newBullet.transform.position = transform.parent.position + ((WeaponSlot) equipmentProperties.mySlot).bulletSpawnPosition;
			newBullet.transform.localRotation = Quaternion.Euler (90f, 0f, 0f);
			newBullet.properties.master = equipmentProperties.mySlot.shipParent;
			parentMotor.currentEP -= properties.energyCost;
			
			nextShotTime = Time.time + properties.cooldown * newBullet.properties.cooldownMultiplier + newBullet.properties.cooldownBonus;
		}
	}
}
