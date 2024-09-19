using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(RectTransform))]
public class HideOpcionText : MonoBehaviour
{

    public string HidePopUpMessage = "Press H to hide";

    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

        // Crear un nuevo objeto de texto dentro del Canvas
        text = this.AddComponent<TextMeshProUGUI>();
        // Configurar las propiedades del TextMeshPro
        text.text = HidePopUpMessage;
        text.fontSize = 36;
        text.color = Color.white;

        // Ajustar la posicion del texto dentro del Canvas
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(500, 100); // Tamanio del rectangulo
        rectTransform.anchoredPosition = new Vector2(0, 0); // Centrado en la pantalla

        text.enabled = false;
    }

    public void ShowOption ()
    {
        text.enabled = true;
    }

    public void HideOption ()
    {
        text.enabled = false;
    }

}
