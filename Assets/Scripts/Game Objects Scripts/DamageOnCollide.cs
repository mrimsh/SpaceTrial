using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpaceShipMotor))]

[AddComponentMenu("Ships Components/Damage On Collide")]

public class DamageOnCollide : MonoBehaviour
{
	public float damageGainingFrequency = 0.3f, selfDamage = 1.0f, targetDamage = 1.0f;

	private float lastDamageTime = 0f;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void OnCollisionStay (Collision collisionInfo)
	{
		if (Time.time > lastDamageTime + damageGainingFrequency) {
			foreach (ContactPoint contact in collisionInfo.contacts) {
				SpaceShipMotor targetSpaceShipMotor = contact.otherCollider.GetComponent<SpaceShipMotor> ();
				GlobalProperties globalProperties = contact.otherCollider.GetComponent<GlobalProperties> ();
				
				
				if (globalProperties != null) {
					if (!globalProperties.invulnerable) {
						if (targetSpaceShipMotor != null) {
							targetSpaceShipMotor.DamageShip (targetDamage, (transform.parent.GetComponent<PlayerController> () == null) ? DamageSource.FriendlyShip : DamageSource.Player);
							GetCollisionDamage ();
						} else {
							Destroy (contact.otherCollider.gameObject);
							GetCollisionDamage ();
						}
					}
				} else {
					if (targetSpaceShipMotor != null) {
						targetSpaceShipMotor.DamageShip (targetDamage, (transform.parent.GetComponent<PlayerController> () == null) ? DamageSource.FriendlyShip : DamageSource.Player);
						GetCollisionDamage ();
					} else {
						Destroy (contact.otherCollider.gameObject);
						GetCollisionDamage ();
					}
				}
				
				Debug.DrawRay (contact.point, contact.normal, Color.white, 3.0f);
			}
		}
	}

	private void GetCollisionDamage ()
	{
		GetComponent<SpaceShipMotor> ().DamageShip (selfDamage, DamageSource.Self);
		lastDamageTime = Time.time;
	}
}
