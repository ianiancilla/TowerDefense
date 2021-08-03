using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Placeable
{
    public bool CanBePlaced(Vector2Int coordinates);

    public GameObject Place(Vector2Int coordinates);

    public void Remove(Vector2Int coordinates);
}
