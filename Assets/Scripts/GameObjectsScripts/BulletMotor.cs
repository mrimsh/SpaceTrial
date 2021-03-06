using UnityEngine;
using System.Collections;

public class BulletMotor : SpaceObject
{
	[HideInInspector]
	public float damage;
	[HideInInspector]
	public SpaceShipMotor sourceShip;
	private float birthTime;
	
	public AmmoSaveData AmmoOriginData {
		get {
			return _ammoOriginData;
		}
		set {
			_ammoOriginData = value;
			Sprite.spriteName = "none";
			Sprite.spriteName = value.sprite;
			damage = _ammoOriginData.damage;
		}
	}

	public UISprite Sprite {
		get {
			if (_sprite == null) {
				_sprite = transform.Find ("sprite").GetComponent<UISprite> ();
			}
			return _sprite;
		}
	}

	private AmmoSaveData _ammoOriginData;
	private UISprite _sprite;
	
	protected override void Start ()
	{
		base.Start ();
		birthTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (birthTime + _ammoOriginData.lifetime > Time.time) {
			// Move forward
			transform.localPosition += transform.localRotation * new Vector3 (0, AmmoOriginData.speed * Time.deltaTime, 0);
			// Is bullet moved out of screen border
			if (transform.localPosition.x > GameManager.SCREENWIDTH * 0.5f) {
				transform.localPosition = new Vector3 (-GameManager.SCREENWIDTH * 0.5f, transform.localPosition.y, transform.localPosition.z);
			} else if (transform.localPosition.x < -GameManager.SCREENWIDTH * 0.5f) {
				transform.localPosition = new Vector3 (GameManager.SCREENWIDTH * 0.5f, transform.localPosition.y, transform.localPosition.z);
			}
		} else {
			Selfdestruct (DamageSource.Unknown);
		}
	}
	
	public override void OnTriggerStay (Collider otherCollider)
	{
		// Send messages to all rigidbodies, that was collided
		otherCollider.SendMessage ("BulletWasCollided", this, SendMessageOptions.DontRequireReceiver);
	}
	
	public override void SOCollided (SpaceObject collidedObject)
	{
	}
	
	/// <summary>
	/// Call this function, when this bullet has been catched.
	/// </summary>
	public void BulletCatched ()
	{
		Selfdestruct (DamageSource.Unknown);
	}
}
