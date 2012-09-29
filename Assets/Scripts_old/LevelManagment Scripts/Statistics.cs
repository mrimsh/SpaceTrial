using UnityEngine;
using System.Collections.Generic;

public class Statistics : MonoBehaviour
{
	/// <summary>
	/// Collection of ship names and how many of them was destroyed.
	/// </summary>
	private List<ShipsStatistics> shipsStatistics = new List<ShipsStatistics> ();
	
	private float playerLastPosition;
	
	public LevelProperties levelProperties;

	[HideInInspector]
	public int numberOfDestroyedShips;
	[HideInInspector]
	public float playerDistanceMoved;
	
	public List<ShipsStatistics> ShipsStatistics {
		get {
			return this.shipsStatistics;
		}
	}
	
	/// <summary>
	/// Adds destroyed ship. If there wasn't ship with this name - function creates a new <see cref="ShipsStatistics"> class. Else it just adds one to destroyed quantity.
	/// </summary>
	/// <param name="shipName">
	/// A <see cref="System.String"/> name of ship that was destroyed.
	/// </param>
	/// <param name="deathSource">
	/// A <see cref="DamageSource"/> info, about a source of damage that destroyed the ship.
	/// </param>
	public void AddDestroyedShip (string shipName, DamageSource deathSource)
	{
		if (deathSource == DamageSource.Player || deathSource == DamageSource.Self || deathSource == DamageSource.FriendlyShip) {
			numberOfDestroyedShips++;
			
			foreach (ShipsStatistics shipsstats in shipsStatistics) {
				if (shipsstats.name.Equals (shipName)) {
					shipsstats.quantity++;
					return;
				}
			}
			
			ShipsStatistics newShipStatistics = new ShipsStatistics ();
			shipsStatistics.Add (newShipStatistics);
			newShipStatistics.name = shipName;
		}
	}

	public bool IsLevelPassed ()
	{
		return levelProperties.levelVictoryConditions.destroyedShipsNumberCondition.CheckVictoryCondition () && levelProperties.levelVictoryConditions.destroyedSpecificShipsNumberCondition.CheckVictoryCondition () && levelProperties.levelVictoryConditions.distanceVictoryCondition.CheckVictoryCondition ();
	}

	// Use this for initialization
	void Start ()
	{
		if (levelProperties == null) {
			levelProperties = GameObject.Find ("GameManager").GetComponent<LevelProperties> ();
		}
		
		playerLastPosition = levelProperties.playerController.ship.transform.position.y;
	}

	// Update is called once per frame
	void Update ()
	{
		if (IsLevelPassed () && levelProperties.levelGenerator.CurrentGameState == GameState.Game) {
			levelProperties.levelGenerator.CurrentGameState = GameState.Win;
		}
	}

	void FixedUpdate ()
	{
		if (levelProperties.levelGenerator.CurrentGameState == GameState.Game) {
			playerDistanceMoved += levelProperties.playerController.ship.transform.position.y - playerLastPosition;
			playerLastPosition = levelProperties.playerController.ship.transform.position.y;
		}
	}
}

/// <summary>
/// Class to collect number of destroyed ships with identical names.
/// </summary>
public class ShipsStatistics
{
	public string name;
	public int quantity = 1;
}
