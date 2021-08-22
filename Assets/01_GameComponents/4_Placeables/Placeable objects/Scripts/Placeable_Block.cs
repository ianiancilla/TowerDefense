using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;


public class Placeable_Block : MonoBehaviour, Placeable
{
    [SerializeField] MMFeedback placingFeedback;
    [SerializeField] MMFeedback removingFeedback;


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

        // if node is accessible AND would not block path entirely
        // AND cannot be bridged (no stones on pitfalls)
        bool isWalkable = gridManager.GetNode(coordinates).IsWalkable;
        bool willBlockAnyKodama = pathfinder.WillBlockAnyPath(coordinates);
        bool canBeBridged = gridManager.GetNode(coordinates).CanBeBridged;

        return isWalkable && !willBlockAnyKodama && !canBeBridged;
    }

    public GameObject Place(Vector2Int coordinates)
    {
        // needs to be here as methods are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();
        placeablePool = FindObjectOfType<PlaceableObjectsPool>();


        gridManager.SetWalkable(coordinates, false);
        pathfinder.BroadcastRecalculatePath();

        Vector3 worldPos = gridManager.GetWorldPosFromGridCoordinates(coordinates);
        // find available in pool
        GameObject placedObject = FindObjectInPool();

        // move and activate
        placedObject.transform.position = worldPos;
        placedObject.SetActive(true);

        // feedback
        if (placingFeedback)
        {
            placingFeedback.Initialization(this.gameObject);
            placingFeedback.Play(this.transform.position);
        }

        return placedObject;
    }

    public void Remove(Vector2Int coordinates)
    {
        // needs to be here as all other methods in class are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();

        gridManager.SetWalkable(coordinates, true);
        pathfinder.BroadcastRecalculatePath();

        // feedback
        removingFeedback?.Play(this.transform.position);

        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the node at coordinates to have the correct stats for this type of placeable
    /// </summary>
    public void SetNodeToPlaceableStats(Vector2Int coordinates)
    {
        // needs to be here as methods are called before object is instantiated
        gridManager = FindObjectOfType<Pathfinding_GridManager>();

        gridManager.SetWalkable(coordinates, false);
    }

    private GameObject FindObjectInPool()
    {
        // find object in pool
        GameObject placedObject = null;
        foreach (GameObject go in placeablePool.pool)
        {
            // if it is inactive (not already in use) and of the right type
            if (!go.activeSelf && go.GetComponent<Placeable_Block>() != null)
            {
                placedObject = go;
                break;
            }
        }

        return placedObject;
    }

}