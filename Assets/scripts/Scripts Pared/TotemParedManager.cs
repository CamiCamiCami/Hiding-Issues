using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotemParedManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown () {

        Debug.Log("potencio");

        UnityEngine.SceneManagement.SceneManager.LoadScene("Pared Puzzle");

    }

}
