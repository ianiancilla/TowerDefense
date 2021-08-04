using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable_Bridge : MonoBehaviour, Placeable
{
    // cache
    Pathfinding_GridManager gridManager;
    Pathfinding_Pathfinder pathfinder;

    public bool CanBePlaced(Vector2Int coordinates)
    {
        // needs to be here as methods are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();


        if (gridManager.GetNode(coordinates) == null) { return false; }

        // if node is accessible and can be bridged

        bool isWalkable = gridManager.GetNode(coordinates).IsWalkable;
        bool canBeBridged = gridManager.GetNode(coordinates).CanBeBridged;

        return isWalkable && canBeBridged;
    }

    public GameObject Place(Vector2Int coordinates)
    {
        // needs to be here as methods are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();

        gridManager.SetHazard(coordinates, false);

        Vector3 worldPos = gridManager.GetWorldPosFromGridCoordinates(coordinates);
        GameObject instance = Instantiate(this.gameObject, worldPos, Quaternion.identity);

        return instance;
    }

    public void Remove(Vector2Int coordinates)
    {
        // needs to be here as all other methods in class are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();

        gridManager.SetHazard(coordinates, true);

        Destroy(this.gameObject);
    }
}
