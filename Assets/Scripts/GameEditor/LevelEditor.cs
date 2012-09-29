using UnityEngine;
using System.Collections;

public class LevelEditor : TypicalEditor
{
	public UIInput inpt_spaceObjectName,
	inpt_spaceObjectsAtScreen,
	inpt_spaceObjectsGenerationFrequency,
	inpt_spaceObjectsInLevel,
	inpt_vc_DistanceToPass,
	inpt_vc_ShipsNumToDestroy;
	public UIPopupList poplst_spaceObjects;
	private LevelSaveData currentLevel;
	
	void Start ()
	{
		int r = Random.Range (1000, 9999);
		currentLevel = new LevelSaveData ("Level" + r);
	}
	
	public void Load (LevelSaveData level)
	{
		currentLevel = level;
		sprite_icon.spriteName = level.icon;
		inpt_name.text = level.name;
		inpt_spaceObjectsAtScreen.text = level.spaceObjectsAtScreen.ToString ();
		inpt_spaceObjectsGenerationFrequency.text = level.spaceObjectsGenerationFrequency.ToString ();
		inpt_spaceObjectsInLevel.text = level.spaceObjectsInLevel.ToString ();
		inpt_vc_DistanceToPass.text = level.vc_DistanceToPass.ToString ();
		inpt_vc_ShipsNumToDestroy.text = level.vc_ShipsNumToDestroy.ToString ();
		
		RefreshSpaceObjectsList ();
		poplst_spaceObjects.selection = "-";
		poplst_spaceObjects.selection = poplst_spaceObjects.items [0];
	}
	
	void OnSaveBtnClick ()
	{
		if (!EditorMenu.Instance.levelSaveCollection.levels.Contains (currentLevel)) {
			EditorMenu.Instance.levelSaveCollection.levels.Add (currentLevel);
		}
		EditorMenu.Instance.resourceManager.Save ("levels.xml", EditorMenu.Instance.levelSaveCollection, typeof(LevelsSaveCollection));
	}
	
	void OnBackBtnClick ()
	{
		EditorMenu.FlipPanels (EditorMenu.Instance.area_levelPanel, EditorMenu.Instance.area_menuPanel);
	}
	
	void OnRemoveSpaceObjectBtnClick ()
	{
		int removingItem = int.Parse (poplst_spaceObjects.selection);
		
		if (currentLevel.spaceObjects.Count > 1) {
			currentLevel.spaceObjects.RemoveAt (removingItem);
			RefreshSpaceObjectsList ();
			poplst_spaceObjects.selection = "-";
			poplst_spaceObjects.selection = (removingItem > 0 ? removingItem - 1 : 0).ToString ();
		}
	}
	
	void OnNameInput (UIInput sender)
	{
		LevelSaveData selectedElement = EditorMenu.Instance.levelSaveCollection.levels.Find (delegate(LevelSaveData lsd)
		{
			return lsd.name == currentLevel.name;
		}
		);
		if (sender.text.Equals ("New Level", System.StringComparison.OrdinalIgnoreCase)) {
			lbl_status.text = "Wrong Name! Set another.";
			lbl_status.animation.Play ();
		} else if (selectedElement == null || selectedElement == currentLevel) {
			currentLevel.name = sender.text;
		} else {
			lbl_status.text = "Wrong Name! Already exists.";
			lbl_status.animation.Play ();
		}
	}
	
	void OnIconSelectBtnClick ()
	{
		EditorMenu.Instance.ShowSpriteSelectionDialog (EditorMenu.Instance.iconAtlas, "OnIconWasSelected", gameObject, EditorMenu.Instance.area_levelPanel);
	}
	
	void OnIconWasSelected (string selection)
	{
		sprite_icon.spriteName = selection;
		currentLevel.icon = selection;
	}
	
	void OnSpaceObjectInput (UIInput sender)
	{
		currentLevel.spaceObjects [int.Parse (poplst_spaceObjects.selection)] = sender.text;
	}
	
	void OnSpaceObjectsAtScreenInput (UIInput sender)
	{
		CheckIntInput (sender, ref currentLevel.spaceObjectsAtScreen);
	}
	
	void OnSpaceObjectsGenerationFrequencyInput (UIInput sender)
	{
		CheckFloatInput (sender, ref currentLevel.spaceObjectsGenerationFrequency);
	}
	
	void OnSpaceObjectsInLevelInput (UIInput sender)
	{
		CheckIntInput (sender, ref currentLevel.spaceObjectsInLevel);
	}
	
	void OnDistanceToPassInput (UIInput sender)
	{
		CheckIntInput (sender, ref currentLevel.vc_DistanceToPass);
	}
	
	void OnShipsNumToDestroyInput (UIInput sender)
	{
		CheckIntInput (sender, ref currentLevel.vc_ShipsNumToDestroy);
	}
	
	void OnSpaceObjectsSelectionChange (string selection)
	{
		if (selection == "-") {
			return;
		} else if (selection == "ADD..") {
			currentLevel.spaceObjects.Add ("");
			RefreshSpaceObjectsList ();
			poplst_spaceObjects.selection = "-";
			poplst_spaceObjects.selection = poplst_spaceObjects.items [poplst_spaceObjects.items.Count - 2];
		} else {
			int selectedSpaceObject = int.Parse (selection);
			inpt_spaceObjectName.text = currentLevel.spaceObjects [selectedSpaceObject];
		}
	}
	
	void RefreshSpaceObjectsList ()
	{
		poplst_spaceObjects.items.Clear ();
		for (int i = 0; i < currentLevel.spaceObjects.Count; i++) {
			poplst_spaceObjects.items.Add (i.ToString ());
		}
		poplst_spaceObjects.items.Add ("ADD..");
	}
}
