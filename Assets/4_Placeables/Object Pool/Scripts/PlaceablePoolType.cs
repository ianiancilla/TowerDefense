using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlaceablePoolType
{
    [SerializeField] GameObject placeablePrefab;
    public GameObject PlaceablePrefab { get { return placeablePrefab; } }
    
    [SerializeField] int availableNumber;
    public int AvailableNumber { get { return availableNumber; } }
}
