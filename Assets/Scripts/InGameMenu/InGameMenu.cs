using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour
{
	public UILabel lbl_speed;
	public UISlider prbar_hp, prbar_sp, prbar_ep, prbar_speed;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.playerShip != null) {
			SpaceShipMotor playerShip = GameManager.Instance.playerShip;
			lbl_speed.text = ((int)playerShip.CurrentSpeed).ToString ();
			prbar_hp.sliderValue = playerShip.CurrentHP / playerShip.maxHP;
			prbar_sp.sliderValue = playerShip.CurrentSP / playerShip.maxSP;
			prbar_ep.sliderValue = playerShip.CurrentEP / playerShip.maxEP;
			prbar_speed.sliderValue = playerShip.CurrentSpeed / playerShip.maxSpeed;
		}
	}
	
	void OnMenuBtnClick ()
	{
		Application.LoadLevel ("MainMenu");
	}
	
	void OnAutoFireActivate (bool isChecked)
	{
		PlayerController.Instance.SetAutoFire (isChecked);
	}
}
