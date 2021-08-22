using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementRandomYRotation : MonoBehaviour
{
    private void OnEnable()
    {
        var euler = transform.eulerAngles;
        euler.y = Random.Range(0f, 360f);
        this.transform.eulerAngles = euler;
    }
}
