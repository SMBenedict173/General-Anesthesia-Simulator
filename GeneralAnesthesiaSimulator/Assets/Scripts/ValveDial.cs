using System;
using UnityEngine;

public class ValveDial : Grippable, IToggleable
{
    [Tooltip("Animator")]
    public Animator animator;
    public float OffRotation;
    public float OnRotation;
    private float currentRotation;
    private float targetRotation;

    private string rotateAnimationParameter = "Rotate";
    public float AnimationDelta;
	void Update()
    {
        //Attempt without using animations
        //if (IsActivated && currentRotation == OffRotation)
        //{
        //    ThisTransform.rotation.Set(0, OnRotation, 0, 0);
        //    currentRotation = OnRotation;
        //}
        //else if (!IsActivated && currentRotation == OnRotation)
        //{
        //    ThisTransform.rotation.Set(0, OffRotation, 0, 0);
        //    currentRotation = OnRotation;
        //}

        //Attempt using animations...Requires all dials and valves to have animations
        if (currentRotation != targetRotation)
        {
            currentRotation = Mathf.MoveTowards(currentRotation, targetRotation, Time.time * AnimationDelta);
            animator.SetFloat(rotateAnimationParameter, currentRotation);
        }
    }
     
    public void ToggleActivation()
    {
        if (IsActivated)
        {
            IsActivated = false;
            targetRotation = OffRotation;
        }
        else
        {
            IsActivated = true;
            targetRotation = OnRotation;
        }
    }
}
