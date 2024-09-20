using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    public Slider slider;
    public float valorSlider;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volumen", 0.5f); //el valor se mantiene guardado hasta el prox cambio
        AudioListener.volume = slider.value;
    }

    public void ChangeVolume(float valor){
        valorSlider = valor;
        PlayerPrefs.GetFloat("Volumen", valorSlider);
        AudioListener.volume = slider.value;
    }
}
