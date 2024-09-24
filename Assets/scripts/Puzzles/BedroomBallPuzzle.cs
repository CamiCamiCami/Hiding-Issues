using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedroomBallPuzzle : Puzzle
{


    public SceneAsset Scene;
    public GameObject Ball;


    // Start is called before the first frame update
    void Start()
    {
        base.StartPuzzle();

        PuzzleInteractable.AddPuzzleInteractable(Ball, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void HandleComponentInteracted(PuzzleInteractable interactable, Player player)
    {
        SceneManager.LoadScene(Scene.name);
    }

}
