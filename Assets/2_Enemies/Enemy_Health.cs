using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    // properties
    [SerializeField] int maxHP = 20;


    // member variables
    int currentHP = 0;

    // cache
    Enemy myEnemy;


    private void Start()
    {
        // cache
        myEnemy = GetComponent<Enemy>();
    }


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
        myEnemy.RewardCurrency();

        // send back to the pool
        gameObject.SetActive(false);
    }

}
