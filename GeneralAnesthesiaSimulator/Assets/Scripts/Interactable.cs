using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool completedInteraction = false;
    private bool isActive = false;

    public void CompleteInteraction()
    {
        if (isActive)
            this.completedInteraction = true;
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
