using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Slider slider;
    public float valorSlider;
    public Image panelBrillo;

    void Start()
    {
        // Establece el valor inicial del slider desde PlayerPrefs
        slider.value = PlayerPrefs.GetFloat("brillo", 0.4f);
        ActualizarBrillo(slider.value);  // Aplica el valor del slider al brillo inicial
    }

    // Método que se llama cuando el valor del slider cambia
    public void ChangeBrillo(float valor)
    {
        valorSlider = valor;
        // Guarda el valor del brillo en PlayerPrefs
        PlayerPrefs.SetFloat("brillo", valorSlider);
        // Actualiza el color del panelBrillo con el valor del slider
        ActualizarBrillo(valorSlider);
    }

    // Método auxiliar para actualizar el brillo
    void ActualizarBrillo(float valor)
    {
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, valor);
    }
}
