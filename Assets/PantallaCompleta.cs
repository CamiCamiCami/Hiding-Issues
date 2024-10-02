using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaCompleta : MonoBehaviour
{
    public Toggle toggle;
    void Start()
    {
        if(Screen.fullScreen){
            toggle.isOn = true;
        } else {
            toggle.isOn = false;
        }
    }

    void Update()
    {
        
    }

    public void ChangePantallaCompleta(bool pantallaCompleta){
        Screen.fullScreen = pantallaCompleta;
    }
}
