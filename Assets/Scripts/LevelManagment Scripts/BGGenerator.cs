using UnityEngine;
using System.Collections;

[AddComponentMenu("Level Components/Background Generator")]

public class BGGenerator : MonoBehaviour
{
	public LevelProperties levelProperties;
	private GameObject currBGScreen, nextBGScreen;

	// Use this for initialization
	void Start ()
	{
		currBGScreen = CreateBGScreen ();
		nextBGScreen = CreateBGScreen ();
		currBGScreen.transform.position = Vector3.zero;
		nextBGScreen.transform.position = new Vector3 (0, 200f, 0);
	}

	// Update is called once per frame
	void Update ()
	{
		try {
			if (levelProperties.playerController.ship.transform.position.y > nextBGScreen.transform.position.y) {
				Destroy (currBGScreen, 1f);
				currBGScreen = nextBGScreen;
				nextBGScreen = CreateBGScreen ();
				nextBGScreen.transform.position = new Vector3 (0, currBGScreen.transform.position.y + 200f, 0);
			}
		} catch (MissingReferenceException e) {
			Debug.Log (e);
		}
	}

	public GameObject CreateBGScreen ()
	{
		GameObject newBGScreen = new GameObject ("bgscreen");
		int nextIndex;
		
		for (int i = 0; i < levelProperties.bgObjectsAtOneScreen; i++) {
			nextIndex = Random.Range (0, levelProperties.bgObjects.Length - 1);
			
			GameObject newBGObject = (GameObject)Instantiate (levelProperties.bgObjects[nextIndex].objectPrefab);
			newBGObject.transform.parent = newBGScreen.transform;
			newBGObject.transform.localPosition = new Vector3 (Random.Range (-60, 60), Random.Range (0, 200), 0);
			
			switch (levelProperties.bgObjects[nextIndex].typeOfResizing) {
			case BackgroundObjects.deltaSizeType.@add:
				newBGObject.transform.localScale = levelProperties.bgObjects[nextIndex].normalSize + new Vector3 (Random.Range (0, levelProperties.bgObjects[nextIndex].deltaSize.x), Random.Range (0, levelProperties.bgObjects[nextIndex].deltaSize.y), Random.Range (0, levelProperties.bgObjects[nextIndex].deltaSize.z));
				break;
			case BackgroundObjects.deltaSizeType.multiply:
				newBGObject.transform.localScale = Vector3.Scale (levelProperties.bgObjects[nextIndex].normalSize, new Vector3 (Random.Range (1, levelProperties.bgObjects[nextIndex].deltaSize.x), Random.Range (1, levelProperties.bgObjects[nextIndex].deltaSize.y), Random.Range (1, levelProperties.bgObjects[nextIndex].deltaSize.z)));
				break;
			}
		}
		
		return newBGScreen;
	}
}
