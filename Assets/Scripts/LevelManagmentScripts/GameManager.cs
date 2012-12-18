using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public const float SCREENHEIGHT = 960f;
	public static float SCREENWIDTH;
	
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
	/// <summary>
	/// The offset of ship from bottom border of screen relatively to its height
	/// </summary>
	public float viewOffset;
	public SpaceObjectsFactory soFactory;
	private float resultOffset;
	
	void Awake ()
	{
		_instance = this;
		SCREENWIDTH = ((float)Screen.width / (float)Screen.height) * SCREENHEIGHT;
	}

	// Use this for initialization
	void Start ()
	{
		resultOffset = (viewOffset * SCREENHEIGHT) - SCREENHEIGHT * 0.5f;
		soFactory = GetComponent<SpaceObjectsFactory> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerShip != null) {
			gamePanel.transform.localPosition = new Vector3 (0, resultOffset - playerShip.transform.localPosition.y, 0);
		}
	}
}
