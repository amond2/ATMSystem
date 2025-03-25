using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransferUI : BaseUI
{
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private TMP_InputField customMoneyInputField;
    [SerializeField] private Button transferButton;
    [SerializeField] private Button backButton;

    public TMP_Text infoMessage;

    protected override UIState GetUIState()
    {
        return UIState.Transfer;
    }

    private void OnEnable()
    {
        infoMessage.text = string.Empty;
        idInputField.text = string.Empty;
        customMoneyInputField.text = string.Empty;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        if (backButton != null)
            backButton.onClick.AddListener(() => OnClickBackButton(UIState.Bank | UIState.MyPage));


        if (transferButton != null)
            transferButton.onClick.AddListener(OnClickTransferButton);

        if (customMoneyInputField != null)
            customMoneyInputField.onValueChanged.AddListener(delegate { OnInputCustomMoneyInputField(); });
    }
    
    void OnClickBackButton(UIState targetState)
    {
        uiManager.ChangeState(targetState);
    }
    
    // CustomMoneyInputField에 숫자 외의 문자는 제거
    private void OnInputCustomMoneyInputField()
    {
        string digitsOnly = string.Empty;
        foreach (char c in customMoneyInputField.text)
        {
            if (char.IsDigit(c))
                digitsOnly += c;
        }
        if (customMoneyInputField.text != digitsOnly)
        {
            customMoneyInputField.text = digitsOnly;
        }
    }
    
    private void OnClickTransferButton()
    {
        if (string.IsNullOrEmpty(idInputField.text))
        {
            infoMessage.text = "송금을 받는 분의 아이디를 입력하세요.";
            return;
        }
        
        if (string.IsNullOrEmpty(customMoneyInputField.text))
        {
            infoMessage.text = "송금할 금액을 입력하세요.";
            return;
        }
        
        int transferAmount = int.Parse(customMoneyInputField.text);
    
        if (transferAmount > GameManager.Instance.userData.balance)
        {
            infoMessage.text = "통장의 잔액이 부족해요.";
            return;
        }
        
        string transferUserID = idInputField.text;
        
        GameManager.Instance.userData.balance -= transferAmount;
        UserData userInList = UserManager.Instance.userList.Find(u => u.userID == transferUserID);
        if (userInList == null)
        {
            infoMessage.text = "존재하지 않는 아이디이에요.";
            return;
        }
        
        
        if (userInList != null)
        {
            if (transferUserID == GetCurrentUserID())
            {
                infoMessage.text = "본인에게 송금할 수 없아요.";
                return;
            }
            userInList.balance += transferAmount;
        }

        customMoneyInputField.text = string.Empty;

        infoMessage.text = $"{transferAmount:N0}원 송금 완료.";
        
        MyPageUI myPageUI = FindObjectOfType<MyPageUI>();
        if (myPageUI != null)
        {
            myPageUI.MyPageTexts();
        }
    }
    
    string GetCurrentUserID()
    {
        if (GameManager.Instance != null && GameManager.Instance.userData != null)
        {
            return GameManager.Instance.userData.userID;
        }
        return string.Empty;
    }
}
