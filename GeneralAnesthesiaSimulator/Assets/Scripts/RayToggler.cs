using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]
public class RayToggler : MonoBehaviour
{
    private XRRayInteractor rayInteractor = null;

    private float toggleDelay;

    private void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleDelay > 0)
        {
            toggleDelay -= 0.02F;
        }
    }

    public void SetEnabled(bool value)
    {
        if (toggleDelay <= 0.0F)
        {
            rayInteractor.enabled = value;
            toggleDelay = 1.0F;
        }
    }

}
