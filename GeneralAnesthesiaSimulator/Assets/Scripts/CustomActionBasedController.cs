using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;
using UnityEngine.SpatialTracking;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class CustomActionBasedController : ActionBasedController
{
    [SerializeField]
    public InputActionProperty m_SimulationPressAction;
    /// <summary>
    /// The Input System action to use for translating the interactor's attach point closer or further away from the interactor.
    /// Must be a <see cref="Vector2Control"/> Control. Will use the Y-axis as the translation input.
    /// </summary>
    public InputActionProperty simulationPressAction
    {
        get => m_SimulationPressAction;
        set => SetInputActionProperty(ref m_SimulationPressAction, value);
    }

    void EnableAllDirectActions()
    {
        m_SimulationPressAction.EnableDirectAction();
    }

    void DisableAllDirectActions()
    {
        m_SimulationPressAction.DisableDirectAction();
    }

    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }
}
