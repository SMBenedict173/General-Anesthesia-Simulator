using System;
using UnityEngine;

public abstract class Toggleable : MonoBehaviour
{
    public bool IsActivated;

    public bool GetActivationStatus()
    {
        return this.IsActivated;
    }

    public void ToggleActivation()
    {
        bool previousActivationStatus = IsActivated;
        if (!previousActivationStatus)
        {
            Activate();

        }

        if (previousActivationStatus)
        {
            Deactivate();

        }
    }

    protected abstract void Activate();

    protected abstract void Deactivate();
    
}
