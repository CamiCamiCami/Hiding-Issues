using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotemBallManager : MonoBehaviour
{

    public SceneAsset ballScene;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseDown () {

        RoundManager.Instance.OpenPuzzleScene(ballScene);

    }

}
