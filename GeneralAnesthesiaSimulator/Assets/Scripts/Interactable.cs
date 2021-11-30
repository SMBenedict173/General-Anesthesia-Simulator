using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool completedInteraction = false;
    private bool isActive = false;

    [SerializeField]
    private bool autoComplete;


    public void CompleteInteraction()
    {
        if (isActive)
        {
            this.completedInteraction = true;
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

        SetIncomplete();

        if (autoComplete)
        {
            completedInteraction = true;
        }
    }

    public void SetInactive()
    {
        isActive = false;
    }
}
