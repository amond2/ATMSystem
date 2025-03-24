using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignUpUI : BaseUI
{
    [SerializeField] private TMP_InputField userNameInputField;
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField passwordConfirmInputField;
    [SerializeField] private Button signUpButton;
    [SerializeField] private Button backButton;
    
    public TMP_Text InfoMessage;
    
    protected override UIState GetUIState()
    {
        return UIState.SignUp;
    }
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        if (backButton != null)
            backButton.onClick.AddListener(() => OnClickBackButton(UIState.Login));
        if (signUpButton != null)
            signUpButton.onClick.AddListener(OnClickSignUpButton);
        
        // 입력 완료 후 유효성 검사 또는 처리: onEndEdit 사용하기, myInputField.onEndEdit.AddListener(OnInputFieldEndEdit);
        
    }
    
    void OnClickBackButton(UIState targetState)
    {
        InfoMessage.text = string.Empty;
        
        uiManager.ChangeState(targetState);
    }

    void OnClickSignUpButton()
    {
        if (string.IsNullOrEmpty(userNameInputField.text))
        {
            InfoMessage.text = "이름을 입력하세요.";
            return;
        }
        
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
        
        if (string.IsNullOrEmpty(passwordConfirmInputField.text))
        {
            InfoMessage.text = "비밀번호를 재입력하세요.";
            return;
        }
        
        if (passwordInputField.text != passwordConfirmInputField.text)
        {
            InfoMessage.text = "비밀번호가 일치하지 않아요.";
            return;
        }
    
        UserManager userManager = FindObjectOfType<UserManager>();
        if (userManager == null)
        {
            return;
        }
    
        UserData existingUser = userManager.userList.Find(u => u.userID == idInputField.text);
        if (existingUser != null)
        {
            InfoMessage.text = "이미 존재하는 아이디이에요.";
            return;
        }
        
        UserManager.Instance.RegisterUser(idInputField.text, userNameInputField.text, passwordInputField.text, 100000, 0);
    
        GameManager.Instance.SaveUserData();
        
        InfoMessage.text = "회원가입이 완료되었어요.";
    }
}
