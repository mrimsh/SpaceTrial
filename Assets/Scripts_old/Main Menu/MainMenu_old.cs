using UnityEngine;
using System.Collections;

[AddComponentMenu("Menu Components/Main Menu")]

public class MainMenu_old : MonoBehaviour
{
	public const int GT_CAMPAIGN = 0;
	public const int GT_RANDOM = 1;
	public const int GT_SURVIVAL = 2;
	public const int GT_CUSTOM = 3;
	public const int GD_CASUAL = 0;
	public const int GD_HARDCORE = 1;

	public GUISkin guiskin;
	Rect[] areas = { new Rect (426 * 0.5f - 60f, 80, 120, 500) };
	MenuState currentMenuState = MenuState.MainMenuSelectionScreen;
	TextAsset changelog, help;
	private string[] gameTypes = {"Random"}, gameDifficulties = {"Casual", "Hardcore"};
	/// <summary>
	/// Game type identificator: <see cref="GT_CAMPAIGN"/>, <see cref="GT_RANDOM"/>, <see cref="GT_SURVIVAL"/>, <see cref="GT_CUSTOM"/>.
	/// </summary>
	private int gameTypeID;
	/// <summary>
	/// Game difficulty identificator: <see cref="GD_CASUAL"/>, <see cref="GD_HARDCORE"/>.
	/// </summary>
	private int gameDifficultyID;

	public enum MenuState
	{
		MainMenuSelectionScreen,
		NewGameSelectionScreen,
		Extras,
		HelpScreen,
		ChangelogScreen
	}

	// Use this for initialization
	void Start ()
	{
		changelog = (TextAsset)Resources.Load ("Other/Changelog", typeof(TextAsset));
		help = (TextAsset)Resources.Load ("Other/Help", typeof(TextAsset));
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void OnGUI ()
	{
		GUI.skin = guiskin;
		DrawMenuElements (currentMenuState);
	}

	void DrawMenuElements (MenuState menuState)
	{
		switch (menuState) {
		case MenuState.MainMenuSelectionScreen:
			GUILayout.BeginArea (areas[0]);
			GUILayout.BeginVertical ();
			if (GUILayout.Button ("Start")) {
				currentMenuState = MenuState.NewGameSelectionScreen;
			}
			if (GUILayout.Button ("Game Editor")) {
				Application.LoadLevel ("GameEditor");
			}
			if (GUILayout.Button ("Extras")) {
				currentMenuState = MenuState.Extras;
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
			break;
		case MenuState.NewGameSelectionScreen:
			GUILayout.Space (80);
			
			GUILayout.Label("Select Game Type", "Label2");
			GUILayout.BeginHorizontal ();
			gameTypeID = GUILayout.SelectionGrid(gameTypeID, gameTypes, 2, "Radio");
			GUILayout.EndHorizontal ();
			
			GUILayout.Label("Choose Difficulty Mode", "Label2");
			GUILayout.BeginHorizontal ();
			gameDifficultyID = GUILayout.SelectionGrid(gameDifficultyID, gameDifficulties, 2, "Radio");
			GUILayout.EndHorizontal ();
			
			GUILayout.Space (80);
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Back..")) {
				currentMenuState = MenuState.MainMenuSelectionScreen;
			}
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Start Game")) {
				//PlayerPrefs.SetInt ("GameTypeID", gameTypeID);
				PlayerPrefs.SetInt ("GameTypeID", 1);
				PlayerPrefs.SetInt ("GameDifficultyID", gameDifficultyID);
				Application.LoadLevel ("Game");
			}
			GUILayout.EndHorizontal ();
			
			/*
			GUILayout.BeginVertical ();
			if (GUILayout.Button ("Casual")) {
				PlayerPrefs.SetString ("NextGameMode", "Casual");
				Application.LoadLevel ("Game");
			}
			if (GUILayout.Button ("Hardcore")) {
				PlayerPrefs.SetString ("NextGameMode", "Hardcore");
				Application.LoadLevel ("Game");
			}
			if (GUILayout.Button ("Back..")) {
				currentMenuState = MenuState.MainMenuSelectionScreen;
			}
			GUILayout.EndVertical ();
			
			GUILayout.BeginVertical ();
			if (GUILayout.Button ("Casual")) {
				PlayerPrefs.SetString ("NextGameMode", "Casual");
				Application.LoadLevel ("Game");
			}
			if (GUILayout.Button ("Hardcore")) {
				PlayerPrefs.SetString ("NextGameMode", "Hardcore");
				Application.LoadLevel ("Game");
			}
			if (GUILayout.Button ("Back..")) {
				currentMenuState = MenuState.MainMenuSelectionScreen;
			}
			GUILayout.EndVertical ();
			
			GUILayout.EndHorizontal ();
			*/
			
			break;
		case MenuState.Extras:
			GUILayout.BeginArea (areas[0]);
			GUILayout.BeginVertical ();
			if (GUILayout.Button ("Help")) {
				currentMenuState = MenuState.HelpScreen;
			}
			if (GUILayout.Button ("Changelog")) {
				currentMenuState = MenuState.ChangelogScreen;
			}
			if (GUILayout.Button ("Back..")) {
				currentMenuState = MenuState.MainMenuSelectionScreen;
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
			break;
		case MenuState.HelpScreen:
			GUILayout.BeginVertical ();
			GUILayout.Box (help.text);
			if (GUILayout.Button ("Back..")) {
				currentMenuState = MenuState.MainMenuSelectionScreen;
			}
			GUILayout.EndVertical ();
			break;
		case MenuState.ChangelogScreen:
			GUILayout.BeginVertical ();
			GUILayout.Box (changelog.text);
			if (GUILayout.Button ("Back..")) {
				currentMenuState = MenuState.MainMenuSelectionScreen;
			}
			GUILayout.EndVertical ();
			break;
		}
		
	}
}
