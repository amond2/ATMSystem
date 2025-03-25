using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 메인 화면, 입금 출금 UI 노출 가능

public class BankUI : BaseUI
{
    [SerializeField] private Button WithdrawInButton;
    [SerializeField] private Button DepositInButton;
    [SerializeField] private Button TranferInButton;
    
    protected override UIState GetUIState()
    {
        return UIState.Bank;
    }
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        if (WithdrawInButton != null)
            WithdrawInButton.onClick.AddListener(() => OnClickButton(UIState.Withdraw | UIState.MyPage ));
        if (DepositInButton != null)
            DepositInButton.onClick.AddListener(() => OnClickButton(UIState.Deposit | UIState.MyPage ));
        if (TranferInButton != null)
            TranferInButton.onClick.AddListener(() => OnClickButton(UIState.Transfer | UIState.MyPage ));
    }
    
    void OnClickButton(UIState targetState)
    {
        uiManager.ChangeState(targetState);
    }
}