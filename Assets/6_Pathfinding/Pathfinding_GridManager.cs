using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_GridManager : MonoBehaviour
{
    // properties
    [Tooltip ("The size of a tile")]
    [SerializeField] private int gridUnitSize = 5;
    public int GridUnitSize { get { return gridUnitSize; } }

    [Tooltip("How many tiles are in the grid on x and y")]
    [SerializeField] private Vector2Int gridSizeInTiles;

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
        for (int x = 0; x < gridSizeInTiles.x; x++)
        {
            for (int y = 0; y < gridSizeInTiles.y; y++)
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

    public void BlockNode (Vector2Int coordinates)
    {
        if (gridDict.ContainsKey(coordinates))
        {
            gridDict[coordinates].isWalkable = false;
        }
    }

    public void ResetPathfindingProperties()
    {
        foreach (KeyValuePair<Vector2Int, Pathfinding_Node> entry in gridDict)
        {
            entry.Value.reachedFromNode = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetGridCoordinatesFromWorldPos(Vector3 worldPos)
    {
        Vector2Int gridCoordinates = new Vector2Int();

        gridCoordinates.x = Mathf.RoundToInt(worldPos.x / GridUnitSize);
        gridCoordinates.y = Mathf.RoundToInt(worldPos.z / GridUnitSize);

        return gridCoordinates;
    }

    public Vector3 GetWorldPosFromGridCoordinates (Vector2 gridCoordinates)
    {
        Vector3 worldPos = new Vector3(gridCoordinates.x * GridUnitSize,
                                       0,
                                       gridCoordinates.y * GridUnitSize);

        return worldPos;
    }

}
