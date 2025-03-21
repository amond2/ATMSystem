using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum UIState
{
    Bank,
    Withdraw,
    Deposit
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public TMP_Text userNameText;
    public TMP_Text balanceLabel;
    public TMP_Text balanceUnit;
    public TMP_Text cashLabel;
    public TMP_Text cashUnit;

    BankUI bankUI;
    WithdrawUI withdrawUI;
    DepositUI depositUI;
    
    void Awake()
    {
        Instance = this;
        
        bankUI = GetComponentInChildren<BankUI>(true);
        bankUI?.Init(this);
        withdrawUI = GetComponentInChildren<WithdrawUI>(true);
        withdrawUI?.Init(this);
        depositUI = GetComponentInChildren<DepositUI>(true);
        depositUI?.Init(this);
    }
    
    public void ChangeState(UIState state)
    {
        bankUI?.SetActive(state);
        withdrawUI?.SetActive(state);
        depositUI?.SetActive(state);
    }
    
    public void UpdateTexts()
    {
        var userData = GameManager.Instance.userData;
        
        userNameText.text = userData.userName;
        balanceLabel.text = "통장 잔액";
        balanceUnit.text = string.Format("{0:#,###}", userData.balance);
        cashLabel.text = "보유 현금";
        cashUnit.text = string.Format("{0:#,###}", userData.cash);
    }
}
