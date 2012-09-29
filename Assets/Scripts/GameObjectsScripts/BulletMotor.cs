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
	
	// Use this for initialization
	void Start ()
	{
		birthTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (birthTime + _ammoOriginData.lifetime > Time.time) {
			// Move forward
			transform.localPosition += transform.localRotation * new Vector3 (0, AmmoOriginData.speed * Time.deltaTime, 0);
			// Is bullet moved out of screen border
			if (transform.localPosition.x > GameManager.Instance.rightBorder) {
				transform.localPosition = new Vector3 (GameManager.Instance.leftBorder, transform.localPosition.y, transform.localPosition.z);
			} else if (transform.localPosition.x < GameManager.Instance.leftBorder) {
				transform.localPosition = new Vector3 (GameManager.Instance.rightBorder, transform.localPosition.y, transform.localPosition.z);
			}
		} else {
			Selfdestruct ();
		}
	}
	
	void OnTriggerStay (Collider otherCollider)
	{
		// Send messages to all rigidbodies, that was collided
		otherCollider.SendMessage ("BulletWasCollided", this, SendMessageOptions.DontRequireReceiver);
	}
	
	/// <summary>
	/// Call this function, when this bullet has been catched.
	/// </summary>
	public void BulletCatched ()
	{
		Selfdestruct ();
	}
	
	private void Selfdestruct ()
	{
		Destroy (gameObject);
	}
}
