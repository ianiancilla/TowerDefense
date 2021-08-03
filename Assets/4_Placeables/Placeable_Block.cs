using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable_Block : MonoBehaviour, Placeable
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

        // if node is accessible and would not block path entirely

        bool isWalkable = gridManager.GetNode(coordinates).IsWalkable;
        bool willBlockAnyKodama = pathfinder.WillBlockAnyPath(coordinates);

        return isWalkable && !willBlockAnyKodama;
    }

    public GameObject Place(Vector2Int coordinates)
    {
        // needs to be here as methods are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();

        gridManager.SetWalkable(coordinates, false);
        pathfinder.BroadcastRecalculatePath();

        Vector3 worldPos = gridManager.GetWorldPosFromGridCoordinates(coordinates);
        GameObject instance = Instantiate(this.gameObject, worldPos, Quaternion.identity);

        return instance;
    }

    public void Remove(Vector2Int coordinates)
    {
        // needs to be here as all other methods in class are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();

        gridManager.SetWalkable(coordinates, true);
        pathfinder.BroadcastRecalculatePath();

        Destroy(this.gameObject);
    }
}
