using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Waypoint : MonoBehaviour
{
    [SerializeField] bool canAcceptTower = true;
    public bool CanAcceptTower { get { return canAcceptTower; } }
    
    [SerializeField] GameObject objectToBePlaced;

    private void OnMouseDown()
    {
        if (canAcceptTower)
        {
            Instantiate(objectToBePlaced, transform.position, Quaternion.identity);
            canAcceptTower = false;
        }
    }
}
