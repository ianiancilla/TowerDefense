using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable_Bridge : MonoBehaviour, Placeable
{
    // cache
    Pathfinding_GridManager gridManager;
    Pathfinding_Pathfinder pathfinder;
    PlaceableObjectsPool placeablePool;

    public bool CanBePlaced(Vector2Int coordinates)
    {
        // needs to be here as methods are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();
        placeablePool = FindObjectOfType<PlaceableObjectsPool>();

        if (gridManager.GetNode(coordinates) == null) { return false; }

        // if there is an available INACTIVE placeable of this kind in the pool
        if (placeablePool.pool.Count == 0) { return false; }
        if (FindObjectInPool() == null) { return false; }

        // if node is accessible and can be bridged

        bool isWalkable = gridManager.GetNode(coordinates).IsWalkable;
        bool canBeBridged = gridManager.GetNode(coordinates).CanBeBridged;

        return isWalkable && canBeBridged;
    }

    public GameObject Place(Vector2Int coordinates)
    {
        // needs to be here as methods are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        placeablePool = FindObjectOfType<PlaceableObjectsPool>();

        gridManager.SetHazard(coordinates, false);

        Vector3 worldPos = gridManager.GetWorldPosFromGridCoordinates(coordinates);
        // find available in pool
        GameObject placedObject = FindObjectInPool();

        // move and activate
        placedObject.transform.position = worldPos;
        placedObject.SetActive(true);

        return placedObject;
    }

    public void Remove(Vector2Int coordinates)
    {
        // needs to be here as all other methods in class are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();

        gridManager.SetHazard(coordinates, true);

        this.gameObject.SetActive(false);
    }

    private GameObject FindObjectInPool()
    {
        // find object in pool
        GameObject placedObject = null;
        foreach (GameObject go in placeablePool.pool)
        {
            // if it is inactive (not already in use) and of the right type
            if (!go.activeSelf && go.GetComponent<Placeable_Bridge>() != null)
            {
                placedObject = go;
                break;
            }
        }

        return placedObject;
    }
}
