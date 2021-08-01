using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower tower;
    [SerializeField] bool canAcceptTower = true;
    public bool CanAcceptTower { get { return canAcceptTower; } }


    // member variables
    private Vector2Int coordinates;

    // cache
    Pathfinding_GridManager gridManager;
    Pathfinder pathfinder;

    private void Awake()
    {
        // cache
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetGridCoordinatesFromWorldPos(this.transform.position);

            if (!CanAcceptTower)
            {
                gridManager.SetWalkable(coordinates, false);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates) == null) { return; }

        // if node is accessible and would not block path entirely
        if (gridManager.GetNode(coordinates).isWalkable
            && !pathfinder.WillBlockPath(coordinates))
        {
            bool isPlaced = true;//tower.CreateTower(tower, transform.position);
            gridManager.SetWalkable(coordinates, !isPlaced);
            pathfinder.SetNewPath();
            pathfinder.BroadcastRecalculatePath();
        }
    }
}
