using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;
using System;

public class RightHandController : MonoBehaviour
{
    [Tooltip("Hand associated with this controller")]
    public Hand thisHand;
    public ActionBasedController controller;
    [SerializeField]
    private InputActionReference simulationPressAction;
    private bool isHolding;

    public XRDirectInteractor interactor;
    private bool isActivating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.thisHand.SetGrip(controller.selectAction.action.ReadValue<float>());
        this.thisHand.SetTrigger(controller.activateAction.action.ReadValue<float>());
        this.thisHand.SetPress(simulationPressAction.action.ReadValue<float>());

        if (isHolding && isActivating)
        {
            //Not ready until Grippable script is written:P SMB - 09/06/21
            //interactor.selectTarget.gameObject.GetComponent<Grippable>().isUsing = true;
        }
    }
}
