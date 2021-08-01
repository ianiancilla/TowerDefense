using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int endCoordinates;

    // member variables
    Pathfinding_Node startNode;
    Pathfinding_Node endNode;
    Pathfinding_Node currentSearchedNode;
    List<Pathfinding_Node> path = new List<Pathfinding_Node>();

    Dictionary<Vector2Int, Pathfinding_Node> reachedDict = new Dictionary<Vector2Int, Pathfinding_Node>();
    Queue<Pathfinding_Node> exploringQueue = new Queue<Pathfinding_Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    // cache
    Pathfinding_GridManager gridManager;

    private void Awake()
    {
        // cache
         gridManager = FindObjectOfType<Pathfinding_GridManager>();

        // intialise
        startNode = gridManager.GetNode(startCoordinates);
        endNode = gridManager.GetNode(endCoordinates);

        SetNewPath();
    }

    private void Start()
    {
    }

    public List<Pathfinding_Node> GetPath_BreadthFirstSearch()
    {
        bool isDone = false;

        List<Pathfinding_Node> tempPath = new List<Pathfinding_Node>();

        reachedDict.Clear();
        exploringQueue.Clear();

        // start from the first node
        exploringQueue.Enqueue(startNode);
        reachedDict.Add(startCoordinates, startNode);


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
                tempPath = BacktrackPath(currentSearchedNode);
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

        return tempPath;
    }

    public List<Pathfinding_Node> GetPath_BreadthFirstSearch(Vector2Int startPointGridCoordinates)
    {
        bool isDone = false;

        List<Pathfinding_Node> tempPath = new List<Pathfinding_Node>();

        reachedDict.Clear();
        exploringQueue.Clear();

        // start from the first node
        exploringQueue.Enqueue(gridManager.GetNode(startPointGridCoordinates));
        reachedDict.Add(startPointGridCoordinates, gridManager.GetNode(startPointGridCoordinates));


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
                tempPath = BacktrackPath(currentSearchedNode);
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

        return tempPath;
    }

    private List<Pathfinding_Node> BacktrackPath(Pathfinding_Node currentNode)
    {
        List<Pathfinding_Node> tempPath = new List<Pathfinding_Node>();

        do 
        {
            tempPath.Add(currentNode);
            currentNode.isPath = true;
            currentNode = currentNode.reachedFromNode;
        } while (currentNode != null);

        tempPath.Reverse();

        return tempPath;
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

    private void SetNodesAsPath()
    {
        foreach (Pathfinding_Node node in path)
        {
            node.isPath = true;
        }
    }

    /// <summary>
    /// Checks if blocking a given tile will make the startpoint > endpoint path set
    /// in the Pathfinder script impossible.
    /// </summary>
    /// <param name="coordinates">Tile coordinates of the tile which would be blocked.</param>
    /// <returns></returns>
    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (gridManager.GetNode(coordinates) == null) { return false; }

        // temporarily sets as unwalkable, checks for new path, sets it back
        bool previousWalkableState = gridManager.GetNode(coordinates).isWalkable;
        gridManager.SetWalkable(coordinates, false);
        var tempPath = GetPath_BreadthFirstSearch();

        gridManager.SetWalkable(coordinates, previousWalkableState);

        if (tempPath.Count <= 1) { return true; }
        else { return false; }
    }
    
    public void SetNewPath()
    {
        gridManager.ResetPathfindingProperties();
        path = GetPath_BreadthFirstSearch();
        SetNodesAsPath();
    }

    public void BroadcastRecalculatePath()
    {
        BroadcastMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
    }
}
