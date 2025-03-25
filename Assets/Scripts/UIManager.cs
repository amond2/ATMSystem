using UnityEngine;

[System.Flags]
public enum UIState
{
    None = 0,
    MyPage = 1 << 0,  // 1
    Bank = 1 << 1,    // 2
    Withdraw = 1 << 2,// 4
    Deposit = 1 << 3, // 8
    Login = 1 << 4,   // 16
    SignUp = 1 << 5,   // 32
    Transfer = 1 << 6
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    MyPageUI myPageUI;
    BankUI bankUI;
    WithdrawUI withdrawUI;
    DepositUI depositUI;
    LoginUI loginUI;
    SignUpUI signUpUI;
    TransferUI transferUI;
    
    void Awake()
    {
        Instance = this;
        
        myPageUI = GetComponentInChildren<MyPageUI>(true);
        myPageUI?.Init(this);
        bankUI = GetComponentInChildren<BankUI>(true);
        bankUI?.Init(this);
        withdrawUI = GetComponentInChildren<WithdrawUI>(true);
        withdrawUI?.Init(this);
        depositUI = GetComponentInChildren<DepositUI>(true);
        depositUI?.Init(this);
        loginUI = GetComponentInChildren<LoginUI>(true);
        loginUI?.Init(this);
        signUpUI = GetComponentInChildren<SignUpUI>(true);
        signUpUI?.Init(this);
        transferUI = GetComponentInChildren<TransferUI>(true);
        transferUI?.Init(this);
    }
    
    public void ChangeState(UIState state)
    {
        if(myPageUI != null)
            myPageUI.gameObject.SetActive((state & UIState.MyPage) != 0);
        if(bankUI != null)
            bankUI.gameObject.SetActive((state & UIState.Bank) != 0);
        if(withdrawUI != null)
            withdrawUI.gameObject.SetActive((state & UIState.Withdraw) != 0);
        if(depositUI != null)
            depositUI.gameObject.SetActive((state & UIState.Deposit) != 0);
        if(loginUI != null)
            loginUI.gameObject.SetActive((state & UIState.Login) != 0);
        if(signUpUI != null)
            signUpUI.gameObject.SetActive((state & UIState.SignUp) != 0);
        if(transferUI != null)
            transferUI.gameObject.SetActive((state & UIState.Transfer) != 0);
    }
}
