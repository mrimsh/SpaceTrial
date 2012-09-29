using UnityEngine;
using System.Collections;

public class HangarGUIManager : MonoBehaviour {
	
	public GUISkin skin;
	public Rect[] buttonsBounds; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUI.skin = skin; 
		GUI.Button (buttonsBounds[0], "Back Button", "HangarLeftButton");
		GUI.Button (buttonsBounds[1], "Middle Button", "HangarMiddleButton");
		GUI.Button (buttonsBounds[2], "Next Button", "HangarRightButton");
	}
}
