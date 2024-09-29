using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jugar(string jueguito){
        UnityEngine.SceneManagement.SceneManager.LoadScene(jueguito);
    }
    
    public void Salir(){
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}
