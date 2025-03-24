using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { get; private set; }
    
    public List<UserData> userList = new List<UserData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public void RegisterUser(string userID, string userName, string password, int cash, int balance)
    {
        UserData newUser = new UserData(userID, userName, password, cash, balance);
        userList.Add(newUser);
        Debug.Log($"New user, id: {userID}, name: {userName}, cash: {cash}, balance: {balance}");
    }

    public UserData GetUser(string userName)
    {
        return userList.Find(user => user.userName == userName);
    }
    
    public bool CheckUserPassword(string userID, string password)
    {
        UserData user = userList.Find(u => u.userID == userID);
        if (user == null)
        {
            return false;
        }
        return user.CheckPassword(password);
    }
    
    public bool LoginUser(string userID, string password)
    {
        UserData user = userList.Find(u => u.userID == userID);
        if (user != null && user.CheckPassword(password))
        {
            GameManager.Instance.userData = user;
            Debug.Log($"{userID} logged in");
            return true;
        }
        return false;
    }
}