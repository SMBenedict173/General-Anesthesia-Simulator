using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;
using System;

public class RightHandController : MonoBehaviour//, XRIDefaultInputActions.IXRIRightHandActions
{
    [Tooltip("Hand associated with this controller")]
    public Hand thisHand;
    public ActionBasedController controller;

    public UnityAction grabInput;
    public bool isHolding;

    private XRRayInteractor interactor;
    private bool isPressing;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<ActionBasedController>();
        interactor = gameObject.GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        this.thisHand.SetGrip(controller.selectAction.action.ReadValue<float>());
        this.thisHand.SetTrigger(controller.activateAction.action.ReadValue<float>());

        if (isHolding && isPressing)
        {
            //Not ready until Grippable script is written:P SMB - 09/06/21
            //interactor.selectTarget.gameObject.GetComponent<Grippable>().isUsing = true;
        }
    }



    //public void OnPosition(InputAction.CallbackContext context)
    //{

    //}

    //public void OnRotation(InputAction.CallbackContext context)
    //{

    //}

    //public void OnSelect(InputAction.CallbackContext context)
    //{
    //    if (context.ReadValue<float>() > 0.4)
    //    {
    //        isHolding = true;
    //    }
    //    else
    //    {
    //        isHolding = false;
    //    }
    //}

    //public void OnActivate(InputAction.CallbackContext context)
    //{
    //    if (context.ReadValue<float>() > 0.4)
    //    {
    //        isHolding = true;
    //    }
    //    else
    //    {
    //        isHolding = false;
    //    }
    //}

    //public void OnUIPress(InputAction.CallbackContext context)
    //{
    //    //Probably useful if we implement a pause menu
    //}

    //public void OnHapticDevice(InputAction.CallbackContext context)
    //{

    //}

    //public void OnTeleportSelect(InputAction.CallbackContext context)
    //{

    //}

    //public void OnTeleportModeActivate(InputAction.CallbackContext context)
    //{

    //}

    //public void OnTeleportModeCancel(InputAction.CallbackContext context)
    //{

    //}

    //public void OnTurn(InputAction.CallbackContext context)
    //{

    //}

    //public void OnMove(InputAction.CallbackContext context)
    //{

    //}

    //public void OnRotateAnchor(InputAction.CallbackContext context)
    //{

    //}

    //public void OnTranslateAnchor(InputAction.CallbackContext context)
    //{

    //}

    //public void OnSimulationPress(InputAction.CallbackContext context)
    //{
    //    this.thisHand.SetPress(context.ReadValue<float>());
    //    if (context.ReadValue<float>() > 0.4)
    //    {
    //        isPressing = true;
    //    }
    //    else
    //    {
    //        isPressing = false;
    //    }
    //}
}
