using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlaceablePoolType
{
    [SerializeField] GameObject placeablePrefab;
    public GameObject PlaceablePrefab { get { return placeablePrefab; } }

    [SerializeField] string placeableTag;
    public string PlaceableTag { get { return placeableTag; } }

    [SerializeField] int availableNumber;
    public int AvailableNumber { get { return availableNumber; } }
}
