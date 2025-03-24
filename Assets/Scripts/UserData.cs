using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable] // 직렬화 역직렬화 사용하려면 추가해야함.
public class UserData
{
    public string userID;
    public string userName;
    public string Password;
    public int balance;
    public int cash;

    public UserData(string userID, string userName, string password, int cash, int balance)
    {
        this.userID = userID;
        this.userName = userName;
        this.Password = EncryptPassword(password);
        this.cash = cash;
        this.balance = balance;
    }

    // SHA256을 사용하여 비밀번호를 암호화
    private string EncryptPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    
    public bool CheckPassword(string password)
    {
        return EncryptPassword(password) == this.Password;
    }
}
