using UnityEngine;
using System.Collections;

[AddComponentMenu("Other Objects Components/Space Object")]

public class SpaceObject : MonoBehaviour
{
	public LevelProperties levelProperties;
	
	public virtual void CatchBullet (BulletProperties bulletProperties)
	{
	}
}
