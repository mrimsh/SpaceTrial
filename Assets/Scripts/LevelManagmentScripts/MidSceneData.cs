using UnityEngine;
using System.Collections;

public class MidSceneData : MonoBehaviour
{
	#region Singletone
	public static MidSceneData Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.Find ("MidSceneData").GetComponent<MidSceneData> ();
			}
			return _instance;
		}
	}
	
	private static MidSceneData _instance;
	#endregion
	
	//[HideInInspector]
	public LevelSaveData selectedLevel;
	//[HideInInspector]
	public string selectedInitialShip;
	//[HideInInspector]
	public ShipsSaveCollection shipsInLevel;
	//[HideInInspector]
	public EquipmentSaveCollection equipmentInLevel;
	//[HideInInspector]
	public AmmoSaveCollection ammoInLevel;
	
	void Awake ()
	{
		_instance = this;
		DontDestroyOnLoad (gameObject);
	}
}
