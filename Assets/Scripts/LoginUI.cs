using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginUI : BaseUI
{
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signUpButton;
    
    public TMP_Text InfoMessage;
    
    protected override UIState GetUIState()
    {
        return UIState.Login;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        if (idInputField != null)
            idInputField.onValueChanged.AddListener(OnInputidInputField);
        if (passwordInputField != null)
            passwordInputField.onValueChanged.AddListener(OnInputpasswordInputField);
        
        if (loginButton != null)
            loginButton.onClick.AddListener(() => OnClickLoginButton(UIState.Bank));
        if (signUpButton != null)
            signUpButton.onClick.AddListener(() => OnClicksSignUpButton(UIState.SignUp));
        
    }
    
    private void OnClickLoginButton(UIState targetState)
    {
        if (string.IsNullOrEmpty(idInputField.text))
        {
            InfoMessage.text = "아이디를 입력하세요.";
            return;
        }
        
        if (string.IsNullOrEmpty(passwordInputField.text))
        {
            InfoMessage.text = "비밀번호를 입력하세요.";
            return;
        }
        
        // OnInputidInputField, OnInputpasswordInputField 입력한 값이 유효한지 체크
        // 회원가입 된 회원 이여야 함
        
        uiManager.ChangeState(targetState);
    }
    
    private void OnClicksSignUpButton(UIState targetState)
    {
        InfoMessage.text = "";
        
        uiManager.ChangeState(targetState);
    }

    private void OnInputidInputField(string input)
    {
        
    }
    
    private void OnInputpasswordInputField(string input)
    {
        
    }
}
