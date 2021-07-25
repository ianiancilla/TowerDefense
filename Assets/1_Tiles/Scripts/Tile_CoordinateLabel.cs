using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
[ExecuteAlways]
public class Tile_CoordinateLabel : MonoBehaviour
{
    // properties
    [SerializeField] Color labelColorWalkable = Color.white;
    [SerializeField] Color labelColorNotWalkable = Color.red;
    [SerializeField] Color labelColorExplored = Color.yellow;
    [SerializeField] Color labelColorPath = Color.blue;

    [SerializeField] KeyCode toggleCoordinateLabelKey = KeyCode.Tab;

    //cache
    TMP_Text label;
    Pathfinding_Grid grid;
    //REMOVED Tile_Waypoint waypoint;

    //member variables
    Vector2Int coordinates = new Vector2Int();
    Pathfinding_Node correspondingNode;

    private void Awake()
    {
        // cache
        label = GetComponent<TMP_Text>();
        grid = FindObjectOfType<Pathfinding_Grid>();
        //REMOVED waypoint = GetComponentInParent<Tile_Waypoint>();


        // initialise
        UpdateCoordinates();
        UpdateLabelAndName();
        if (Application.isPlaying) { label.enabled = false; }
    }


    // Update is called once per frame
    void Update()
    {
        ColorCoordinatesLabel();
        ToggleLabelVisibility();

        // only in Editor mode
        if (!Application.isPlaying)
        {
            UpdateCoordinates();
            UpdateLabelAndName();
        }
    }

    private void UpdateCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        correspondingNode = grid.GetNode(coordinates);
    }

    private void UpdateLabelAndName()
    {
        // label text
        string stringCoordinates = coordinates.ToString();
        label.text = stringCoordinates;
        transform.parent.name = stringCoordinates;
    }

    private void ColorCoordinatesLabel()
    {
        if (!grid) { return; }
        if (correspondingNode == null) { return; }


        // label color
        if (!correspondingNode.isWalkable) { label.color = labelColorNotWalkable; }
        else if (correspondingNode.isPath) { label.color = labelColorPath; }
        else if (correspondingNode.isExplored) { label.color = labelColorExplored; }
        else { label.color = labelColorWalkable; }

    }

    private void ToggleLabelVisibility()
    {
        if (Input.GetKeyDown(toggleCoordinateLabelKey))
        {
            label.enabled = !label.IsActive();
        }
    }

}
