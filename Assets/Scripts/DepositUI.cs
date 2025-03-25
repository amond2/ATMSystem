using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepositUI : BaseUI
{
    [SerializeField] private Button moneyInput_1;
    [SerializeField] private Button moneyInput_2;
    [SerializeField] private Button moneyInput_3;
    [SerializeField] private TMP_InputField customMoneyInputField;
    [SerializeField] private Button depositButton;
    [SerializeField] private Button backButton;
    
    public TMP_Text infoMessage;
    
    protected override UIState GetUIState()
    {
        return UIState.Deposit;
    }

    private void OnEnable()
    {
        infoMessage.text = string.Empty;
        customMoneyInputField.text = string.Empty;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        if (backButton != null)
            backButton.onClick.AddListener(() => OnClickBackButton(UIState.Bank | UIState.MyPage ));
        
        if (moneyInput_1 != null)
            moneyInput_1.onClick.AddListener(() => OnClickMoneyInput(10000));
        if (moneyInput_2 != null)
            moneyInput_2.onClick.AddListener(() => OnClickMoneyInput(20000));
        if (moneyInput_3 != null)
            moneyInput_3.onClick.AddListener(() => OnClickMoneyInput(50000));
        
        if (depositButton != null)
            depositButton.onClick.AddListener(OnClickDepositButton);
        
        if (customMoneyInputField != null)
            customMoneyInputField.onValueChanged.AddListener(delegate { OnInputCustomMoneyInputField(); });
    }
    
    void OnClickBackButton(UIState targetState)
    {
        uiManager.ChangeState(targetState);
    }
    
    // MoneyInput 버튼 클릭 시, 해당 금액을 누적하여 CustomMoneyInputField에 반영
    private void OnClickMoneyInput(int addAmount)
    {
        int currentAmount = 0;
        if (!string.IsNullOrEmpty(customMoneyInputField.text))
        {
            int.TryParse(customMoneyInputField.text, out currentAmount);
        }
        currentAmount += addAmount;
        customMoneyInputField.text = currentAmount.ToString();
    }

    // CustomMoneyInputField에 숫자 외의 문자는 제거
    private void OnInputCustomMoneyInputField()
    {
        string digitsOnly = "";
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
    
    private void OnClickDepositButton()
    {
        if (string.IsNullOrEmpty(customMoneyInputField.text))
        {
            infoMessage.text = "입금할 금액을 입력하세요.";
            return;
        }
        
        int depositAmount = int.Parse(customMoneyInputField.text);
    
        if (depositAmount > GameManager.Instance.userData.cash)
        {
            infoMessage.text = "현금이 부족해요.";
            return;
        }

        // if (depositAmount <= GameManager.Instance.userData.balance)
        // {
            GameManager.Instance.userData.cash -= depositAmount;
            GameManager.Instance.userData.balance += depositAmount;

            customMoneyInputField.text = string.Empty;

            infoMessage.text = $"{depositAmount:N0}원 입금 완료.";
            
            MyPageUI myPage = FindObjectOfType<MyPageUI>();
            if(myPage != null)
            {
                myPage.MyPageTexts();
            }
        // }
    }
}
