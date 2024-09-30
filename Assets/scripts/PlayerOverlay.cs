using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class PlayerOverlay : MonoBehaviour
{

    private Fade fadeComponent;
    private SecurityPercentageUI securityComponent;
    private HideOpcionText hidePopUp;
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = this.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = this.AddComponent<CanvasScaler>();
        GraphicRaycaster raycaster = this.AddComponent<GraphicRaycaster>();

        fadeComponent = this.GetComponentInChildren<Fade>();
        securityComponent = this.GetComponentInChildren<SecurityPercentageUI>();
        hidePopUp = this.GetComponentInChildren<HideOpcionText>();
    }
    public void Fade()
    {
        fadeComponent.StartFade();
    }

    public void ResetFade()
    {
        fadeComponent.ResetFade();
    }

    public void SetSecurityPercentage(int? newPercentage)
    {
        if (newPercentage.HasValue)
        {
            securityComponent.SetSecurityPercentage(newPercentage.Value);
        } else
        {
            securityComponent.UnknownSecurityPercentage();
        }
    }

    public void VoidSecurityPercentage()
    {
        securityComponent.VoidSecurityPercentage();
    }

    public void ShowHidePopUp()
    {
        hidePopUp.ShowOption();
    }

    public void ResetHidePopUp()
    {
        hidePopUp.HideOption();
    }
}
