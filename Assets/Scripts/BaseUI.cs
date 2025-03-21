using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 공통 UI 제어

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;

    public virtual void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }
    
    protected abstract UIState GetUIState(); 
    public virtual void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
    
    
}