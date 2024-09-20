using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    public Slider slider;
    public float valorSlider;
    public Image Brillo;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brillo", 0.4f);
        Brillo.color = new Color(Brillo.color.r, Brillo.color.g, Brillo.color.b, slider.value);
    }
    void Update()
    {
        
    }

    public void ChangeBrillo(float valor)
    {
        valorSlider = valor;
        PlayerPrefs.GetFloat("brillo", valorSlider);
        Brillo.color = new Color(Brillo.color.r, Brillo.color.g, Brillo.color.b, slider.value);
    }
}
