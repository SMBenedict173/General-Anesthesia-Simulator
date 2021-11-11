using System;
using UnityEngine;

public class ToggleableLight : Toggleable
{
	public bool StartingStatus;

    public Light ThisLight;

    public float IntensityWhenActive;

	void Start()
    {
        IsActivated = StartingStatus;
    }

    void Update()
    {

    }

    protected override void Activate()
    {
        ThisLight.intensity = IntensityWhenActive;
        IsActivated = true;
    }

    protected override void Deactivate()
    {
        ThisLight.intensity = 0F;
        IsActivated = false;
    }
}
