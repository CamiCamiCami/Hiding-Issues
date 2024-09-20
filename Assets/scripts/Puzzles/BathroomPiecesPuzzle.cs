using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomPiecesPuzzle : Puzzle
{

    public GameObject ToiletModel;
    public GameObject SinkModel;
    public GameObject ShowerModel;

    private bool toiletInteracted = false;
    private bool sinkInteracted = false;
    private bool showerInteracted = false;

    // Start is called before the first frame update
    void Start()
    {
        base.StartPuzzle();
        PuzzleInteractable.AddPuzzleInteractable(ToiletModel, this);
        PuzzleInteractable.AddPuzzleInteractable(SinkModel, this);
        PuzzleInteractable.AddPuzzleInteractable(ShowerModel, this);
    }

    public override void HandleComponentInteracted(PuzzleInteractable interactable, Player player)
    {
        Debug.Log("");
        GameObject interacted_obj = interactable.gameObject;

        if (interacted_obj == ToiletModel)
        {
            toiletInteracted = true;
        } else if (interacted_obj == SinkModel)
        {
            sinkInteracted = true;
        } else if (interacted_obj == ShowerModel)
        {
            showerInteracted = true;
        }

        bool isFinished = toiletInteracted && sinkInteracted && showerInteracted;
        if (isFinished)
        {
            base.OnSolve();
        }
    }
}
