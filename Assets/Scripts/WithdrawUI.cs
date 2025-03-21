using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WithdrawUI : BaseUI
{
    [SerializeField] private Button moneyInput_1;
    [SerializeField] private Button moneyInput_2;
    [SerializeField] private Button moneyInput_3;
    [SerializeField] private TMP_InputField customMoneyInputField;
    [SerializeField] private Button withdrawButton;
    [SerializeField] private Button backButton;
    
    public TMP_Text InfoMessage;
    
    protected override UIState GetUIState()
    {
        return UIState.Withdraw;
    }
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        if (backButton != null)
            backButton.onClick.AddListener(() => OnClickBackButton(UIState.Bank));
        
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
        InfoMessage.text = "";
        
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
    
    private void OnClickWithdrawButton()
    {
        if (string.IsNullOrEmpty(customMoneyInputField.text))
        {
            InfoMessage.text = "출금 금액을 입력하세요.";
            return;
        }
        
        int withdrawAmount = int.Parse(customMoneyInputField.text);
    
        // 잔액이 부족한 경우 먼저 체크
        if (withdrawAmount > GameManager.Instance.userData.balance)
        {
            InfoMessage.text = "잔액이 부족합니다.";
            return;
        }
    
        // 출금 가능하면 처리
        GameManager.Instance.userData.balance -= withdrawAmount;
        GameManager.Instance.userData.cash += withdrawAmount;
    
        customMoneyInputField.text = "";
    
        InfoMessage.text = string.Format("{0:N0}원 출금 완료.", withdrawAmount);
        
        UIManager.Instance.UpdateTexts();
        
        GameManager.Instance.SaveUserData();
    }
}