using UnityEngine;
using System.Collections;

[System.Serializable]
public class EquipmentSlot
{
	#region Constructors
	private EquipmentSlot ()
	{
	}

	public EquipmentSlot (SpaceShipMotor parentShip)
	{
		this.parentShip = parentShip;
	}
	#endregion
	
	private float nextUseTime;
	private float lastUseTime;
	
	public EquipmentSaveData MountedEquipment {
		set {
			if (value == null) {
				Dismount ();
			} else {
				_mountedEquipment = value;
				parentShip.minSpeed += _mountedEquipment.minSpeed;
				parentShip.maxSpeed += _mountedEquipment.maxSpeed;
				parentShip.acceleration += _mountedEquipment.acceleration;
				parentShip.maxHP += _mountedEquipment.hp;
				parentShip.maxEP += _mountedEquipment.ep;
				parentShip.epRegen += _mountedEquipment.epRegen;
				parentShip.maxSP += _mountedEquipment.sp;
				parentShip.spRegen += _mountedEquipment.spRegen;
				parentShip.spRegenDelay += _mountedEquipment.spRegenDelay;
			}
		}
		get {
			return _mountedEquipment;
		}
	}

	public SpaceShipMotor parentShip;
	public /*private*/ EquipmentSaveData _mountedEquipment;
	
	public void Use ()
	{
		if (_mountedEquipment != null) {
			if (_mountedEquipment.type == EquipmentType.Weapon) {
				if (lastUseTime + _mountedEquipment.cooldown < Time.time &&
					parentShip.CurrentEP > _mountedEquipment.energyCost) {
					
					BulletMotor newBullet = ((GameObject)GameObject.Instantiate (GameManager.Instance.typicalBulletPrefab)).GetComponent<BulletMotor> ();
					newBullet.transform.parent = GameManager.Instance.gamePanel.transform;
					newBullet.transform.localPosition = parentShip.transform.localPosition + (parentShip.transform.localRotation * new Vector3 (0, 40f, 0));
					newBullet.transform.localRotation = parentShip.transform.localRotation;
					newBullet.transform.localScale = Vector3.one;
					newBullet.AmmoOriginData = MidSceneData.Instance.ammoInLevel.ammo.Find (delegate(AmmoSaveData asd)
					{
						return asd.name == _mountedEquipment.ammo [0];
					});
					newBullet.sourceShip = parentShip;
					parentShip.CurrentEP -= _mountedEquipment.energyCost;
					
					lastUseTime = Time.time;
				}
			}
		}
	}
	
	public void Dismount ()
	{
		if (_mountedEquipment != null) {
			parentShip.minSpeed -= _mountedEquipment.minSpeed;
			parentShip.maxSpeed -= _mountedEquipment.maxSpeed;
			parentShip.acceleration -= _mountedEquipment.acceleration;
			parentShip.maxHP -= _mountedEquipment.hp;
			parentShip.maxEP -= _mountedEquipment.ep;
			parentShip.epRegen -= _mountedEquipment.epRegen;
			parentShip.maxSP -= _mountedEquipment.sp;
			parentShip.spRegen -= _mountedEquipment.spRegen;
			parentShip.spRegenDelay -= _mountedEquipment.spRegenDelay;
		}
	}
}
