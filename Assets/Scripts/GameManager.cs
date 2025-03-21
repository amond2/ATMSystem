using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 내 데이터 호출, 전역적인 설정 제어

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public UserData userData;

    private void Awake()
    {
        if (Instance != null && Instance != this) // 게임 메니져는 씬당 1개 가 실행되도록 하는게 좋다. 
        {
            Destroy(gameObject); // 게임메니저가 중복 존재하는 경우 중복을 없애야한다. (여러 씬을 사용하는 경우, 상황 발생 가능)
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        userData = new UserData("김효재", 100000, 50000);
    }
    
    void Start()
    {
        LoadUserData();
        UIManager.Instance.UpdateTexts();
    }

    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(userData);
        
        string filePath = Application.persistentDataPath + "/userdata.json";
        
        System.IO.File.WriteAllText(filePath, json);
        
        Debug.Log("UserData saved to " + filePath);
    }

    public void LoadUserData()
    {
        string filePath = Application.persistentDataPath + "/userdata.json";
        
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            
            userData = JsonUtility.FromJson<UserData>(json);
            
            Debug.Log("UserData loaded from " + filePath);
        }
        else
        {
            Debug.LogWarning("UserData file not found at " + filePath);
        }
    }
}
