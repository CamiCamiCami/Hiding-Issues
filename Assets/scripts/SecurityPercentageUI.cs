using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SecurityPercentageUI : MonoBehaviour
{
    public string UnknownPercentageText = "???%";
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        if (!this.TryGetComponent<RectTransform>(out var transform))
        {
            throw new Exception("SecurityPercentageUI: Object must have a RectTransform component");
        }

        text = this.AddComponent<TextMeshProUGUI>();
        text.text = "";

        transform.anchorMin = new Vector2(0, 1);
        transform.anchorMax = new Vector2(0, 1);
        transform.pivot = new Vector2(0, 1);
    }

    public void SetSecurityPercentage(int newPercentage)
    {
        text.text = newPercentage.ToString() + "%";
        
    }

    public void VoidSecurityPercentage()
    {
        text.text = "";
    }

    public void UnknownSecurityPercentage()
    {
        text.text = UnknownPercentageText;
    }
}

    