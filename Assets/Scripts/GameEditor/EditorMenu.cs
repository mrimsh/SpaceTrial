using UnityEngine;
using System.Collections;


[AddComponentMenu("Menu Components/Editor Menu")]

public class EditorMenu : MonoBehaviour
{

	string newShipName = "New Ship";

	public enum GameEditorState
	{
		GameEditorMainMenu,
		CreateNewShip,
		EditShip
	}
	Rect[] windowsSizes = { new Rect (50, 50, 200, 80) };

	GameEditorState currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
	public GameObject newShipTemplate;

	public GUISkin guiskin;

	void OnGUI ()
	{
		GUI.skin = guiskin;
		DrawMenuElements (currentGameEditorState);
	}

	void DrawMenuElements (GameEditorState gameEditorState)
	{
		switch (gameEditorState) {
		
		///////////////////
		/// MAIN SCREEN ///
		///////////////////
		case GameEditorState.GameEditorMainMenu:
			
			GUILayout.BeginVertical ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create New Ship")) {
				currentGameEditorState = EditorMenu.GameEditorState.CreateNewShip;
			}
			if (GUILayout.Button ("Edit Custom Ship")) {
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create New Ship Mesh")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			if (GUILayout.Button ("Edit Custom Ship Mesh")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create New Weapon")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			if (GUILayout.Button ("Edit Custom Weapon")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create New Bullet")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			if (GUILayout.Button ("Edit Custom Bullet")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Back")) {
				Application.LoadLevel ("MainMenu");
			}
			
			GUILayout.EndVertical ();
			break;
		
		/////////////////////
		/// NEW SHIP EDIT ///
		/////////////////////
		case GameEditorState.CreateNewShip:
			
			windowsSizes[0] = GUI.Window (0, windowsSizes[0], DrawInGameMenuWindow, "Enter new ship name");
			
			break;
		
		/////////////////
		/// SHIP EDIT ///
		/////////////////
		case GameEditorState.EditShip:
			GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
			
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			
			if (GUILayout.Button ("Cancel")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Create")) {
				currentGameEditorState = EditorMenu.GameEditorState.EditShip;
			}
			GUILayout.EndHorizontal ();
			GUILayout.EndVertical ();
			break;
		}
	}

	private void DrawInGameMenuWindow (int id)
	{
		switch (id) {
		case 0:
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Name");
			newShipName = GUILayout.TextField (newShipName, 32, GUILayout.Width (120));
			GUILayout.EndHorizontal ();
			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Cancel")) {
				currentGameEditorState = EditorMenu.GameEditorState.GameEditorMainMenu;
			}
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Create")) {
				currentGameEditorState = EditorMenu.GameEditorState.EditShip;
			}
			GUILayout.EndHorizontal ();
			GUI.DragWindow ();
			break;
		}
	}
}
