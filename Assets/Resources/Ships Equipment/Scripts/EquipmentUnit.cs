using UnityEngine;
using System.Collections;

[AddComponentMenu("Ships Components/Equipment/Equipment Unit")]

public class EquipmentUnit : MonoBehaviour {
	
	public EquipmentProperties equipmentProperties;
	[HideInInspector]
	public SpaceShipMotor parentMotor;
}
