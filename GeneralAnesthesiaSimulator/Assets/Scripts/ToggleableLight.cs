using System;
using UnityEngine;

public class ToggleableLight : MonoBehaviour, IToggleable
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
        

    }

    //public bool GetActivationStatus()
    //{
    //    return triggerObject.IsActivated;
    //}

    private void Activate()
    {
        ThisLight.intensity = IntensityWhenActive;
        IsActive = true;
    }

    private void DeActivate()
    {
        ThisLight.intensity = 0F;
        IsActive = false;
    }

    public void ToggleActivation()
    {
        bool previousActivationStatus = IsActive;
        //bool GotInput = GetActivationStatus();

        if (!previousActivationStatus)
        {
            Activate();
        }

        if (previousActivationStatus)
        {
            DeActivate();
        }
    }
}
