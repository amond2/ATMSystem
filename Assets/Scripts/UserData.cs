using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserData : MonoBehaviour
{
    string playerName = "김효재";
    int balance = 50000;
    int cash = 1000000;

    public TMP_Text NameText;
    public TMP_Text BalanceText;
    public TMP_Text BalanceUnit;
    public TMP_Text CashText;
    public TMP_Text CashUnit;
    

    void Start()
    {
        UpdateTexts();
    }

    void UpdateTexts()
    {
        NameText.text = playerName;
        BalanceText.text = "통장 잔액";
        BalanceUnit.text = string.Format("{0:#,###}", balance);
        CashText.text = "보유 현금";
        CashUnit.text = string.Format("{0:#,###}", cash);
    }
}
