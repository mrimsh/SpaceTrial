using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]

[AddComponentMenu("Other Objects Components/Destroy On Touch")]

public class DestroyOnTouch : MonoBehaviour
{
	void OnTriggerEnter (Collider collidedCollider)
	{
		Destroyable target = collidedCollider.gameObject.GetComponent<Destroyable> ();
		if (target != null) {
			ShipController sc = target.targetToDestroy.GetComponent<ShipController> ();
			
			if (sc != null && target.isDestroyable) {
				sc.DestroyShip (DamageSource.Enviroment);
			} else if (target.isDestroyable) {
				Destroy (target.targetToDestroy);
			}
		}
	}
}
