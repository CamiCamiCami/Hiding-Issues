using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotemBallManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseDown () {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Ball Puzzle");

    }

}
