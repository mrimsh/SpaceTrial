using UnityEngine;
using System.Collections;

[AddComponentMenu("Ships Components/AI/AI Controller")]

public class AIController : ShipController
{
	[HideInInspector]
	public SpaceShipMotor shipMotor;
	private float lastDeadZonePosition;

	// Use this for initialization
	void Start ()
	{
		shipMotor = GetComponent<SpaceShipMotor> ();
		shipMotor.TargetSpeed = shipMotor.maxSpeed;
		shipMotor.isAutoFire = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.playerShip != null) {
			lastDeadZonePosition = GameManager.Instance.playerShip.transform.localPosition.y - 150f;
		}
		if (transform.localPosition.y < lastDeadZonePosition) {
			shipMotor.DestroyShip (DamageSource.Enviroment);
		}
	}

	void ShipDestroyed (DamageSource source)
	{	
		GameManager.Instance.soFactory.shipsInGame--;
	}
}
