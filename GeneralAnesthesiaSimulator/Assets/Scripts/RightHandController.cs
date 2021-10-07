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

    private XRDirectInteractor interactor;
    private bool isPressing;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<ActionBasedController>();
        interactor = gameObject.GetComponent<XRDirectInteractor>();
        thisHand = gameObject.GetComponentInChildren(typeof(Hand)) as Hand;
    }

    // Update is called once per frame
    void Update()
    {
        float selectInputValue = controller.selectAction.action.ReadValue<float>();
        this.thisHand.SetGrip(selectInputValue);
        if (selectInputValue > 0.4 && interactor.selectTarget != null)
        {
            isHolding = true;
        }
        else
        {
            isHolding = false;
            if (thisHand.transform.parent != gameObject.transform)
            {
                thisHand.transform.SetParent(gameObject.transform, false);
            }
        }

        float activateInputValue = controller.activateAction.action.ReadValue<float>();
        this.thisHand.SetTrigger(activateInputValue);

        if (activateInputValue > 0.4 && isHolding)
        {
            isPressing = true;
        }
        else
        {
            isPressing = false;
        }
        if (isHolding)
        {
            ValveDial heldDial = interactor.selectTarget.gameObject.GetComponent<ValveDial>();
            if (heldDial != null && thisHand.transform.parent != heldDial.transform)
            {
                thisHand.transform.SetParent(heldDial.transform, false);
            }
            ////try
            ////{
            ////    ValveDial heldDial = interactor.selectTarget.gameObject.GetComponent<ValveDial>();
            //      thisHand.thisHand.gameObject.transform.setParent(heldDial.gameObject);
            ////}
            //Not ready until Grippable script is written:P SMB - 09/06/21
            //interactor.selectTarget.gameObject.GetComponent<Grippable>().isUsing = true;

            if (isHolding && isPressing)
            {
                interactor.selectTarget.gameObject.GetComponent<Grippable>().Activate();
            }
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
