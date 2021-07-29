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
    Stack<Pathfinding_Node> Path = new Stack<Pathfinding_Node>();

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

        BreadthFirstSearch();
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

    private void BreadthFirstSearch()
    {
        bool isDone = false;

        

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
                BacktrackPath();
                isDone = true;
                break;
            }

            // if not done:
            foreach (Pathfinding_Node neighbour in FindNeighbours(currentSearchedNode))
            {
                if (!reachedDict.ContainsKey(neighbour.coordinates))
                {
                    neighbour.reachedFromNode = currentSearchedNode;
                    exploringQueue.Enqueue(neighbour);
                    reachedDict.Add(neighbour.coordinates, neighbour);
                }
            }
        }
    }

    private void BacktrackPath()
    {
        do 
        {
            Path.Push(currentSearchedNode);
            currentSearchedNode.isPath = true;
            currentSearchedNode = currentSearchedNode.reachedFromNode;
        } while (currentSearchedNode != null);
    }
}
