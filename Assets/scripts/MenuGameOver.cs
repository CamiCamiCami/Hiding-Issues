using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void IrAlMenu(string menu){
        UnityEngine.SceneManagement.SceneManager.LoadScene(menu);
    }

    public void restart(string jueguito){
        UnityEngine.SceneManagement.SceneManager.LoadScene(jueguito);
        Debug.Log("Reseteando ando");
    }
}
