using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Tile_CoordinateLabel : MonoBehaviour
{
    //cache
    TMP_Text label;

    //member variables
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        // cache
        label = GetComponent<TMP_Text>();

        UpdateCoordinates();
        UpdateLabelAndName();
    }

    // Update is called once per frame
    void Update()
    {
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
        string stringCoordinates = coordinates.ToString();
        label.text = stringCoordinates;
        transform.parent.name = stringCoordinates;
    }
}
