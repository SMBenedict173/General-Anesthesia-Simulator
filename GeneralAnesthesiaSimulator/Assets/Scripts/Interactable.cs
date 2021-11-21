using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool completedInteraction = false;
    private bool isActive = false;

    [SerializeField]
    private int interactionsNecessary;

    [SerializeField]
    private int interactionsCompleted = 0; 


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
        this.interactionsCompleted = 0;
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
