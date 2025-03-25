using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class WithdrawUI : BaseUI
{
    [SerializeField] private Button moneyInput_1;
    [SerializeField] private Button moneyInput_2;
    [SerializeField] private Button moneyInput_3;
    [SerializeField] private TMP_InputField customMoneyInputField;
    [SerializeField] private Button withdrawButton;
    [SerializeField] private Button backButton;
    
    public TMP_Text infoMessage;
    
    protected override UIState GetUIState()
    {
        return UIState.Withdraw;
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
            backButton.onClick.AddListener(() => OnClickBackButton(UIState.Bank | UIState.MyPage));
        
        if (moneyInput_1 != null)
            moneyInput_1.onClick.AddListener(() => OnClickMoneyInput(10000));
        if (moneyInput_2 != null)
            moneyInput_2.onClick.AddListener(() => OnClickMoneyInput(20000));
        if (moneyInput_3 != null)
            moneyInput_3.onClick.AddListener(() => OnClickMoneyInput(50000));
        
        if (withdrawButton != null)
            withdrawButton.onClick.AddListener(OnClickWithdrawButton);
        
        if (customMoneyInputField != null)
            customMoneyInputField.onValueChanged.AddListener(delegate { OnInputCustomMoneyInputField(); });
    }
    
    void OnClickBackButton(UIState targetState)
    {
        uiManager.ChangeState(targetState);
    }
    
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
    
    private void OnClickWithdrawButton()
    {
        if (string.IsNullOrEmpty(customMoneyInputField.text))
        {
            infoMessage.text = "출금할 금액을 입력하세요.";
            return;
        }
        
        int withdrawAmount = int.Parse(customMoneyInputField.text);
    
        if (withdrawAmount > GameManager.Instance.userData.balance)
        {
            infoMessage.text = "통장의 잔액이 부족해요.";
            return;
        }

        
        GameManager.Instance.userData.balance -= withdrawAmount;
        GameManager.Instance.userData.cash += withdrawAmount;

        customMoneyInputField.text = string.Empty;

        infoMessage.text = $"{withdrawAmount:N0}원 출금 완료.";
        
        MyPageUI myPageUI = FindObjectOfType<MyPageUI>();
        if (myPageUI != null)
        {
            myPageUI.MyPageTexts();
        }
    }
}