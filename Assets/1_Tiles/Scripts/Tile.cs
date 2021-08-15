using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isObstacle = false;
    [SerializeField] bool isHazard = false;
    [SerializeField] bool canBeBridged = false;

    [SerializeField] GameObject placeableObject;

    // member variables
    private Vector2Int coordinates;
    public GameObject placedObject = null;

    // cache
    Pathfinding_GridManager gridManager;
    PlaceableObjectsPool placeablePool;

    private void Awake()
    {
        // cache
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        placeablePool = FindObjectOfType<PlaceableObjectsPool>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetGridCoordinatesFromWorldPos(this.transform.position);
            SetCorrespondingNodeProperties();
        }

    }

    private void SetCorrespondingNodeProperties()
    {
        if (isObstacle)
        {
            gridManager.SetWalkable(coordinates, false);
        }

        if (isHazard)
        {
            gridManager.SetHazard(coordinates, true);
        }

        if (canBeBridged)
        {
            gridManager.SetBridge(coordinates, true);
        }
    }

    private void OnMouseDown()
    {
        // if there is a kodama on the tile, do nothing
        if (gridManager.doesTileHaveKodamaOnIt(coordinates)) { return; }

        // if tile is empty, place
        if (placedObject == null)
        {

            Placeable placeable = placeablePool.SelectedPlaceable ? .GetComponent<Placeable>();

            if (placeable == null) { return; }

            if (placeable.CanBePlaced(coordinates))
            {
                placedObject = placeable.Place(coordinates);
            }
        }
        // if tile has placeable, remove instead
        else
        {
            Placeable placed = placedObject.GetComponent<Placeable>();
            placed.Remove(coordinates);
            placedObject = null;
        }
    }

}