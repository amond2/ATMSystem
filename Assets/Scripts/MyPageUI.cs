using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class MyPageUI : BaseUI
{
    [SerializeField] public TMP_Text userNameText;
    [SerializeField] public TMP_Text balanceLabel;
    [SerializeField] public TMP_Text balanceUnit;
    [SerializeField] public TMP_Text cashLabel;
    [SerializeField] public TMP_Text cashUnit;
    
    protected override UIState GetUIState()
    {
        return UIState.MyPage;
    }
    
    void Start()
    {
        MyPageTexts();
        Debug.Log("MyPageTexts has been updated1");

    }
    
    public void MyPageTexts()
    {
        var userData = GameManager.Instance.userData;
        
        userNameText.text = userData.userName;
        balanceLabel.text = "통장 잔액";
        balanceUnit.text = string.Format("{0:N0}", userData.balance);
        cashLabel.text = "보유 현금";
        cashUnit.text = string.Format("{0:N0}", userData.cash);
        
        GameManager.Instance.UpdateUserData(
            GameManager.Instance.userData.userName, 
            GameManager.Instance.userData.balance, 
            GameManager.Instance.userData.cash);
        
        Debug.Log("MyPageTexts has been updated2");
    }
}
