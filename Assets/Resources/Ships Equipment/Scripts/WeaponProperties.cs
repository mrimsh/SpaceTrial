using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WeaponProperties
{
    public float damageMultiplier = 1f;
	public float damageBonus = 0f;
	public float cooldown = 0.3f;
	public float energyCost = 0f;
    public int activeBulletIndex;
	[HideInInspector]
    public WeaponSlot mySlot;
    public List<TypicalBullet> availableBullets;
}
