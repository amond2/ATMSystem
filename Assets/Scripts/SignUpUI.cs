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
    }
    
    void OnClickBackButton(UIState targetState)
    {
        InfoMessage.text = "";
        
        uiManager.ChangeState(targetState);
    }
}
