using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MPUIKIT;

public class PlaceableButtonUI : MonoBehaviour
{
    // properties
    [SerializeField] string myPlaceableTag;
    [SerializeField] Color selectedOutlineColour = Color.red;

    // variables
    Color defaultOutlineColour;

    // cache
    PlaceableObjectsPool placeablePool;
    TMP_Text myTextbox;
    MPImage myMPImage;

    // Start is called before the first frame update
    void Start()
    {
        // cache
        placeablePool = FindObjectOfType<PlaceableObjectsPool>();
        myTextbox = GetComponentInChildren<TMP_Text>();
        myMPImage = GetComponent<MPImage>();

        defaultOutlineColour = myMPImage.OutlineColor;

        RefreshUI();
    }

    // Update is called once per frame
    public void RefreshUI()
    {
        UpdatePlaceableCount();
        SetOutlineColor();
    }

    public void UpdatePlaceableCount()
    {
        if (myTextbox == null) { return; }
        myTextbox.text = CountAvailable().ToString();
    }

    public void SetOutlineColor()
    {
        // on first run when it's not ready yet
        if (placeablePool == null) { return; }
        
        // if there is no current selected placeable
        GameObject selectedPlaceable = placeablePool.SelectedPlaceable;
        if (selectedPlaceable == null) { return; }

        if (selectedPlaceable.tag == myPlaceableTag)
        {
            myMPImage.OutlineColor = selectedOutlineColour;
        }
        else
        {
            myMPImage.OutlineColor = defaultOutlineColour;
        }

    }

    private int CountAvailable()
    {
        int total = 0;

        if (placeablePool == null) { return total; }

        foreach (GameObject placeable in placeablePool.pool)
        {
            // active items are not available for placing
            if (placeable.activeSelf) { continue; }

            if (placeable.tag == myPlaceableTag)
            {
                total++;
            }
        }

        return total;
    }


}
