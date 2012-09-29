using UnityEngine;
using System.Collections;

public class JoyBtn : MonoBehaviour
{
	private Vector3 startPosition;
	public float touchRadius = 20f;
	public GameObject messageTarget;
	public string functionName = "JoystickMoved";
	public MovingRestriction movingRestriction;
	
	void Start ()
	{
		startPosition = transform.localPosition;
	}
	
	void Update ()
	{	
		if (messageTarget != null) {
			messageTarget.SendMessage (functionName, ((transform.localPosition - startPosition) / touchRadius), SendMessageOptions.DontRequireReceiver);
		}
	}
	
	void OnPress (bool pressed)
	{
		if (pressed) {
			startPosition = transform.localPosition;
		} else {
			transform.localPosition = startPosition;
		}
	}
	
	void OnDrag (Vector2 delta)
	{
		switch (movingRestriction) {
		case MovingRestriction.None:
			transform.localPosition += new Vector3 (delta.x, delta.y, 0);
			break;
		case MovingRestriction.Horizontal:
			transform.localPosition += new Vector3 (delta.x, 0, 0);
			break;
		case MovingRestriction.Vertical:
			transform.localPosition += new Vector3 (0, delta.y, 0);
			break;
		}
		Vector3 offsetDelta = transform.localPosition - startPosition;
		if (offsetDelta.sqrMagnitude > touchRadius * touchRadius) {
			transform.localPosition = startPosition + offsetDelta.normalized * touchRadius;
		}
	}
}

public enum MovingRestriction
{
	None,
	Horizontal,
	Vertical
}