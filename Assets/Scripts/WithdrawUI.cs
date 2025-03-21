using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WithdrawUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Withdraw;
    }
}
