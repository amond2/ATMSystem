using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Bank,
    Withdraw,
    Deposit
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    BankUI bankUI;
    WithdrawUI withdrawUI;
    DepositUI depositUI;
    
    void Awake()
    {
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
}
