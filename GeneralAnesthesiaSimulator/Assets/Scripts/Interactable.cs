using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool completedInteraction = false;
    private bool isActive = false;

    private int interactionsNecessary;
    private int interactionsCompleted; 


    public void CompleteInteraction()
    {
        if (isActive)
        {
            interactionsCompleted += 1;

            if (interactionsCompleted == interactionsNecessary)
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
    }

    public void SetInactive()
    {
        isActive = false;
    }
}
