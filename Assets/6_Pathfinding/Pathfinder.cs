using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // member variables
    [SerializeField] Pathfinding_Node currentSearchedNode;
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    // cache
    Pathfinding_Grid grid;

    private void Start()
    {
        // cache
        grid = FindObjectOfType<Pathfinding_Grid>();

        // TODO remove
        FindNeighbours();
    }

    private void FindNeighbours()
    {
        List<Pathfinding_Node> neighbours = new List<Pathfinding_Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int investigatedCoordinates = currentSearchedNode.coordinates + direction;
            if (grid.GetNode(investigatedCoordinates) != null)
            {
                neighbours.Add(grid.GetNode(investigatedCoordinates));
            }
        }
    }
}
