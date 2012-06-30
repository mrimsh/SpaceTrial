using UnityEngine;
using System.Collections;

[System.Serializable]

public class TypicalSlot
{
	[HideInInspector]
	public LevelProperties levelProperties;
	public EquipmentUnit mountedEquipmentUnit;
	public EquipmentCell linkToCell;

	/// <summary>
	/// Type of the equipment. See <see cref="EquipmentType"/> enumeration.
	/// </summary>
	public EquipmentType type;

	/// <summary>
	/// Link to ship motor that mounts equipment from this slot. 
	/// </summary>
	public SpaceShipMotor shipParent;

	/// <summary>
	/// Bounds of cell and equipment icon on Equipment Map.
	/// </summary>
	public Rect iconBounds;
	
	public Vector2 iconPosition;

	/// <summary>
	/// Creates and ataches new equipment, from selected prefab, to a ship.
	/// </summary>
	/// <param name="eUnit">
	/// A <see cref="EquipmentUnit"/> of equipment that need to be mounted.
	/// </param>
	/// <returns>
	/// A <see cref="EquipmentUnit"/> that was created and mounted, or null if failed.
	/// </returns>
	public EquipmentUnit MountEquipment (EquipmentUnit eUnit)
	{
		if (eUnit.equipmentProperties.type == type) {
			EquipmentUnit newEUnit = (EquipmentUnit)GameObject.Instantiate (eUnit);
			
			switch (type) {
			case EquipmentType.Weapon:
				break;
			case EquipmentType.Hull:
				break;
			case EquipmentType.Engine:
				break;
			case EquipmentType.Misc:
				break;
			}
			newEUnit.equipmentProperties.mySlot = this;
			newEUnit.transform.parent = getLevelProperties ().playerController.ship.transform;
			mountedEquipmentUnit = newEUnit;
			mountedEquipmentUnit.parentMotor = getLevelProperties ().playerController.shipMotor;
//			linkToCell.renderer.material = eUnit.equipmentProperties.icon;
			
			return mountedEquipmentUnit;
		} else {
			return null;
		}
	}

	/// <summary>
	/// Removes equipment from the current ship. 
	/// </summary>
	public void UnmountEquipment ()
	{
//		linkToCell.renderer.material = linkToCell.noItemIcon;
		GameObject.Destroy (mountedEquipmentUnit.gameObject);
	}

	/// <summary>
	/// Returns Level Properties from GameManager gameobject.
	/// </summary>
	/// <returns>
	/// A <see cref="LevelProperties"/>.
	/// </returns>
	private LevelProperties getLevelProperties ()
	{
		if (levelProperties == null) {
			levelProperties = GameObject.Find ("GameManager").GetComponent<LevelProperties> ();
		}
		return levelProperties;
	}
}

public enum EquipmentType
{
	Weapon,
	Hull,
	Engine,
	Misc
}
