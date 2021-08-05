using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectsPool : MonoBehaviour
{
    // properties
    [SerializeField] PlaceablePoolType[] availablePlaceables;

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
        // instantiate prefabs
        foreach (PlaceablePoolType type in availablePlaceables)
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
}

