using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_Pathfinder : MonoBehaviour
{
    // member variables
    Vector2Int endCoordinates;
    Pathfinding_Node endNode;

    // pathfinding variables
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    // cache
    Pathfinding_GridManager gridManager;

    private void Awake()
    {
        // cache
        gridManager = FindObjectOfType<Pathfinding_GridManager>();

        // find Shrine
        GameObject shrine = GameObject.FindGameObjectWithTag("Shrine");
        endCoordinates = gridManager.GetGridCoordinatesFromWorldPos(shrine.transform.position);
        endNode = gridManager.GetNode(endCoordinates);

    }

    public List<Pathfinding_Node> GetPath_BreadthFirstSearch(Vector2Int startPointGridCoordinates)
    {
        // for storing the found path
        List<Pathfinding_Node> path = new List<Pathfinding_Node>();

        // dictionary of cells that have been reached (not necessarily explored)
        Dictionary<Vector2Int, Pathfinding_Node> reachedDict = new Dictionary<Vector2Int, Pathfinding_Node>();
        
        // Queue of cells that need to be explored
        Queue<Pathfinding_Node> exploringQueue = new Queue<Pathfinding_Node>();

        bool isDone = false;

        Pathfinding_Node startNode = gridManager.GetNode(startPointGridCoordinates);
        Pathfinding_Node currentSearchedNode;


        // start from the first node
        exploringQueue.Enqueue(startNode);
        reachedDict.Add(startPointGridCoordinates, startNode);


        // while there is still a queue
        while (exploringQueue.Count > 0 && !isDone)
        {
            // take the first queued node
            currentSearchedNode = exploringQueue.Dequeue();
            // mark node as exlored
            currentSearchedNode.isExplored = true;

            // check if it's our target
            if (currentSearchedNode == endNode)
            {
                // backtrack the path to fill stack then exit
                path = BacktrackPath(currentSearchedNode);
                isDone = true;
                break;
            }

            // if not finished, explore new neighbours:
            foreach (Pathfinding_Node neighbour in FindNeighbours(currentSearchedNode))
            {
                if (!reachedDict.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
                {
                    neighbour.reachedFromNode = currentSearchedNode;
                    exploringQueue.Enqueue(neighbour);
                    reachedDict.Add(neighbour.coordinates, neighbour);
                }
            }
        }

        // needed to avoid infinite loop as all nodes will be explored and have a connection!!!
        gridManager.ResetPathfindingProperties();

        return path;
    }

    private List<Pathfinding_Node> BacktrackPath(Pathfinding_Node currentNode)
    {
        List<Pathfinding_Node> path = new List<Pathfinding_Node>();

        do 
        {
            path.Add(currentNode);
            currentNode.isPath = true;
            currentNode = currentNode.reachedFromNode;
        } while (currentNode != null);

        path.Reverse();

        return path;
    }

    private List<Pathfinding_Node> FindNeighbours(Pathfinding_Node currentNode)
    {
        List<Pathfinding_Node> neighbours = new List<Pathfinding_Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int investigatedCoordinates = currentNode.coordinates + direction;
            if (gridManager.GetNode(investigatedCoordinates) != null)
            {
                neighbours.Add(gridManager.GetNode(investigatedCoordinates));
            }
        }

        return neighbours;
    }

    private void SetNodesAsPath(List<Pathfinding_Node> path)
    {
        foreach (Pathfinding_Node node in path)
        {
            node.isPath = true;
        }
    }

    /// <summary>
    /// Checks if blocking a given tile will make the current point > endpoint path set
    /// in the Pathfinder script impossible.
    /// </summary>
    /// <param name="blockedCoordinates">Tile coordinates of the tile which would be blocked.</param>
    /// <returns></returns>
    public bool WillBlockPath(Vector2Int blockedCoordinates, Vector2Int startPointOfCheck)
    {
        if (gridManager.GetNode(blockedCoordinates) == null) { return false; }

        // temporarily sets as unwalkable, checks for new path, sets it back
        bool previousWalkableState = gridManager.GetNode(blockedCoordinates).isWalkable;
        gridManager.SetWalkable(blockedCoordinates, false);

        var tempPath = GetPath_BreadthFirstSearch(startPointOfCheck);

        gridManager.SetWalkable(blockedCoordinates, previousWalkableState);

        if (tempPath.Count <= 1) { return true; }
        else { return false; }
    }
    
    public void BroadcastRecalculatePath()
    {
        BroadcastMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
    }
}
