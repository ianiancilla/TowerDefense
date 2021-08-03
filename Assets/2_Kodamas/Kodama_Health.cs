using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Kodama))]
public class Kodama_Health : MonoBehaviour
{
    // cache
    Kodama myKodama;


    private void Start()
    {
        // cache
        myKodama = GetComponent<Kodama>();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

}
