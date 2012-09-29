using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	
	#region Singletone
	public static GameManager Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.Find ("GameManager").GetComponent<GameManager> ();
			}
			return _instance;
		}
	}
	
	private static GameManager _instance;
	#endregion
	
	[HideInInspector]
	public SpaceShipMotor playerShip;
	public GameObject typicalShipPrefab;
	public GameObject typicalBulletPrefab;
	public GameObject gamePanel;
	public float rightBorder, leftBorder;
	public SpaceObjectsFactory soFactory;
	
	void Awake ()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		soFactory = GetComponent<SpaceObjectsFactory> ();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (playerShip != null) {
			gamePanel.transform.localPosition = new Vector3 (0, -90f - playerShip.transform.localPosition.y, 0);
		}
	}
}
