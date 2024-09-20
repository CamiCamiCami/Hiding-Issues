using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opciones : MonoBehaviour
{
    public ControladorOpciones pantallaOpciones2;
    void Start()
    {
        pantallaOpciones2 = GameObject.FindGameObjectWithTag("opciones").GetComponent<ControladorOpciones>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            MostrarOpciones();
        }
    }

    public void MostrarOpciones(){
        pantallaOpciones2.pantallaOpciones.SetActive(true);
    }
}
