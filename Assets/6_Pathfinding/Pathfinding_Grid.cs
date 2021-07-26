using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_Grid : MonoBehaviour
{
    // properties
    [SerializeField] Vector2Int gridSize;

    // member variables
    Dictionary<Vector2Int, Pathfinding_Node> gridDict = new Dictionary<Vector2Int, Pathfinding_Node>();
    public Dictionary<Vector2Int, Pathfinding_Node> GridDict { get { return gridDict; } }

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        // walk all grid coordinates
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int worldPos = new Vector2Int(x, y);

                Pathfinding_Node newNode = new Pathfinding_Node(worldPos,
                                                                true);

                gridDict[worldPos] = newNode;
            }
        }
    }

    public Pathfinding_Node GetNode(Vector2Int coordinates)
    {
        if (gridDict.ContainsKey(coordinates))
        {
            return gridDict[coordinates];
        }
        return null;
    }


}