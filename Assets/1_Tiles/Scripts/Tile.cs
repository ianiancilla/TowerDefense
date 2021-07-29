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

    private void Awake()
    {
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetGridCoordinatesFromWorldPos(this.transform.position);

            if (!CanAcceptTower)
            {
                gridManager.BlockNode(coordinates);
            }
        }

    }

    private void OnMouseDown()
    {
        if (canAcceptTower)
        {
            bool isPlaced = tower.CreateTower(tower, transform.position);
            
        }
    }
}
