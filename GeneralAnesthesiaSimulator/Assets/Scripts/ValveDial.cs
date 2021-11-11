using System;
using UnityEngine;

public class ValveDial : Toggleable
{
    public float OffRotation;
    public float OnRotation;
    private float targetRotation;
    public Transform ThisTransform;

    public float AnimationDelta;
	void Update()
    {
        Quaternion newRotation = Quaternion.Euler(0, targetRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * AnimationDelta);
    }
     

    protected override void Deactivate()
    {
        IsActivated = false;
        targetRotation = OffRotation;
    }

    protected override void Activate()
    {
        IsActivated = true;
        targetRotation = OnRotation;
    }
    
    
}
