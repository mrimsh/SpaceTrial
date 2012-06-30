using UnityEngine;
using System.Collections;

[AddComponentMenu("Ships Components/AI/AI Controller")]

public class AIController : ShipController
{
	[HideInInspector]
	public SpaceShipMotor shipMotor;

	// Use this for initialization
	void Start ()
	{
		if (levelProperties == null) {
			levelProperties = GameObject.Find ("GameManager").GetComponent<LevelProperties> ();
		}
		transform.position = new Vector3 (Random.Range (levelProperties.spawnZone.transform.position.x - levelProperties.spawnZone.transform.localScale.x * 0.5f, levelProperties.spawnZone.transform.position.x + levelProperties.spawnZone.transform.localScale.x * 0.5f), levelProperties.spawnZone.transform.position.y, levelProperties.spawnZone.transform.position.z);
	}

	// Update is called once per frame
	void Update ()
	{
		if (shipMotor != null) {
			shipMotor.currentFlySpeed = shipMotor.properties.maxFlySpeed;
		}
	}

	public override void DestroyShip (DamageSource source)
	{
		levelProperties.statistics.AddDestroyedShip (shipMotor.name, source);
		levelProperties.levelGenerator.decreaseInGameObjects ();
		base.DestroyShip (source);
	}
}
