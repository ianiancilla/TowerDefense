using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pathfinding_Node
{
    // member variables
    public Vector2Int coordinates;

    public bool isWalkable;
    public bool isExplored = false;
    public bool isPath = false;
    public Pathfinding_Node reachedFromNode;

    // constructor
    public Pathfinding_Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
