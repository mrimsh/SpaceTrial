using UnityEngine;
using System.Collections;

[AddComponentMenu("Ships Components/Destroyable")]

public class Destroyable : MonoBehaviour
{
    public bool isDestroyable = true;
    [HideInInspector]
    public GameObject targetToDestroy;
}
