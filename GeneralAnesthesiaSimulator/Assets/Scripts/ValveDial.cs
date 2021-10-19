using System;
using UnityEngine;

public class ValveDial : Grippable, IToggleable
{
    public float OffRotation;
    public float OnRotation;
    private float targetRotation;

    public float AnimationDelta;
	void Update()
    {
        Quaternion newRotation = Quaternion.Euler(0, targetRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * AnimationDelta);
    }
     

    private void Deactivate()
    {
        IsActivated = false;
        targetRotation = OffRotation;
    }

    private void Activate()
    {
        IsActivated = true;
        targetRotation = OnRotation;
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
}
