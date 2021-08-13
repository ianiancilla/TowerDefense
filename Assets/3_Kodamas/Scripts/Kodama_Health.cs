using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Kodama_Movement))]
public class Kodama_Health : MonoBehaviour
{

    // cache
    Kodama_Movement myMovement;


    private void Start()
    {
        // cache
        myMovement = GetComponent<Kodama_Movement>();
    }

    public void Die()
    {
        FindObjectOfType<GameManager>().GameOver();
        this.gameObject.SetActive(false);
    }

}
