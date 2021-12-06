using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class Interactable : MonoBehaviour
{

    private bool completedInteraction = false;
    private bool isActive = false;

    [SerializeField]
    private bool autoComplete;


    [SerializeField]
    private List<GameObject> subInteractables;

    private bool isJointConnection;
    private bool isJointDisconnection;


    public void CompleteInteraction()
    {
        if (isActive)
        {
            this.completedInteraction = true;

            if(this.gameObject.GetComponent<HoseEnd>() != null)
            {
                this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            }
        }
    }

    public void SetIncomplete()
    {
        this.completedInteraction = false;
    }

    public bool GetCompletedInteraction()
    {
        return this.completedInteraction;
    }

    public void SetActive()
    {
        isActive = true;

        if (this.gameObject.GetComponent<HoseEnd>() != null)
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
        }

        SetIncomplete();

        if (autoComplete)
        {
            CompleteInteraction();
        }
    }

    public void SetInactive()
    {
        isActive = false;

        if (this.gameObject.GetComponent<HoseEnd>() != null)
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        }
    }
}
