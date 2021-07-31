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
    Stack<Pathfinding_Node> path = new Stack<Pathfinding_Node>();

    Dictionary<Vector2Int, Pathfinding_Node> reachedDict = new Dictionary<Vector2Int, Pathfinding_Node>();
    Queue<Pathfinding_Node> exploringQueue = new Queue<Pathfinding_Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    // cache
    Pathfinding_GridManager gridManager;

    private void Awake()
    {
        // cache
         gridManager = FindObjectOfType<Pathfinding_GridManager>();
    }

    private void Start()
    {
        // intialise
        startNode = gridManager.GetNode(startCoordinates);
        endNode = gridManager.GetNode(endCoordinates);

        ResetPath();
    }

    private Stack<Pathfinding_Node> GetPath_BreadthFirstSearch()
    {
        bool isDone = false;

        Stack<Pathfinding_Node> tempPath = new Stack<Pathfinding_Node>();

        reachedDict.Clear();
        exploringQueue.Clear();

        // start from the first node
        exploringQueue.Enqueue(startNode);
        reachedDict.Add(startNode.coordinates, startNode);


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

    private Stack<Pathfinding_Node> BacktrackPath(Pathfinding_Node currentNode)
    {
        Stack<Pathfinding_Node> tempPath = new Stack<Pathfinding_Node>();

        do 
        {
            tempPath.Push(currentNode);
            currentNode.isPath = true;
            currentNode = currentNode.reachedFromNode;
        } while (currentNode != null);

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

    private void SetPath()
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
    
    public void ResetPath()
    {
        gridManager.ResetPathfindingProperties();
        path = GetPath_BreadthFirstSearch();
        SetPath();
    }
}
