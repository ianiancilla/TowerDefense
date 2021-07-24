using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int placementCost = 75;

    public bool CreateTower(Tower tower, Vector3 position)
    {
        CurrencyBank bank = FindObjectOfType<CurrencyBank>();
     
        if (!bank) { return false; }

        if (bank.CurrentBalance >= placementCost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(placementCost);
            return true;
        }

        return false;

    }
}
