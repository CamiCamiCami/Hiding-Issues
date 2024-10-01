using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

[RequireComponent(typeof(Collider))]
public class PuzzleInteractable : MonoBehaviour
{
    public Puzzle ReportBackTo;

    private Outline outline;
    private void Awake()
    {
        Color outlineColor = Color.white;
        float outlineWidth = 5f;
        outline = this.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;
        outline.enabled = false;
    }

    public void WhenInteracted(Player player)
    {
        ReportBackTo.HandleComponentInteracted(this, player);
    }

    public void WhenSelected()
    {
        outline.enabled = true;
    }

    public void WhenUnselected()
    {
        outline.enabled = false;
    }

    public static PuzzleInteractable AddPuzzleInteractable(GameObject victim, Puzzle report_to)
    {
        PuzzleInteractable component = victim.AddComponent<PuzzleInteractable>();
        component.ReportBackTo = report_to;
        return component;
    }
}
