using UnityEngine;
using System.Collections;

[AddComponentMenu("Ships Components/Equipment/Weapons/Bullets/Typical Bullet")]

public class TypicalBullet : SpaceObject
{

    public BulletProperties properties;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, properties.lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 400 * Time.deltaTime, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != properties.master.gameObject)
        {
            other.gameObject.GetComponent<SpaceObject>().CatchBullet(properties);
            Destroy(gameObject);
        }
    }
}
