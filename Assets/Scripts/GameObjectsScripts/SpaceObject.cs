using UnityEngine;
using System.Collections;

[AddComponentMenu("Other Objects Components/Space Object")]

public class SpaceObject : MonoBehaviour
{
	protected GameManager gm;
	
	protected virtual void Start ()
	{
		gm = GameManager.Instance;
	}
	
	public virtual void OnTriggerStay (Collider otherCollider)
	{
		// Send messages to all rigidbodies, that was collided
		otherCollider.SendMessage ("SOCollided", this, SendMessageOptions.DontRequireReceiver);
		Selfdestruct (DamageSource.Unknown);
	}
	
	public virtual void SOCollided (SpaceObject collidedObject)
	{
		Selfdestruct (DamageSource.Unknown);
	}

	public virtual void Selfdestruct (DamageSource source)
	{
		Destroy (gameObject);
	}
}
