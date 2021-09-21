using System;
using UnityEngine;

public class ValveDial : Grippable
{
    public float OffRotation;
    public float OnRotation;
    private float currentRotation;

	void Update()
    {
        if (IsActivated && currentRotation == OffRotation)
        {
            ThisTransform.rotation.Set(0, OnRotation, 0, 0);
            currentRotation = OnRotation;
        }
        else if (!IsActivated && currentRotation == OnRotation)
        {
            ThisTransform.rotation.Set(0, OffRotation, 0, 0);
            currentRotation = OnRotation;
        }
    }
}
