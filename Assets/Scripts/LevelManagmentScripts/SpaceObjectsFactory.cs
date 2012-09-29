using UnityEngine;
using System.Collections;

public class SpaceObjectsFactory : MonoBehaviour
{
	public UIAtlas SpaceShipsAtlas;
	public Transform parentPanel;
	public float verticalSpawnOffset = 370f;
	[HideInInspector]
	public int shipsInGame;
	private int shipsWasProduced;
	private float lastShipCreationTime;
	LevelSaveData selectedLevel;
	
	void Awake ()
	{
		// Create player
		SpaceShipMotor playerShip = CreateShipTemplate (new Vector3 (0, 0, -1f), Quaternion.identity);
		playerShip.ActivateShip (MidSceneData.Instance.selectedInitialShip);
		playerShip.Sprite.depth = 100;
		GameManager.Instance.playerShip = playerShip;
	}

	// Use this for initialization
	void Start ()
	{
		selectedLevel = MidSceneData.Instance.selectedLevel;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.playerShip != null) {
			if (shipsInGame < selectedLevel.spaceObjectsAtScreen &&
			(selectedLevel.spaceObjectsInLevel == 0 || shipsWasProduced < selectedLevel.spaceObjectsInLevel)) {
				if (lastShipCreationTime + MidSceneData.Instance.selectedLevel.spaceObjectsGenerationFrequency < Time.time) {
					SpaceShipMotor newShip = CreateShipTemplate (
					new Vector3 (Random.Range (-150f, 150f), GameManager.Instance.playerShip.transform.localPosition.y + verticalSpawnOffset, -1f),
					new Quaternion (0, 0, 180, 0));
					newShip.ActivateShip (MidSceneData.Instance.shipsInLevel.ships [Random.Range (0, MidSceneData.Instance.shipsInLevel.ships.Count)].name);
					newShip.gameObject.AddComponent<AIController> ();
					shipsInGame++;
					shipsWasProduced++;
					lastShipCreationTime = Time.time;
				}
			}
		}
	}
	
	private SpaceShipMotor CreateShipTemplate (Vector3 position, Quaternion rotation)
	{
		SpaceShipMotor newShip = (Instantiate (GameManager.Instance.typicalShipPrefab) as GameObject).GetComponent<SpaceShipMotor> ();
		newShip.transform.parent = parentPanel;
		newShip.transform.localPosition = position;
		newShip.transform.localRotation = rotation;
		newShip.transform.localScale = Vector3.one;

		return newShip;
	}
}
