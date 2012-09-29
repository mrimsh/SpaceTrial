using UnityEngine;
using System.Collections;

public class AmmoEditor : TypicalEditor
{
	public UIInput inpt_damage,
	inpt_splashRadius,
	inpt_cooldownMultiplier,
	inpt_cooldownBonus,
	inpt_lifetime,
	inpt_speed;
	public UISprite sprite_sprite;
	public UICheckbox chk_isHoming,
	chk_isSelfDestruct;
	private AmmoSaveData currentAmmo;
	
	void Awake ()
	{
		int r = Random.Range (1000, 9999);
		currentAmmo = new AmmoSaveData ("Equipment" + r);
	}
	
	public void Load (AmmoSaveData ammo)
	{
		currentAmmo = ammo;
		inpt_cooldownBonus.text = currentAmmo.cooldownBonus.ToString ();
		inpt_cooldownMultiplier.text = currentAmmo.cooldownMultiplier.ToString ();
		inpt_damage.text = currentAmmo.damage.ToString ();
		sprite_icon.spriteName = currentAmmo.icon;
		inpt_name.text = currentAmmo.name;
		inpt_lifetime.text = currentAmmo.lifetime.ToString ();
		inpt_splashRadius.text = currentAmmo.splashRadius.ToString ();
		inpt_speed.text = currentAmmo.speed.ToString ();
		sprite_sprite.spriteName = currentAmmo.sprite;
		chk_isHoming.isChecked = currentAmmo.isHoming;
		chk_isSelfDestruct.isChecked = currentAmmo.isSelfDestruct;
	}
	
	void OnSaveBtnClick ()
	{
		if (!EditorMenu.Instance.ammoSaveCollection.ammo.Contains (currentAmmo)) {
			EditorMenu.Instance.ammoSaveCollection.ammo.Add (currentAmmo);
		}
		EditorMenu.Instance.resourceManager.Save ("ammo.xml", EditorMenu.Instance.ammoSaveCollection, typeof(AmmoSaveCollection));
	}
	
	void OnBackBtnClick ()
	{
		EditorMenu.FlipPanels (EditorMenu.Instance.area_ammoPanel, EditorMenu.Instance.area_menuPanel);
	}
	
	void OnNameInput (UIInput sender)
	{
		AmmoSaveData selectedAmmo = EditorMenu.Instance.ammoSaveCollection.ammo.Find (delegate(AmmoSaveData asd)
		{
			return asd.name == currentAmmo.name;
		}
		);
		if (sender.text.Equals ("New Ammo", System.StringComparison.OrdinalIgnoreCase)) {
			lbl_status.text = "Wrong Name! Set another.";
			lbl_status.animation.Play ();
		} else if (selectedAmmo == null || selectedAmmo == currentAmmo) {
			currentAmmo.name = sender.text;
		} else {
			lbl_status.text = "Wrong Name! Already exists.";
			lbl_status.animation.Play ();
		}
	}
	
	void OnCooldownBonusInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentAmmo.cooldownBonus);
	}
	
	void OnCooldownMultiplierInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentAmmo.cooldownMultiplier);
	}
	
	void OnDamageInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentAmmo.damage);	
	}
	
	void OnIconSelectBtnClick ()
	{
		EditorMenu.Instance.ShowSpriteSelectionDialog (EditorMenu.Instance.bulletSpriteAtlas, "OnIconWasSelected", gameObject, EditorMenu.Instance.area_ammoPanel);
	}
	
	void OnIconWasSelected (string selection)
	{
		sprite_icon.spriteName = selection;
		currentAmmo.icon = selection;
	}
	
	void OnSpriteSelectBtnClick ()
	{
		EditorMenu.Instance.ShowSpriteSelectionDialog (EditorMenu.Instance.bulletSpriteAtlas, "OnSpriteWasSelected", gameObject, EditorMenu.Instance.area_ammoPanel);
	}
	
	void OnSpriteWasSelected (string selection)
	{
		sprite_sprite.spriteName = selection;
		currentAmmo.sprite = selection;
	}
	
	void OnLifetimeInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentAmmo.lifetime);
	}
	
	void OnSpeedInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentAmmo.speed);
	}
	
	void OnSplashRadiusInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentAmmo.splashRadius);
	}
	
	void OnIsHomingActivate (bool state)
	{
		currentAmmo.isHoming = state;
	}
	
	void OnIsSelfDestructActivate (bool state)
	{
		currentAmmo.isSelfDestruct = state;
	}
}
