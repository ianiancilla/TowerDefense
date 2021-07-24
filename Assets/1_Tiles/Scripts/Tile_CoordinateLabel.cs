using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Tile_CoordinateLabel : MonoBehaviour
{
    // properties
    [SerializeField] Color labelColorCanPlace = Color.blue;
    [SerializeField] Color labelColorCannotPlace = Color.red;
    [SerializeField] KeyCode toggleCoordinateLabelKey = KeyCode.Tab;

    //cache
    TMP_Text label;
    Tile_Waypoint waypoint;

    //member variables
    Vector2Int coordinates = new Vector2Int();


    private void Awake()
    {
        // cache
        label = GetComponent<TMP_Text>();
        waypoint = GetComponentInParent<Tile_Waypoint>();


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
        // label color
        if (waypoint.CanAcceptTower)
        {
            label.color = labelColorCanPlace;
        }
        else
        {
            label.color = labelColorCannotPlace;
        }
    }

    private void ToggleLabelVisibility()
    {
        if (Input.GetKeyDown(toggleCoordinateLabelKey))
        {
            label.enabled = !label.IsActive();
        }
    }

}
