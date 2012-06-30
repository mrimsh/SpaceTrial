using UnityEngine;
using System.Collections;

[AddComponentMenu("Ships Components/Ship Controller")]

public class ShipController : MonoBehaviour
{
	protected LevelProperties levelProperties;
	
	void Start ()
	{
		levelProperties = GameObject.Find ("GameManager").GetComponent<LevelProperties> ();
	}
	
	public virtual void DestroyShip (DamageSource source)
	{
		Destroy (gameObject);
	}
}