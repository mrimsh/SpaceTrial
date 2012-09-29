using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentEditor : TypicalEditor
{
	public UIInput inpt_acceleration,
	inpt_ep,
	inpt_epRegen,
	inpt_hp,
	inpt_hpRegen,
	inpt_maxSpeed,
	inpt_minSpeed,
	inpt_sp,
	inpt_spRegen,
	inpt_spRegenDelay,
	inpt_strafe,
	inpt_weight,
	inpt_cooldown,
	inpt_damageBonus,
	inpt_damageMultiplier,
	inpt_energyCost;
	public UILabel lbl_ammoName;
	public UIPopupList poplst_type,
	poplst_ammo;
	public GameObject area_mainProps;
	public UISprite sprite_ammo;
	private EquipmentSaveData currentEquipmentElement;
	
	void Awake ()
	{
		int r = Random.Range (1000, 9999);
		currentEquipmentElement = new EquipmentSaveData ("Equipment" + r);
	}
	
	public void Load (EquipmentSaveData equipmentElement)
	{
		currentEquipmentElement = equipmentElement;
		inpt_acceleration.text = currentEquipmentElement.acceleration.ToString ();
		inpt_ep.text = currentEquipmentElement.ep.ToString ();
		inpt_epRegen.text = currentEquipmentElement.epRegen.ToString ();
		inpt_hp.text = currentEquipmentElement.hp.ToString ();
		inpt_hpRegen.text = currentEquipmentElement.hpRegen.ToString ();
		sprite_icon.spriteName = currentEquipmentElement.icon;
		inpt_maxSpeed.text = currentEquipmentElement.maxSpeed.ToString ();
		inpt_minSpeed.text = currentEquipmentElement.minSpeed.ToString ();
		inpt_name.text = currentEquipmentElement.name;
		inpt_sp.text = currentEquipmentElement.sp.ToString ();
		inpt_spRegen.text = currentEquipmentElement.spRegen.ToString ();
		inpt_spRegenDelay.text = currentEquipmentElement.spRegenDelay.ToString ();
		inpt_strafe.text = currentEquipmentElement.strafe.ToString ();
		inpt_weight.text = currentEquipmentElement.weight.ToString ();
		inpt_cooldown.text = currentEquipmentElement.cooldown.ToString ();
		inpt_damageBonus.text = currentEquipmentElement.damageBonus.ToString ();
		inpt_damageMultiplier.text = currentEquipmentElement.damageMultiplier.ToString ();
		inpt_energyCost.text = currentEquipmentElement.energyCost.ToString ();
		
		switch (currentEquipmentElement.type) {
		case EquipmentType.Engine:
			poplst_type.selection = "Engine";
			break;
		case EquipmentType.Hull:
			poplst_type.selection = "Hull";
			break;
		case EquipmentType.Weapon:
			poplst_type.selection = "Weapon";
			break;
		case EquipmentType.Misc:
			poplst_type.selection = "Misc";
			break;
		}
		
		RefreshAmmoList ();
		poplst_ammo.selection = "-";
		poplst_ammo.selection = poplst_ammo.items [0];
	}
	
	void OnSaveBtnClick ()
	{
		if (!EditorMenu.Instance.equipmentSaveCollection.equipment.Contains (currentEquipmentElement)) {
			EditorMenu.Instance.equipmentSaveCollection.equipment.Add (currentEquipmentElement);
		}
		EditorMenu.Instance.resourceManager.Save ("equipment.xml", EditorMenu.Instance.equipmentSaveCollection, typeof(EquipmentSaveCollection));
	}
	
	void OnBackBtnClick ()
	{
		EditorMenu.FlipPanels (EditorMenu.Instance.area_equipmentPanel, EditorMenu.Instance.area_menuPanel);
	}
	
	#region Equipment properties
	void OnIconSelectBtnClick ()
	{
		EditorMenu.Instance.ShowSpriteSelectionDialog (EditorMenu.Instance.iconAtlas, "OnIconWasSelected", gameObject, EditorMenu.Instance.area_equipmentPanel);
	}
	
	void OnIconWasSelected (string selection)
	{
		sprite_icon.spriteName = selection;
		currentEquipmentElement.icon = selection;
	}
	
	void OnNameInput (UIInput sender)
	{
		EquipmentSaveData selectedElement = EditorMenu.Instance.equipmentSaveCollection.equipment.Find (delegate(EquipmentSaveData esd)
		{
			return esd.name == currentEquipmentElement.name;
		}
		);
		if (sender.text.Equals ("New Equipment", System.StringComparison.OrdinalIgnoreCase)) {
			lbl_status.text = "Wrong Name! Set another.";
			lbl_status.animation.Play ();
		} else if (selectedElement == null || selectedElement == currentEquipmentElement) {
			currentEquipmentElement.name = sender.text;
		} else {
			lbl_status.text = "Wrong Name! Already exists.";
			lbl_status.animation.Play ();
		}
	}
	
	void OnTypeSelectionChange (string selection)
	{
		switch (poplst_type.selection) {
		case "Engine":
			currentEquipmentElement.type = EquipmentType.Engine;
			break;
		case "Hull":
			currentEquipmentElement.type = EquipmentType.Hull;
			break;
		case "Weapon":
			currentEquipmentElement.type = EquipmentType.Weapon;
			break;
		case "Misc":
			currentEquipmentElement.type = EquipmentType.Misc;
			break;
		default:
			currentEquipmentElement.type = EquipmentType.Misc;
			break;
		}
	}
	
	void OnAccelerationInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.acceleration);
	}
	
	void OnEpInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.ep);
	}
	
	void OnEpRegenInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.epRegen);	
	}
	
	void OnHPInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.hp);
	}
	
	void OnHPRegenInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.hpRegen);
	}
	
	void OnMaxSpeedInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.maxSpeed);
	}
	
	void OnMinSpeedInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.minSpeed);
	}
	
	void OnSPInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.sp);
	}
	
	void OnSPRegenInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.spRegen);
	}
	
	void OnSPRegenDelayInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.spRegenDelay);
	}
	
	void OnStrafeInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.strafe);
	}
	
	void OnWeightInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.weight);
	}
	
	void OnRemoveAmmoBtnClick ()
	{
		int removingItem = int.Parse (poplst_ammo.selection);
		
		if (currentEquipmentElement.ammo.Count > 1) {
			currentEquipmentElement.ammo.RemoveAt (removingItem);
			RefreshAmmoList ();
			poplst_ammo.selection = "-";
			poplst_ammo.selection = (removingItem > 0 ? removingItem - 1 : 0).ToString ();
		}
	}
	
	void OnAmmoSelectBtnClick ()
	{
		EditorMenu.Instance.ShowAmmoSelectionDialog (false, EditorMenu.Instance.ammoSaveCollection.ammo, "AmmoSelectedInDialog", gameObject, EditorMenu.Instance.area_equipmentPanel);
	}
	
	void AmmoSelectedInDialog (string selection)
	{
		currentEquipmentElement.ammo [int.Parse (poplst_ammo.selection)] = selection;
		sprite_ammo.spriteName = selection;
		lbl_ammoName.text = selection;
	}
	
	void OnCooldownInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.cooldown);
	}
	
	void OnDamageBonusInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.damageBonus);
	}
	
	void OnDamageMultiplierInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.damageMultiplier);
	}
	
	void OnEnergyCostInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentEquipmentElement.energyCost);
	}
	
	void OnAmmoSelectionChange (string selection)
	{
		if (selection == "-") {
			return;
		} else if (selection == "ADD..") {
			currentEquipmentElement.ammo.Add ("none");
			RefreshAmmoList ();
			poplst_ammo.selection = "-";
			poplst_ammo.selection = poplst_ammo.items [poplst_ammo.items.Count - 2];
		} else {
			int selectedAmmo = int.Parse (selection);
			lbl_ammoName.text = currentEquipmentElement.ammo [selectedAmmo];
			sprite_ammo.spriteName = lbl_ammoName.text;
		}
	}
	
	void RefreshAmmoList ()
	{
		poplst_ammo.items.Clear ();
		for (int i = 0; i < currentEquipmentElement.ammo.Count; i++) {
			poplst_ammo.items.Add (i.ToString ());
		}
		poplst_ammo.items.Add ("ADD..");
	}
	#endregion
}
