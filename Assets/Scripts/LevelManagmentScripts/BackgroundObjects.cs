using UnityEngine;
using System.Collections;

[System.Serializable]
public class BackgroundObjects
{
	public enum deltaSizeType
	{
		 @add,
		multiply
	}
	
	public string sprite;
	public GameObject objectPrefab;
	public float maxCountOnScreen;
	public Vector3 normalSize;
	public Vector3 deltaSize;
	public deltaSizeType typeOfResizing = deltaSizeType.@add;
	[HideInInspector]
	public int countOfObjectsOnScreen;
}
