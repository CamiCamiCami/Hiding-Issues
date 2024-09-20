using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PasarAlJuego : MonoBehaviour
{
    private void Awake(){
        var noDestruirEntreEscenas = FindObjectsOfType<PasarAlJuego>();
        if (noDestruirEntreEscenas.Length > 1){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
        
  
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
