using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaceableButtonText : MonoBehaviour
{
    // properties
    [SerializeField] GameObject prefabToCount;

    // cache
    PlaceableObjectsPool placeablePool;
    TMP_Text textbox;
    Placeable myPlaceableType;

    // Start is called before the first frame update
    void Start()
    {
        // cache
        placeablePool = FindObjectOfType<PlaceableObjectsPool>();
        textbox = GetComponent<TMP_Text>();
        myPlaceableType = prefabToCount.GetComponent<Placeable>();
    }

    // Update is called once per frame
    void Update()
    {
        textbox.text = CountAvailable().ToString();
    }

    private int CountAvailable()
    {
        int total = 0;

        foreach (GameObject placeable in placeablePool.pool)
        {
            // active items are not available for placing
            if (placeable.activeSelf) { continue; }

            if (placeable.GetComponent<Placeable>() == myPlaceableType)
            {
                total++;
            }
        }

        return total;
    }
}
