using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaceableButtonText : MonoBehaviour
{
    // properties
    [SerializeField] string myPlaceableTag;

    // cache
    PlaceableObjectsPool placeablePool;
    TMP_Text myTextbox;

    // Start is called before the first frame update
    void Start()
    {
        // cache
        placeablePool = FindObjectOfType<PlaceableObjectsPool>();
        myTextbox = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO remove this from update and refresh on need!
        myTextbox.text = CountAvailable().ToString();
    }

    private int CountAvailable()
    {
        int total = 0;

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
