using UnityEngine;
using System.Collections;

[AddComponentMenu("Level Components/Background Generator")]

public class BGGenerator : MonoBehaviour
{
	public int bgObjectsAtOneScreen = 30;
	/// <summary>
	///	List of objects to generate at background. 
	/// </summary>
	public BackgroundObjects[] bgObjects;
	public UIAtlas bgObjectsAtlas;
	public GameObject parentPanel;
	private GameObject currBGScreen, nextBGScreen;
	private float halfScreenWidth;
	private float halfScreenHeight;

	// Use this for initialization
	void Start ()
	{
		halfScreenWidth = GameManager.SCREENWIDTH * 0.5f;
		halfScreenHeight = GameManager.SCREENHEIGHT * 0.5f;
		Debug.Log (halfScreenWidth + "x" + halfScreenHeight);
		currBGScreen = CreateBGScreen ();
		nextBGScreen = CreateBGScreen ();
		currBGScreen.transform.localPosition = new Vector3 (0, -100f, 1);
		nextBGScreen.transform.localPosition = new Vector3 (0, (halfScreenHeight * 2) - 100f, 1);
	}

	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.playerShip != null) {
			if (GameManager.Instance.playerShip.transform.localPosition.y > nextBGScreen.transform.localPosition.y) {
				Destroy (currBGScreen, 1f);
				currBGScreen = nextBGScreen;
				nextBGScreen = CreateBGScreen ();
				nextBGScreen.transform.localPosition = new Vector3 (0, currBGScreen.transform.localPosition.y + (halfScreenHeight * 2), 0);
			}
		}
	}

	public GameObject CreateBGScreen ()
	{
		GameObject newBGScreen = new GameObject ("bgscreen");
		newBGScreen.layer = LayerMask.NameToLayer ("GameArea");
		newBGScreen.transform.parent = parentPanel.transform;
		newBGScreen.transform.localScale = Vector3.one;
		int nextIndex;
		
		for (int i = 0; i < bgObjectsAtOneScreen; i++) {
			nextIndex = Random.Range (0, bgObjects.Length - 1);
			
			UISprite newBGObject = new GameObject (bgObjects [nextIndex].sprite, typeof(UISprite)).GetComponent<UISprite> ();
			newBGObject.gameObject.layer = LayerMask.NameToLayer ("GameArea");
			newBGObject.atlas = bgObjectsAtlas;
			newBGObject.spriteName = bgObjects [nextIndex].sprite;
			newBGObject.transform.parent = newBGScreen.transform;
			newBGObject.transform.localPosition = new Vector3 (Random.Range (-halfScreenWidth, halfScreenWidth), Random.Range (0, halfScreenHeight * 2), 0);
			
			switch (bgObjects [nextIndex].typeOfResizing) {
			case BackgroundObjects.deltaSizeType.@add:
				newBGObject.transform.localScale = 
					bgObjects [nextIndex].normalSize + new Vector3 (
							Random.Range (0, bgObjects [nextIndex].deltaSize.x), 
							Random.Range (0, bgObjects [nextIndex].deltaSize.y),
							Random.Range (0, bgObjects [nextIndex].deltaSize.z));
				break;
			case BackgroundObjects.deltaSizeType.multiply:
				newBGObject.transform.localScale = 
					Vector3.Scale (bgObjects [nextIndex].normalSize, new Vector3 (
						Random.Range (1, bgObjects [nextIndex].deltaSize.x),
						Random.Range (1, bgObjects [nextIndex].deltaSize.y),
						Random.Range (1, bgObjects [nextIndex].deltaSize.z)));
				break;
			}
		}
		
		return newBGScreen;
	}
}
