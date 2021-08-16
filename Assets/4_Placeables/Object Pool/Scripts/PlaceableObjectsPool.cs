using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectsPool : MonoBehaviour
{
    // properties
    [Tooltip ("Any extra placeables needed. All placeables already on tha map will be removable and if removed will be added to available ones.")]
    [SerializeField] PlaceablePoolType[] availableExtraPlaceables;

    // member variables
    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();

    // currently selected placeable
    private GameObject selectedPlaceable;
    public GameObject SelectedPlaceable { get { return selectedPlaceable; } }


    private void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    private void PopulatePool()
    {
        // add objects that were placed in editor as pre-existing
        AddPreExistingPlaceablesToPool();

        // instantiate prefabs
        foreach (PlaceablePoolType type in availableExtraPlaceables)
        {
            for (int i = 0; i < type.AvailableNumber; i++)
            {
                // instantiate
                GameObject go = Instantiate(type.PlaceablePrefab, this.transform);

                // add to pool
                pool.Add(go);

                // set inactive
                go.SetActive(false);
            }
        }
    }

    public void SetAsCurrentPlaceable(GameObject newPlaceable)
    {
        if (newPlaceable.GetComponent<Placeable>() == null)
        {
            Debug.Log("Trying to set invalid Game Object as current Placeable Object.");
            return;
        }

        selectedPlaceable = newPlaceable;
    }

    private void AddPreExistingPlaceablesToPool()
    {
        foreach (PlaceablePoolType type in availableExtraPlaceables)
        {
            GameObject[] preexisting = GameObject.FindGameObjectsWithTag(type.PlaceableTag);

            // add to pool
            foreach (GameObject go in preexisting)
            {
                pool.Add(go);
            }
        }
    }



}

