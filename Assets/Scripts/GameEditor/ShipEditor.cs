using UnityEngine;
using System.Collections;

public class ShipEditor : TypicalEditor
{
	public UIPopupList poplst_engineSlots,
	poplst_hullSlots,
	poplst_weaponSlots,
	poplst_miscSlots;
	public UIInput
		inpt_weight,
		inpt_hp,
		inpt_award,
		inpt_minSpeed,
		inpt_maxSpeed,
		inpt_strafe,
		inpt_accelleration,
		inpt_engineSlotX,
		inpt_engineSlotY,
		inpt_hullSlotX,
		inpt_hullSlotY,
		inpt_weaponSlotX,
		inpt_weaponSlotY,
		inpt_miscSlotX,
		inpt_miscSlotY;
	public UISprite sprt_sprite;
	public UIAtlas iconsAtlas;
	private ShipSaveData currentShip;
	
	void Start ()
	{
		int r = Random.Range (1000, 9999);
		currentShip = new ShipSaveData ("Ship" + r);
	}
	
	public void Load (ShipSaveData ship)
	{
		currentShip = ship;
		inpt_name.text = ship.name;
		inpt_accelleration.text = ship.accelerationBonus.ToString ();
		inpt_award.text = ship.award.ToString ();
		inpt_hp.text = ship.baseHP.ToString ();
		inpt_maxSpeed.text = ship.maxSpeedBonus.ToString ();
		inpt_minSpeed.text = ship.minSpeedBonus.ToString ();
		inpt_strafe.text = ship.strafeBonus.ToString ();
		inpt_weight.text = ship.maxCargoWeight.ToString ();
		
		sprite_icon.spriteName = ship.sprite;
		sprt_sprite.spriteName = ship.icon;
		
		RefreshEngineSlotsList ();
		poplst_engineSlots.selection = "-";
		poplst_engineSlots.selection = poplst_engineSlots.items [0];
		
		RefreshHullSlotsList ();
		poplst_hullSlots.selection = "-";
		poplst_hullSlots.selection = poplst_hullSlots.items [0];
		
		RefreshWeaponSlotsList ();
		poplst_weaponSlots.selection = "-";
		poplst_weaponSlots.selection = poplst_weaponSlots.items [0];
		
		RefreshMiscSlotsList ();
		poplst_miscSlots.selection = "-";
		poplst_miscSlots.selection = poplst_miscSlots.items [0];
	}
	
	void OnSaveBtnClick ()
	{
		if (!EditorMenu.Instance.shipSaveCollection.ships.Contains (currentShip)) {
			EditorMenu.Instance.shipSaveCollection.ships.Add (currentShip);
		}
		EditorMenu.Instance.resourceManager.Save ("ships.xml", EditorMenu.Instance.shipSaveCollection, typeof(ShipsSaveCollection));
	}
	
	void OnBackBtnClick ()
	{
		EditorMenu.FlipPanels (EditorMenu.Instance.area_shipPanel, EditorMenu.Instance.area_menuPanel);
	}
	
	void OnRemoveEngineSlotBtnClick ()
	{
		int removingItem = int.Parse (poplst_engineSlots.selection);
		
		if (currentShip.engineSlots.Count > 1) {
			currentShip.engineSlots.RemoveAt (removingItem);
			RefreshEngineSlotsList ();
			poplst_engineSlots.selection = "-";
			poplst_engineSlots.selection = (removingItem > 0 ? removingItem - 1 : 0).ToString ();
		}
	}
	
	void OnRemoveHullSlotBtnClick ()
	{
		int removingItem = int.Parse (poplst_engineSlots.selection);
		
		if (currentShip.hullSlots.Count > 1) {
			currentShip.hullSlots.RemoveAt (removingItem);
			RefreshHullSlotsList ();
			poplst_hullSlots.selection = "-";
			poplst_hullSlots.selection = (removingItem > 0 ? removingItem - 1 : 0).ToString ();
		}
	}
	
	void OnRemoveWeaponSlotBtnClick ()
	{
		int removingItem = int.Parse (poplst_engineSlots.selection);
		
		if (currentShip.weaponSlots.Count > 1) {
			currentShip.weaponSlots.RemoveAt (removingItem);
			RefreshWeaponSlotsList ();
			poplst_weaponSlots.selection = "-";
			poplst_weaponSlots.selection = (removingItem > 0 ? removingItem - 1 : 0).ToString ();
		}
	}
	
	void OnRemoveMiscSlotBtnClick ()
	{
		int removingItem = int.Parse (poplst_engineSlots.selection);
		
		if (currentShip.miscSlots.Count > 1) {
			currentShip.miscSlots.RemoveAt (removingItem);
			RefreshMiscSlotsList ();
			poplst_miscSlots.selection = "-";
			poplst_miscSlots.selection = (removingItem > 0 ? removingItem - 1 : 0).ToString ();
		}
	}
	
	void OnNameInput (UIInput sender)
	{
		ShipSaveData selectedElement = EditorMenu.Instance.shipSaveCollection.ships.Find (delegate(ShipSaveData ssd)
		{
			return ssd.name == currentShip.name;
		}
		);
		if (sender.text.Equals ("New Ship", System.StringComparison.OrdinalIgnoreCase)) {
			lbl_status.text = "Wrong Name! Set another.";
			lbl_status.animation.Play ();
		} else if (selectedElement == null || selectedElement == currentShip) {
			currentShip.name = sender.text;
		} else {
			lbl_status.text = "Wrong Name! Already exists.";
			lbl_status.animation.Play ();
		}
	}
	
	void OnIconSelectBtnClick ()
	{
		EditorMenu.Instance.ShowSpriteSelectionDialog (EditorMenu.Instance.iconAtlas, "OnIconWasSelected", gameObject, EditorMenu.Instance.area_shipPanel);
	}
	
	void OnIconWasSelected (string selection)
	{
		sprite_icon.spriteName = selection;
		currentShip.icon = selection;
	}
	
	void OnSpriteSelectBtnClick ()
	{
		EditorMenu.Instance.ShowSpriteSelectionDialog (EditorMenu.Instance.shipSpritesAtlas, "OnSpriteWasSelected", gameObject, EditorMenu.Instance.area_shipPanel);
	}
	
	void OnSpriteWasSelected (string selection)
	{
		sprt_sprite.spriteName = selection;
		currentShip.sprite = selection;
	}
	
	void OnWeightInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentShip.maxCargoWeight);
	}
	
	void OnHPInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentShip.baseHP);
	}
	
	void OnAwardInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentShip.award);
	}
	
	void OnMinSpeedInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentShip.minSpeedBonus);
	}
	
	void OnMaxSpeedInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentShip.maxSpeedBonus);
	}
	
	void OnStrafeInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentShip.strafeBonus);
	}
	
	void OnAccellerationInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentShip.accelerationBonus);
	}
	
	void OnEngineSlotXInput (UIInput sender)
	{
		float result = 0;
		if (CheckFloatInput (sender, ref result)) {
			currentShip.engineSlots [int.Parse (poplst_engineSlots.selection)] = new Vector2 (
				result,
				currentShip.engineSlots [int.Parse (poplst_engineSlots.selection)].y);
		}
	}
	
	void OnEngineSlotYInput (UIInput sender)
	{
		float result = 0;
		if (CheckFloatInput (sender, ref result)) {
			currentShip.engineSlots [int.Parse (poplst_engineSlots.selection)] = new Vector2 (
				currentShip.engineSlots [int.Parse (poplst_engineSlots.selection)].x,
				result);
		}
	}
	
	void OnHullSlotXInput (UIInput sender)
	{
		float result = 0;
		if (CheckFloatInput (sender, ref result)) {
			currentShip.hullSlots [int.Parse (poplst_hullSlots.selection)] = new Vector2 (
				result,
				currentShip.hullSlots [int.Parse (poplst_hullSlots.selection)].y);
		}
	}
	
	void OnHullSlotYInput (UIInput sender)
	{
		float result = 0;
		if (CheckFloatInput (sender, ref result)) {
			currentShip.hullSlots [int.Parse (poplst_hullSlots.selection)] = new Vector2 (
				currentShip.hullSlots [int.Parse (poplst_hullSlots.selection)].x,
				result);
		}
	}
	
	void OnWeaponSlotXInput (UIInput sender)
	{
		float result = 0;
		if (CheckFloatInput (sender, ref result)) {
			currentShip.weaponSlots [int.Parse (poplst_weaponSlots.selection)] = new Vector2 (
				result,
				currentShip.weaponSlots [int.Parse (poplst_weaponSlots.selection)].y);
		}
	}
	
	void OnWeaponSlotYInput (UIInput sender)
	{
		float result = 0;
		if (CheckFloatInput (sender, ref result)) {
			currentShip.weaponSlots [int.Parse (poplst_weaponSlots.selection)] = new Vector2 (
				currentShip.weaponSlots [int.Parse (poplst_weaponSlots.selection)].x,
				result);
		}
	}
	
	void OnMiscSlotXInput (UIInput sender)
	{
		float result = 0;
		if (CheckFloatInput (sender, ref result)) {
			currentShip.miscSlots [int.Parse (poplst_miscSlots.selection)] = new Vector2 (
				result,
				currentShip.miscSlots [int.Parse (poplst_miscSlots.selection)].y);
		}
	}
	
	void OnMiscSlotYInput (UIInput sender)
	{
		float result = 0;
		if (CheckFloatInput (sender, ref result)) {
			currentShip.miscSlots [int.Parse (poplst_miscSlots.selection)] = new Vector2 (
				currentShip.miscSlots [int.Parse (poplst_miscSlots.selection)].x,
				result);
		}
	}
	
	void OnEngineSlotsSelectionChange (string selection)
	{
		if (selection == "-") {
			return;
		} else if (selection == "ADD..") {
			currentShip.engineSlots.Add (Vector2.zero);
			RefreshEngineSlotsList ();
			poplst_engineSlots.selection = "-";
			poplst_engineSlots.selection = poplst_engineSlots.items [poplst_engineSlots.items.Count - 2];
		} else {
			int selectedEngineSlot = int.Parse (selection);
			inpt_engineSlotX.text = currentShip.engineSlots [selectedEngineSlot].x.ToString ();
			inpt_engineSlotY.text = currentShip.engineSlots [selectedEngineSlot].y.ToString ();
		}
	}
	
	void OnHullSlotsSelectionChange (string selection)
	{
		if (selection == "-") {
			return;
		} else if (selection == "ADD..") {
			currentShip.hullSlots.Add (Vector2.zero);
			RefreshHullSlotsList ();
			poplst_hullSlots.selection = "-";
			poplst_hullSlots.selection = poplst_hullSlots.items [poplst_hullSlots.items.Count - 2];
		} else {
			int selectedHullSlot = int.Parse (selection);
			inpt_hullSlotX.text = currentShip.hullSlots [selectedHullSlot].x.ToString ();
			inpt_hullSlotY.text = currentShip.hullSlots [selectedHullSlot].y.ToString ();
		}
	}
	
	void OnWeaponSlotsSelectionChange (string selection)
	{
		if (selection == "-") {
			return;
		} else if (selection == "ADD..") {
			currentShip.weaponSlots.Add (Vector2.zero);
			RefreshWeaponSlotsList ();
			poplst_weaponSlots.selection = "-";
			poplst_weaponSlots.selection = poplst_weaponSlots.items [poplst_weaponSlots.items.Count - 2];
		} else {
			int selectedWeaponSlot = int.Parse (selection);
			inpt_weaponSlotX.text = currentShip.weaponSlots [selectedWeaponSlot].x.ToString ();
			inpt_weaponSlotY.text = currentShip.weaponSlots [selectedWeaponSlot].y.ToString ();
		}
	}
	
	void OnMiscSlotsSelectionChange (string selection)
	{
		if (selection == "-") {
			return;
		} else if (selection == "ADD..") {
			currentShip.miscSlots.Add (Vector2.zero);
			RefreshMiscSlotsList ();
			poplst_miscSlots.selection = "-";
			poplst_miscSlots.selection = poplst_miscSlots.items [poplst_miscSlots.items.Count - 2];
		} else {
			int selectedMiscSlot = int.Parse (selection);
			inpt_miscSlotX.text = currentShip.miscSlots [selectedMiscSlot].x.ToString ();
			inpt_miscSlotY.text = currentShip.miscSlots [selectedMiscSlot].y.ToString ();
		}
	}
	
	void RefreshEngineSlotsList ()
	{
		poplst_engineSlots.items.Clear ();
		for (int i = 0; i < currentShip.engineSlots.Count; i++) {
			poplst_engineSlots.items.Add (i.ToString ());
		}
		poplst_engineSlots.items.Add ("ADD..");
	}
	
	void RefreshHullSlotsList ()
	{
		poplst_hullSlots.items.Clear ();
		for (int i = 0; i < currentShip.hullSlots.Count; i++) {
			poplst_hullSlots.items.Add (i.ToString ());
		}
		poplst_hullSlots.items.Add ("ADD..");
	}
	
	void RefreshWeaponSlotsList ()
	{
		poplst_weaponSlots.items.Clear ();
		for (int i = 0; i < currentShip.weaponSlots.Count; i++) {
			poplst_weaponSlots.items.Add (i.ToString ());
		}
		poplst_weaponSlots.items.Add ("ADD..");
	}
	
	void RefreshMiscSlotsList ()
	{
		poplst_miscSlots.items.Clear ();
		for (int i = 0; i < currentShip.miscSlots.Count; i++) {
			poplst_miscSlots.items.Add (i.ToString ());
		}
		poplst_miscSlots.items.Add ("ADD..");
	}
}
