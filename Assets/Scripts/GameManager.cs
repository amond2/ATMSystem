using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public TMP_Text userNameText;
    public TMP_Text balanceText;
    public TMP_Text balanceUnit;
    public TMP_Text cashText;
    public TMP_Text cashUnit;
    
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
        UpdateTexts();
    }

    void Refresh()
    {
        UpdateTexts();
    }
    
    void UpdateTexts()
    {
        userNameText.text = userData.userName;
        balanceText.text = "통장 잔액";
        balanceUnit.text = string.Format("{0:#,###}", userData.balance);
        cashText.text = "보유 현금";
        cashUnit.text = string.Format("{0:#,###}", userData.cash);
    }
}
