using System;
using UnityEngine;

public class ValveDial : Toggleable
{
    public float OffRotation;
    public float OnRotation;
    private float targetRotation;
    public Transform ThisTransform;
    public char AxisOfRotation;

    public float AnimationDelta;

    void Start()
    {
        if (char.IsWhiteSpace(AxisOfRotation))
        {
            throw new Exception("Axis of rotation must be specified.");
        }
    }
    void Update()
    {
        Quaternion newRotation = Quaternion.Euler(0, 0, 0);
        switch (AxisOfRotation)
        {
            case 'x':
            case 'X':
                newRotation = Quaternion.Euler(targetRotation, 0, 0);
                break;
            case 'y':
            case 'Y':
                newRotation = Quaternion.Euler(0, targetRotation, 0);
                break;
            case 'z':
            case 'Z':
                newRotation = Quaternion.Euler(0, 0, targetRotation);
                break;
        }
        
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
