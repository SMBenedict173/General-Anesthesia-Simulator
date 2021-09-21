using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public abstract class Grippable : MonoBehaviour
{
    public Transform ThisTransform;

    public bool IsActivated;


    void Start()
    {
        IsActivated = false;
    }

    public void Activate()
    {
        IsActivated = true;
    }

    public void Deactivate()
    {
        IsActivated = false;
    }
}
