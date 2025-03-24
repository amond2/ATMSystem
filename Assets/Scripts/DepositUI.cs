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
    
    public TMP_Text InfoMessage;
    
    protected override UIState GetUIState()
    {
        return UIState.Deposit;
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
        
        if (depositButton != null)
            depositButton.onClick.AddListener(OnClickDepositButton);
        
        if (customMoneyInputField != null)
            customMoneyInputField.onValueChanged.AddListener(delegate { OnInputCustomMoneyInputField(); });
    }
    
    void OnClickBackButton(UIState targetState)
    {
        InfoMessage.text = string.Empty;
        
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
            InfoMessage.text = "입금 금액을 입력하세요.";
            return;
        }
        
        int depositAmount = int.Parse(customMoneyInputField.text);
    
        // 잔액이 부족한 경우 먼저 체크
        if (depositAmount > GameManager.Instance.userData.cash)
        {
            InfoMessage.text = "현금이 부족합니다.";
            return;
        }
    
        // 출금 가능하면 처리
        GameManager.Instance.userData.cash -= depositAmount;
        GameManager.Instance.userData.balance += depositAmount;
    
        customMoneyInputField.text = string.Empty;
    
        InfoMessage.text = string.Format("{0:N0}원 입금 완료.", depositAmount);
        
        UIManager.Instance.UpdateTexts();

        GameManager.Instance.SaveUserData();
    }
}
