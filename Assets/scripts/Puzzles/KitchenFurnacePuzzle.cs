using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Cinemachine.CinemachinePathBase;

public class KitchenFurnacePuzzle : Puzzle
{
    public GameObject Note;

    [Header("Furnace Parts")]
    public GameObject[] Stoves = new GameObject[4];
    public GameObject FurnaceDoor;
    public GameObject FurnaceContents;

    [Header("Buttons")]
    public GameObject[] StoveButtons = new GameObject[4];
    public GameObject CheckButton;

    public int[] correctCombination = new int[4];
    public int[] currentCombination = new int[4];
    private Canvas noteCanvas;

    // Start is called before the first frame update
    void Start()
    {
        this.StartPuzzle();

        System.Random random = new System.Random();
        correctCombination[0] = random.Next(1, 4);
        correctCombination[1] = random.Next(1, 4);
        correctCombination[2] = random.Next(1, 4);
        correctCombination[3] = random.Next(1, 4);
        currentCombination[0] = 1;
        currentCombination[1] = 1;
        currentCombination[2] = 1;
        currentCombination[3] = 1;

        initialPositionY = Stoves[0].transform.position.y;

        PuzzleInteractable.AddPuzzleInteractable(StoveButtons[0], this);
        PuzzleInteractable.AddPuzzleInteractable(StoveButtons[1], this);
        PuzzleInteractable.AddPuzzleInteractable(StoveButtons[2], this);
        PuzzleInteractable.AddPuzzleInteractable(StoveButtons[3], this);
        PuzzleInteractable.AddPuzzleInteractable(CheckButton, this);
        PuzzleInteractable.AddPuzzleInteractable(Note, this);

        TextMeshProUGUI text = Note.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "" + correctCombination[0] + correctCombination[1] + correctCombination[2] + correctCombination[3];

        noteCanvas = Note.GetComponentInChildren<Canvas>();
        noteCanvas.enabled = false;
    }

    private float initialPositionY;
    private void HandleStoveButtonInteracted(int stoveNumber)
    {
        const float initialScale = 0.5f;
        const float stepScale = 0.5f;
        const float stepPosition = 0.25f;

        currentCombination[stoveNumber] = currentCombination[stoveNumber] == 4 ? 1 : currentCombination[stoveNumber] + 1;

        Stoves[stoveNumber].transform.localScale = new Vector3(1, (float)(initialScale + currentCombination[stoveNumber] * stepScale), 1);
        Stoves[stoveNumber].transform.position = new Vector3(Stoves[stoveNumber].transform.position.x, (float)(initialPositionY + currentCombination[stoveNumber] * stepPosition), Stoves[stoveNumber].transform.position.z);
    }

    private void HandleCheckButtonInteracted()
    {
        bool nothingWrong = true;
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("i = " + i + " - Current = " + currentCombination[i] + " - Correct = " + correctCombination[i] + " - Result = " + (currentCombination[i] == correctCombination[i]));
            nothingWrong = nothingWrong && (currentCombination[i] == correctCombination[i]);
        }

        if (nothingWrong)
        {
            this.OnSolve();
        }
    }

    private bool lookingAtNote = false;
    private void HandleNoteInteracted(Player player)
    {
        if (lookingAtNote)
        {
            player.Mobilize();
            noteCanvas.enabled = false;
            lookingAtNote = false;
        }
        else
        {
            player.Immobilize();
            noteCanvas.enabled = true;
            lookingAtNote = true;
        }
    }

    public override void HandleComponentInteracted(PuzzleInteractable interactable, Player player)
    {
        GameObject interactedObj = interactable.gameObject;

        for(int i = 0; i < 4; i++)
        {
            if (StoveButtons[i] == interactedObj)
            {
                HandleStoveButtonInteracted(i);
            }
        }

        if (interactedObj == CheckButton)
        {
            HandleCheckButtonInteracted();
        } else if (interactedObj == Note)
        {
            HandleNoteInteracted(player);
        }
    }

    protected override void OnSolve()
    {
        // Le avisa a la habitacion
        base.OnSolve();
        // Desactiva la puerta
        FurnaceDoor.SetActive(false);
        // Pone el texto del contenido
        TextMeshPro text = FurnaceContents.GetComponentInChildren<TextMeshPro>();
        Debug.Log(this.room.GetSecurityPercentage().Value);
        Debug.Log(this.room.GetSecurityPercentage().Value + "%");
        text.text = this.room.GetSecurityPercentage().Value + "%";
    }
}
