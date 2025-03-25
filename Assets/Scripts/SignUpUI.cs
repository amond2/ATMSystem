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
    
    public TMP_Text infoMessage;
    
    protected override UIState GetUIState()
    {
        return UIState.SignUp;
    }
    
    private void OnEnable()
    {
        infoMessage.text = string.Empty;
        userNameInputField.text = string.Empty;
        idInputField.text = string.Empty;
        passwordInputField.text = string.Empty;
        passwordConfirmInputField.text = string.Empty;
    }
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        if (backButton != null)
            backButton.onClick.AddListener(() => OnClickBackButton(UIState.Login));
        if (signUpButton != null)
            signUpButton.onClick.AddListener(OnClickSignUpButton);
    }
    
    void OnClickBackButton(UIState targetState)
    {
        infoMessage.text = string.Empty;
        
        uiManager.ChangeState(targetState);
    }

    void OnClickSignUpButton()
    {
        if (string.IsNullOrEmpty(userNameInputField.text))
        {
            infoMessage.text = "이름을 입력하세요.";
            return;
        }
        
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
        
        if (string.IsNullOrEmpty(passwordConfirmInputField.text))
        {
            infoMessage.text = "비밀번호를 재입력하세요.";
            return;
        }
        
        if (passwordInputField.text != passwordConfirmInputField.text)
        {
            infoMessage.text = "비밀번호가 일치하지 않아요.";
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
            infoMessage.text = "이미 존재하는 아이디이에요.";
            return;
        }
        
        UserManager.Instance.RegisterUser(idInputField.text, userNameInputField.text, passwordInputField.text, 100000000, 0);
        GameManager.Instance.SaveUserData();
        
        infoMessage.text = "회원가입이 완료되었어요.";
    }
}
