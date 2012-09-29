using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float accelerationDeadZone = 0.05f;
	public float accelertaionMaxValue = 0.4f;
	private float accelertaionMultiplier;
	
	#region Singleton
	public static PlayerController Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.Find ("GameManager").GetComponent<PlayerController> ();
			}
			return _instance;
		}
	}
	
	private static PlayerController _instance;
	#endregion
	
	// Use this for initialization
	void Start ()
	{
		accelertaionMultiplier = 1 / accelertaionMaxValue;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float a = Input.acceleration.x;
		if ((a >= accelerationDeadZone && a < accelertaionMaxValue) || (a <= -accelerationDeadZone && a > -0.5f)) {
			StrafeShip (a * accelertaionMultiplier);			
		} else if (a <= -accelertaionMaxValue) {
			StrafeShip (-1f);
		} else if (a >= accelertaionMaxValue) {
			StrafeShip (1f);
		}
	}

	void JoystickMoved (Vector3 delta)
	{
		StrafeShip (delta.x);
		ChangeTargetSpeed (delta.y);
	}
	
	public void StrafeShip (float strafeValue)
	{
		if (strafeValue > 1) {
			strafeValue = 1;
		} else if (strafeValue < -1) {
			strafeValue = -1;
		}
		if (GameManager.Instance.playerShip != null) {
			GameManager.Instance.playerShip.transform.localPosition += 
			GameManager.Instance.playerShip.transform.localRotation * 
				new Vector3 (strafeValue * GameManager.Instance.playerShip.strafe * Time.deltaTime, 0, 0);
		}
	}

	public void ChangeTargetSpeed (float changeValue)
	{
		GameManager.Instance.playerShip.TargetSpeed += Time.deltaTime * changeValue * 20f;
	}
	
	public void SetAutoFire (bool isAutoFire)
	{
		GameManager.Instance.playerShip.isAutoFire = isAutoFire;
	}
}
