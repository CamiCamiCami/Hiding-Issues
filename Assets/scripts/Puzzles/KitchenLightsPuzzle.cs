using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KitchenLightsPuzzle : Puzzle
{
    public GameObject[] Ligths = new GameObject[8];

    [Header("Note")]
    public GameObject Note;
    public GameObject Result;
    public InputField Input;

    private int solution;

    // Start is called before the first frame update
    void Start()
    {
        base.StartPuzzle();

        PuzzleInteractable.AddPuzzleInteractable(Note, this);
        System.Random random = new System.Random();
        solution = random.Next(0,255);

        Result.SetActive(false);
        
        for (int mask = 1, i = 0; i < 8; mask = mask << 1, i++) {
            Ligths[i].SetActive((mask & solution) != 0);
        }
    }

    private bool solved = false;
    void update() {
        if (solved) {
            return;
        }
        if (Input.text == solution.ToString()) {
            solved = true;
            base.OnSolve();

            Result.SetActive(true);
            TextMeshProUGUI text = Note.GetComponentInChildren<TextMeshProUGUI>();
            text.text = this.room.GetSecurityPercentage() + "%";



        }
    }


    private bool lookingAtNote = false;
    public override void HandleComponentInteracted(PuzzleInteractable interactable, Player player) 
    {
        if (interactable.gameObject == Note) {
            if (lookingAtNote) {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Canvas canvas = Note.GetComponentInChildren<Canvas>();
                canvas.enabled = false;
                player.Mobilize();
                lookingAtNote = false;
            } else {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Canvas canvas = Note.GetComponentInChildren<Canvas>();
                canvas.enabled = true;
                player.Immobilize();
                lookingAtNote = true;
            }
        }
    }
}
