using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    // properties
    [SerializeField] int maxHP = 20;


    // member variables
    int currentHP = 0;


    // Start is called before the first frame update
    void OnEnable()
    {
        // initialise
        currentHP = maxHP;
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(1);
    }

    private void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // send back to the pool
        gameObject.SetActive(false);
    }

}
