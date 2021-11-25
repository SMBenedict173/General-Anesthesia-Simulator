using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRDirectInteractor))]
public class DirectInteractorToggler : MonoBehaviour
{
    private XRDirectInteractor directInteractor = null;

    private float toggleDelay;

    private void Awake()
    {
        directInteractor = GetComponent<XRDirectInteractor>();
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
            directInteractor.enabled = value;
            toggleDelay = 1.0F;
        }
    }
}
