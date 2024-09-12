using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallClockPuzzle : Puzzle
{

    public GameObject Minute;
    public GameObject Hour;
    public int RequiredHour = 12;
    public int RequiredMinute = 30;
    private int currentHour;
    private int currentMinute;
    // Start is called before the first frame update
    void Start()
    {
        base.StartPuzzle();
        PuzzleInteractable.AddPuzzleInteractable(Minute.transform.GetChild(0).gameObject, this);
        PuzzleInteractable.AddPuzzleInteractable(Hour.transform.GetChild(0).gameObject, this);

        currentHour = ((int)Mathf.Floor(Hour.transform.eulerAngles.x / 30f) + 9) % 12;
        currentMinute = ((int)Mathf.Floor(Minute.transform.eulerAngles.x / 30f) + 45) % 60;
    }

    public override void HandleComponentInteracted(PuzzleInteractable interactable)
    {
        GameObject useful_interact = interactable.transform.parent.gameObject;
        if (useful_interact == Minute)
        {
            currentMinute = (currentMinute + 5) % 60;
            Minute.transform.Rotate(30f, 0f, 0f);
        } else if (useful_interact == Hour)
        {
            currentHour = (currentHour + 1) % 12;
            Hour.transform.Rotate(30f, 0f, 0f);
        }

        if (currentMinute == RequiredMinute && currentHour == RequiredHour)
        {
            base.OnSolve();
        }
    }
}
