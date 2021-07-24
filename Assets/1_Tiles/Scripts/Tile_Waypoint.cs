using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Waypoint : MonoBehaviour
{
    [SerializeField] bool canAcceptTower = true;
    public bool CanAcceptTower { get { return canAcceptTower; } }
    
    [SerializeField] Tower tower;

    private void OnMouseDown()
    {
        if (canAcceptTower)
        {
            bool isPlaced = tower.CreateTower(tower, transform.position);
            canAcceptTower = !isPlaced;
        }
    }
}
