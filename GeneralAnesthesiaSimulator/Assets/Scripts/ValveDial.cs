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
    private float x;
    private float y;
    private float z;
    void Start()
    {
        if (char.IsWhiteSpace(AxisOfRotation))
        {
            throw new Exception("Axis of rotation must be specified.");
        }
        x = gameObject.transform.eulerAngles.x;
        y = gameObject.transform.eulerAngles.y;
        z = gameObject.transform.eulerAngles.z;
    }
    void Update()
    {
        Quaternion newRotation = Quaternion.Euler(x, y, z);
        switch (AxisOfRotation)
        {
            case 'x':
            case 'X':
                newRotation = Quaternion.Euler(targetRotation, y, z);
                break;
            case 'y':
            case 'Y':
                newRotation = Quaternion.Euler(x, targetRotation, z);
                break;
            case 'z':
            case 'Z':
                newRotation = Quaternion.Euler(x, y, targetRotation);
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
