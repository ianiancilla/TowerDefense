using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kodama : MonoBehaviour
{
    // cache 
    Pathfinding_GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
    }

    public Vector2Int GetCurrentGridTile()
    {
        Vector3 worldPos = transform.position;

        return gridManager.GetGridCoordinatesFromWorldPos(worldPos);
    }
}
