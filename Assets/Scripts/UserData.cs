using System;
using UnityEngine;

[Serializable] // 직렬화 역직렬화 사용하려면 추가해야함.

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
