using UnityEngine;
using System.Collections;

[AddComponentMenu("Menu Components/Equipment/Equipment Cell")]

public class EquipmentCell : MonoBehaviour
{
	public EquipmentUnit test;

	public Material noItemIcon;

	public TypicalSlot linkToSlot;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void OnMouseDown ()
	{
		switch (linkToSlot.type) {
		case EquipmentType.Weapon:
			if (linkToSlot.mountedEquipmentUnit == null) {
				linkToSlot.MountEquipment (test);
			} else {
				linkToSlot.UnmountEquipment ();
			}
			break;
		}
	}
	void OnMouseOver ()
	{
		transform.Find("Underlay").renderer.material.color = Color.green;
	}
	void OnMouseExit ()
	{
		transform.Find("Underlay").renderer.material.color = Color.white;
	}
}
