using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Kodama_Movement))]
public class Kodama_Health : MonoBehaviour
{
    [SerializeField] float destructionDelay = 1f;

    // cache
    Kodama_Movement myMovement;


    private void Start()
    {
        // cache
        myMovement = GetComponent<Kodama_Movement>();
    }

    public void Die()
    {
        // stop movement
        
        Destroy(this.gameObject);
        Debug.Log("DED");
    }

}
