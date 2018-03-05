using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LoadingUI : SingletonMB<LoadingUI>
{
    public GameObject loadScreen;
    public UnityEngine.UI.Slider LoadingBar;
    public TextMeshProUGUI LoadingText;

    public void setPercentage(float progress)
    {
        Debug.Log(progress);
        LoadingBar.value = progress;
        LoadingText.text = progress * 100f + "%";
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public override void CopyValues(SingletonMB<LoadingUI> copy)
    {
        
    }
}
