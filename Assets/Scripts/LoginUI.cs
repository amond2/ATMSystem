using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class LoginUI : BaseUI
{
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signUpButton;
    
    [FormerlySerializedAs("InfoMessage")] public TMP_Text infoMessage;
    
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
            infoMessage.text = "아이디를 입력하세요.";
            return;
        }
        
        if (string.IsNullOrEmpty(passwordInputField.text))
        {
            infoMessage.text = "비밀번호를 입력하세요.";
            return;
        }
        
        // OnInputidInputField, OnInputpasswordInputField 입력한 값이 유효한지 체크
        // 회원가입 된 회원 이여야 함
        
        // 1. 입력한 아이디와 비밀번호가 데이터에 존재시 참, 로그인 성공, 해당 유저의 데이터로 업데이트.
        // 2. 입력한 아이디과 비밀번호가 데이터에 존재하지않음 거짓, 아이디 혹은 비빈번호가 일치하지않습니다 출력
        UserManager userManager = FindObjectOfType<UserManager>();
        if (userManager == null)
        {
            return;
        }

        bool loginSuccess = userManager.LoginUser(idInputField.text, passwordInputField.text);
        if (loginSuccess)
        {
            Debug.Log("Login successful.");

            GameManager.Instance.currentUserID = idInputField.text;
            uiManager.ChangeState(targetState);
        }
        else
        {
            infoMessage.text = "아이디 혹은 비밀번호가 일치하지 않습니다.";
        }
        
        uiManager.ChangeState(targetState);
    }
    
    private void OnClicksSignUpButton(UIState targetState)
    {
        infoMessage.text = string.Empty;
        idInputField.text = string.Empty;
        passwordInputField.text = string.Empty;
        
        uiManager.ChangeState(targetState);
    }

    private void OnInputidInputField(string input)
    {
        
    }
    
    private void OnInputpasswordInputField(string input)
    {
        
    }
}
