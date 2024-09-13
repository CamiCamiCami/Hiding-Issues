using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideOpcionText : MonoBehaviour
{
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Si el canvas no est� asignado, obtenemos el primero que encontremos en la escena
        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }

        // Crear un nuevo objeto de texto dentro del Canvas
        GameObject nuevoTextoObj = new GameObject("TextoMeshPro");
        nuevoTextoObj.transform.SetParent(canvas.transform);

        // A�adir el componente TextMeshPro al objeto reci�n creado
        TextMeshProUGUI textMeshPro = nuevoTextoObj.AddComponent<TextMeshProUGUI>();

        // Configurar las propiedades del TextMeshPro
        textMeshPro.text = "Press H to hide";
        textMeshPro.fontSize = 36;
        textMeshPro.color = Color.white;

        // Ajustar la posici�n del texto dentro del Canvas
        RectTransform rectTransform = textMeshPro.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(500, 100); // Tama�o del rect�ngulo
        rectTransform.anchoredPosition = new Vector2(0, 0); // Centrado en la pantalla

        canvas.gameObject.SetActive(false);
    }

    public void Show_Option ()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
        }
    }

    public void Hide_Option ()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }

}
