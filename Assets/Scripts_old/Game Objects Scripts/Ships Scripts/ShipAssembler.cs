using UnityEngine;
using System.Collections;

[AddComponentMenu("Ships Components/Ship Assembler")]

public class ShipAssembler : MonoBehaviour
{
    private GameObject ship, mesh;
	
	/// <summary>
	/// Constructs new ship. 
	/// </summary>
	/// <param name="shipPrefabName">
	/// A <see cref="System.String"/> ship prefab name, that used to create a ship properties and subobjects from prefab.
	/// </param>
	/// <param name="meshContainerPrefabName">
	/// A <see cref="System.String"/> ship mesh namem that used to create a object with MeshRenderer.
	/// </param>
	/// <returns>
	/// A <see cref="SpaceShipMotor"/> assigned to a new ship.
	/// </returns>
    public SpaceShipMotor_old AssembleShip(string shipPrefabName, string meshContainerPrefabName)
    {
        GameObject ship = (GameObject)Instantiate(Resources.Load("Ships/Prefabs/" + shipPrefabName + "/" + shipPrefabName));
        ship.name = shipPrefabName;
        ship.transform.parent = this.transform;
        SpaceShipMotor_old shipMotor = ship.GetComponent<SpaceShipMotor_old>();

        GameObject meshContainer = new GameObject("Mesh Container");
        meshContainer.transform.parent = ship.transform;
        shipMotor.shipMeshContainer = meshContainer;

        GameObject meshPrefab = (GameObject)Instantiate(Resources.Load("Ships/Meshes/" + meshContainerPrefabName + "/Mesh"));
        meshPrefab.transform.parent = meshContainer.transform;

        ship.GetComponent<Destroyable>().targetToDestroy = gameObject;

        Destroy(this);
        return shipMotor;
    }
}
