using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pathfinding_Node
{
    // member variables
    public Vector2Int coordinates;

    // state 
    private bool isWalkable;
    public bool IsWalkable { get { return isWalkable; } }

    private bool isHazard;
    public bool IsHazard { get { return isHazard; } }


    // pathfinding
    public bool isExplored = false;
    public Pathfinding_Node reachedFromNode;

    // constructor
    public Pathfinding_Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }

    // setters
    public void SetWalkable(bool newWalkable)
    {
        isWalkable = newWalkable;
    }

    public void SetHazard(bool newHazard)
    {
        isHazard = newHazard;
    }

}
