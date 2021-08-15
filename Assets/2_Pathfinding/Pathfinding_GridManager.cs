using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_GridManager : MonoBehaviour
{
    // properties
    [Tooltip ("The size of a tile")]
    [SerializeField] private int gridUnitSize = 5;
    public int GridUnitSize { get { return gridUnitSize; } }

    // member variables
    private Vector2Int gridSizeInTiles;

    Dictionary<Vector2Int, Pathfinding_Node> gridDict = new Dictionary<Vector2Int, Pathfinding_Node>();
    public Dictionary<Vector2Int, Pathfinding_Node> GridDict { get { return gridDict; } }

    private void Awake()
    {
        SetGridSize();
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

    public void SetWalkable (Vector2Int coordinates, bool newWalkable)
    {
        if (gridDict.ContainsKey(coordinates))
        {
            gridDict[coordinates].SetWalkable(newWalkable);
        }
    }

    public void SetHazard(Vector2Int coordinates, bool newHazard)
    {
        if (gridDict.ContainsKey(coordinates))
        {
            gridDict[coordinates].SetHazard(newHazard);
        }
    }
    public void SetBridge(Vector2Int coordinates, bool newState)
    {
        if (gridDict.ContainsKey(coordinates))
        {
            gridDict[coordinates].SetCanBeBridged(newState);
        }
    }


    public void ResetPathfindingProperties()
    {
        foreach (KeyValuePair<Vector2Int, Pathfinding_Node> entry in gridDict)
        {
            entry.Value.reachedFromNode = null;
            entry.Value.isExplored = false;
            //entry.Value.isPath = false;
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
    public bool doesTileHaveKodamaOnIt(Vector2Int coordinates)
    {
        Kodama[] kodamas = FindObjectsOfType<Kodama>(false);

        foreach (Kodama kodama in kodamas)
        {
            Vector2Int kodamaGridPos = kodama.GetCurrentGridTile();
            if (kodamaGridPos == coordinates) { return true; }
        }

        return false;
    }

    private void SetGridSize()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        List<Vector2> coordinates = new List<Vector2>();

        int maxXCoo = 0;
        int maxYCoo = 0;

        foreach (Tile tile in tiles)
        {
            Vector2Int tileCoo = GetGridCoordinatesFromWorldPos(tile.transform.position);

            // check for tile overlap
            if (coordinates.Contains(tileCoo))
            {
                Debug.Log("WARNING: Overlapping tiles at " + tileCoo.ToString());
            }

            if (tileCoo.x > maxXCoo) { maxXCoo = tileCoo.x; }
            if (tileCoo.y > maxYCoo) { maxYCoo = tileCoo.y; }

            coordinates.Add(tileCoo);
        }

        if ( ! coordinates.Contains(Vector2Int.zero))
        {
            Debug.LogError("ERROR: Grid must start at 0,0");
        }

        gridSizeInTiles = new Vector2Int(maxXCoo + 1,
                                         maxYCoo + 1);

    }
}
