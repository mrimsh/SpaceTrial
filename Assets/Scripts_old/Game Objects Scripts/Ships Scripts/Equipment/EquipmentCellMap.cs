using UnityEngine;
using System.Collections;

public class EquipmentCellMap : MonoBehaviour
{
	public GameObject cellPrefab;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void OnGUI ()
	{
	}
	
	/// <summary>
	/// Show Equipment Management interface if it was hidden or hide it if it was visible.
	/// </summary>
	public void ToggleEquipmentWindowVisibility ()
	{
		if (transform.gameObject.active) {
			SetEquipmentWindowVisibility (false);
		} else {
			SetEquipmentWindowVisibility (true);
		}
	}
	
	/// <summary>
	/// Hide or show Equipment Management interface.
	/// </summary>
	/// <param name="isToShow">
	/// A <see cref="System.Boolean"/> to show or not window.
	/// </param>
	/// <seealso cref="ToggleEquipmentWindowVisibility"/>
	public void SetEquipmentWindowVisibility (bool isToShow)
	{
		transform.gameObject.active = isToShow;
		foreach (Transform t in transform) {
			t.gameObject.active = isToShow;
			foreach (Transform tt in t) {
				tt.gameObject.active = isToShow;
			}
		}
	}

	public EquipmentCell AddEquipmentCell (TypicalSlot parentSlot)
	{
		EquipmentCell newCell = ((GameObject)Instantiate (cellPrefab)).GetComponent<EquipmentCell> ();
		
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (parentSlot.iconBounds.x, 0, parentSlot.iconBounds.y);
		newCell.transform.localRotation = Quaternion.identity;
		newCell.transform.localScale = new Vector3 (parentSlot.iconBounds.width, 1, parentSlot.iconBounds.height);
		newCell.linkToSlot = parentSlot;
		newCell.linkToSlot.linkToCell = newCell;
		return newCell;
	}
}
