using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BulletProperties
{
    public float baseDamage, cooldownMultiplier = 1f, cooldownBonus, lifetime = 1f;
    public List<AIBehaviour> behaviours;

    [HideInInspector]
    public float damage;
    [HideInInspector]
    public SpaceShipMotor master;
}
