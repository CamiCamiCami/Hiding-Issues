using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    public Camera CameraComponent;


    private Vector3 ScreenCentre;
    private PuzzleInteractable interactable = null;

    // Start is called before the first frame update
    void Start()
    {
        ScreenCentre = new Vector3(Screen.width / 2, Screen.height / 2, CameraComponent.nearClipPlane);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraCenter = CameraComponent.ScreenToWorldPoint(ScreenCentre);
        RaycastHit hit;
        if (Physics.Raycast(CameraCenter, CameraComponent.transform.forward, out hit, 10f) && hit.collider.gameObject.TryGetComponent<PuzzleInteractable>(out var interact))
        {
            interactable = interact;
        } else
        {
            interactable = null;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable.WhenInteracted();
        }
    }
}
