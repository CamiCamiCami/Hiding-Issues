using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Room))]
public abstract class Puzzle : MonoBehaviour
{
    public bool isSolved = false;
    protected Room room;

    protected void StartPuzzle()
    {
        room = this.GetComponent<Room>();
    }

    protected virtual void OnSolve()
    {
        isSolved = true;
    }

    public virtual string getName()
    {
        return this.GetType().Name;
    }

    public abstract void HandleComponentInteracted(PuzzleInteractable interactable, Player player);
}
