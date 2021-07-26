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

    Dictionary<Vector2Int, Pathfinding_Node> exploredDict = new Dictionary<Vector2Int, Pathfinding_Node>();
    Queue<Pathfinding_Node> exploringQueue = new Queue<Pathfinding_Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    // cache
    Dictionary<Vector2Int, Pathfinding_Node> gridDict;

    private void Awake()
    {
        // cache
         gridDict = FindObjectOfType<Pathfinding_Grid>().GridDict;

    }

    private void Start()
    {
        // intialise
        startNode = gridDict[startCoordinates];
        endNode = gridDict[endCoordinates];



        BreadthFirstSearch();
    }

    private List<Pathfinding_Node> FindNeighbours(Pathfinding_Node currentNode)
    {
        List<Pathfinding_Node> neighbours = new List<Pathfinding_Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int investigatedCoordinates = currentNode.coordinates + direction;
            if (gridDict.ContainsKey(investigatedCoordinates))
            {
                neighbours.Add(gridDict[investigatedCoordinates]);
            }
        }

        return neighbours;
    }

    private void BreadthFirstSearch()
    {
        bool isDone = false;

        // start from the first node
        exploringQueue.Enqueue(startNode);

        // while there is still a queue
        while (exploringQueue.Count > 0 && !isDone)
        {
            // take the first queued node
            currentSearchedNode = exploringQueue.Dequeue();
            exploredDict.Add(currentSearchedNode.coordinates, currentSearchedNode);

            // check if it's out target
            if (currentSearchedNode == endNode)
            {
                isDone = true;
                BacktrackPath();
            }

            // if not done, 
            foreach (Pathfinding_Node neighbour in FindNeighbours(currentSearchedNode))
            {
                if (!exploredDict.ContainsKey(neighbour.coordinates)
                    && !exploringQueue.Contains(neighbour))
                {
                    neighbour.reachedFromNode = currentSearchedNode;
                    exploringQueue.Enqueue(neighbour);
                }
            }
            // mark node as exlored
            currentSearchedNode.isExplored = true;
        }
    }

    private void BacktrackPath()
    {
        bool pathCompleted = false;
        while (!pathCompleted)
        {
            Path.Push(currentSearchedNode);
            currentSearchedNode.isPath = true;
            currentSearchedNode = currentSearchedNode.reachedFromNode;

            if (currentSearchedNode == startNode)
            {
                pathCompleted = true;
            }
        }
    }
}
