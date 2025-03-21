using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 메인 화면, 입금 출금 UI 노출 가능

public class BankUI : BaseUI
{
    [SerializeField] private Button WithdrawInButton;
    [SerializeField] private Button DepositInButton;
    
    protected override UIState GetUIState()
    {
        return UIState.Bank;
    }
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        if (WithdrawInButton != null)
            WithdrawInButton.onClick.AddListener(() => OnClickButton(UIState.Withdraw));
        if (DepositInButton != null)
            DepositInButton.onClick.AddListener(() => OnClickButton(UIState.Deposit));
    }
    
    void OnClickButton(UIState targetState)
    {
        uiManager.ChangeState(targetState);
    }
}