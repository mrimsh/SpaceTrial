using UnityEngine;
using System.Collections;

public class TypicalEditor : MonoBehaviour
{
	public UILabel lbl_status;
	public UIInput inpt_name;
	public UISprite sprite_icon;
	
	public bool CheckFloatInput (UIInput input, ref float valueReference)
	{
		float parsedValue = 0f;
		
		if (!float.TryParse (input.text, out parsedValue)) {
			lbl_status.text = "Error input!\\nEnter correct float.";
			lbl_status.animation.Stop ();
			lbl_status.animation.Play ();
			input.text = valueReference.ToString ();
			return false;
		} else {
			valueReference = parsedValue;
			return true;
		}
	}

	public bool CheckIntInput (UIInput input, ref int valueReference)
	{
		int parsedValue = 0;
		
		if (!int.TryParse (input.text, out parsedValue)) {
			lbl_status.text = "Error input!\\nEnter correct integer.";
			lbl_status.animation.Stop ();
			lbl_status.animation.Play ();
			input.text = valueReference.ToString ();
			return false;
		} else {
			valueReference = parsedValue;
			return true;
		}
	}
}
