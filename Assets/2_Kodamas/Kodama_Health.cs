using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Kodama))]
public class Kodama_Health : MonoBehaviour
{
    // properties
    [SerializeField] int maxHP = 20;


    // member variables
    int currentHP = 0;

    // cache
    Kodama myEnemy;


    private void Start()
    {
        // cache
        myEnemy = GetComponent<Kodama>();
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
