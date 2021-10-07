using System;
using UnityEngine;

public class ActivatableLight : MonoBehaviour, IActivatable
{
	public bool StartingStatus;

    private bool IsActive;

    public Light ThisLight;

    public float IntensityWhenActive;

    public Grippable triggerObject;

	void Start()
    {
        IsActive = StartingStatus;
    }

    void Update()
    {
        bool previousActivationStatus = IsActive;
        IsActive = GetActivationStatus();

        if (!previousActivationStatus && IsActive)
        {
            Activate();
        }

        if (previousActivationStatus && !IsActive)
        {
            DeActivate();
        }

    }

    public bool GetActivationStatus()
    {
        return triggerObject.IsActivated;
    }

    public void Activate()
    {
        ThisLight.intensity = IntensityWhenActive;
    }

    public void DeActivate()
    {
        ThisLight.intensity = 0F;
    }
}
