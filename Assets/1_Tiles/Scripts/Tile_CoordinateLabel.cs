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

    //member variables
    Vector2Int coordinates = new Vector2Int();

    //cache
    TMP_Text label;
    Pathfinding_Grid grid;

    private void OnEnable()
    {
        // cache
        label = GetComponent<TMP_Text>();
        grid = FindObjectOfType<Pathfinding_Grid>();

        // initialise
        UpdateCoordinatesAndNodeLink();
        UpdateLabelAndName();
        label.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        ToggleLabelVisibility();
        UpdateLabelColor();

        // only in Editor mode
        if (!Application.isPlaying)
        {
            UpdateCoordinatesAndNodeLink();
            UpdateLabelAndName();
            label.enabled = true;
        }
    }

    private void UpdateCoordinatesAndNodeLink()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        if (!grid.GridDict.ContainsKey(coordinates))
        {
            return;
        }
    }

    private void UpdateLabelAndName()
    {
        // label text
        string stringCoordinates = coordinates.ToString();
        label.text = stringCoordinates;
        transform.parent.name = stringCoordinates;
    }

    private void UpdateLabelColor()
    {
        if (!grid) { return; }

        Pathfinding_Node correspondingNode = grid.GetNode(coordinates);
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
