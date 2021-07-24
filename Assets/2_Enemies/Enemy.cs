using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // properties
    [SerializeField] int currencyReward = 10;
    [SerializeField] int currencyPenalty = 25;

    // member variables

    // cache
    CurrencyBank bank;

    private void Start()
    {
        bank = FindObjectOfType<CurrencyBank>();
    }

    public void RewardCurrency()
    {
        if (!bank) { return; }
        bank.Deposit(currencyReward);
    }

    public void StealCurrency()
    {
        if (!bank) { return; }
        bank.Withdraw(currencyPenalty);
    }
}
