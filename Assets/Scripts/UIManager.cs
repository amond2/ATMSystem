using UnityEngine;

public enum UIState
{
    MyPage,
    Bank,
    Withdraw,
    Deposit,
    Login,
    SignUp
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
    }
    
    public void ChangeState(UIState state)
    {
        myPageUI?.SetActive(state);
        bankUI?.SetActive(state);
        withdrawUI?.SetActive(state);
        depositUI?.SetActive(state);
        loginUI?.SetActive(state);
        signUpUI?.SetActive(state);
    }
}
