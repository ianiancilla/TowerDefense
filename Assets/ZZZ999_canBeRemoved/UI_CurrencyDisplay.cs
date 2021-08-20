using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CurrencyDisplay : MonoBehaviour
{
    // cache
    TMP_Text currencyLabel;
    CurrencyBank bank;

    // Start is called before the first frame update
    void Start()
    {
        currencyLabel = GetComponent<TMP_Text>();
        bank = FindObjectOfType<CurrencyBank>();
    }

    // Update is called once per frame
    void Update()
    {
        currencyLabel.text = "Current money: " + bank.CurrentBalance.ToString();
    }
}
