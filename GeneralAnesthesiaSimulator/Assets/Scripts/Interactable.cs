using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool completedInteraction;


    void Start()
    {
        completedInteraction = false;
    }

    public void completeInteraction()
    {
        this.completedInteraction = true;
    }

    public bool getCompletedInteraction()
    {
        return this.completedInteraction;
    }
}
