using System;
using UnityEngine;

// [Serializable] // 직렬화용

public class UserData
{
    public string userName;
    public int balance;
    public int cash;
    
    public UserData(string userName, int cash, int balance)
    {
        this.userName = userName;
        this.cash = cash;
        this.balance = balance;
    }
}
