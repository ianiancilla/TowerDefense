using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrencyBank : MonoBehaviour
{
    // properties
    [SerializeField] int startingBalance = 100;

    // member variabkes
    [SerializeField] int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startingBalance;
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        if (amount < 0) { Debug.Log("Warning, depositing negative amount to Currency bank"); }
    }
    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        if (amount < 0) { Debug.Log("Warning, withdrawing negative amount from Currency bank"); }

        if (currentBalance <= 0)
        {
            // GAME OVER

            //TODO reload scene for now
            Debug.Log("GAME OVER");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
