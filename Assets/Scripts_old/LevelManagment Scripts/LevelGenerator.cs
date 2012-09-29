using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LevelProperties))]

[AddComponentMenu("Level Components/Level Generator")]

public class LevelGenerator : MonoBehaviour
{
	/// <summary>
	/// Level properties object.
	/// </summary>
	public LevelProperties levelProperties;

	private float lastGeneratedObjectTime = 0f;
	private int currentNumOfObjects = 0;
	private int nmbrOfGeneratedObjects = 0;
	private GameObject currentShip;
	private GameState currentGameState = GameState.Init;

	[HideInInspector]
	public SpaceShipMotor_old currentShipMotor;

	void Start ()
	{
		// Random init
		if (PlayerPrefs.GetInt ("GameTypeID") == MainMenu_old.GT_RANDOM) {
			RandomizeLevelProperties ();
		}
		if (PlayerPrefs.GetInt ("GameDifficultyID") == 0) {
			//TODO: casual/hardcore initialization
		}
		currentGameState = GameState.Game;
	}

	private void RandomizeLevelProperties ()
	{
		/// Space objects generation properties
		levelProperties.objectsGenerationFrequency = Random.Range (0.2f, 0.8f);
		levelProperties.objectsInGameMax = Random.Range (4, 8);
		
		/// Victory conditions
		while (levelProperties.statistics.IsLevelPassed ()) {
			if (Random.value > 0.5f) {
				levelProperties.levelVictoryConditions.destroyedShipsNumberCondition.isToCheck = true;
				levelProperties.levelVictoryConditions.destroyedShipsNumberCondition.shipsToDestroy = Random.Range (10, 40);
			} else {
				levelProperties.levelVictoryConditions.destroyedShipsNumberCondition.isToCheck = false;
			}
			if (Random.value > 0.5f) {
				levelProperties.levelVictoryConditions.destroyedSpecificShipsNumberCondition.isToCheck = true;
				foreach (SpecialShipTypeDestroyAimInfo shipAim in levelProperties.levelVictoryConditions.destroyedSpecificShipsNumberCondition.shipTypesToDestroyData) {
					shipAim.shipsToDestroy = Random.Range (10, 40);
				}
			} else {
				levelProperties.levelVictoryConditions.destroyedSpecificShipsNumberCondition.isToCheck = false;
			}
			if (Random.value > 0.5f) {
				levelProperties.levelVictoryConditions.distanceVictoryCondition.isToCheck = true;
				levelProperties.levelVictoryConditions.distanceVictoryCondition.distanceToAchieve = Random.Range (500, 1500);
			} else {
				levelProperties.levelVictoryConditions.distanceVictoryCondition.isToCheck = false;
			}
		}
		
		/// BG
		levelProperties.bgObjectsAtOneScreen = Random.Range (30, 60);
	}

	// Update is called once per frame
	void Update ()
	{
		// Generate game objects
		if (currentNumOfObjects < levelProperties.objectsInGameMax && Time.time > lastGeneratedObjectTime + levelProperties.objectsGenerationFrequency && CurrentGameState == GameState.Game) {
			string newShipName = ((SpaceObject)levelProperties.spaceObjects.GetValue (Random.Range (0, levelProperties.spaceObjects.Length - 1))).gameObject.name;
			
			currentShip = new GameObject (newShipName + " Container", typeof(ShipAssembler), typeof(AIController));
			ShipAssembler shipAssembler = currentShip.GetComponent<ShipAssembler> ();
			currentShipMotor = shipAssembler.AssembleShip (newShipName, newShipName);
			
//			currentShip.GetComponent<AIController> ().shipMotor = currentShipMotor;
			
			nmbrOfGeneratedObjects++;
			currentNumOfObjects++;
			lastGeneratedObjectTime = Time.time;
		}
	}

	public void decreaseInGameObjects ()
	{
		currentNumOfObjects--;
	}

	public GameState CurrentGameState {
		get { return this.currentGameState; }
		set {
			switch (value) {
			case GameState.Game:
				break;
			case GameState.Lose:
				Application.LoadLevel ("MainMenu");
				break;
			case GameState.Win:
				levelProperties.guiManager.isToShowGIE = false;
				levelProperties.guiManager.isToShowStatistics = true;
				break;
			}
			currentGameState = value;
		}
	}
}

public enum GameState
{
	Init,
	Game,
	Win,
	Lose
}
