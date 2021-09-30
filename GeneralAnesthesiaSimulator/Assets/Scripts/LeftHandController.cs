using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls; 

[RequireComponent(typeof(CustomActionBasedController))]
public class LeftHandController : MonoBehaviour, XRIDefaultInputActions.IXRILeftHandActions
{
    [Tooltip("Hand associated with this controller")]
    public Hand thisHand;
    public CustomActionBasedController controller;

    private bool isHolding;

    public XRDirectInteractor interactor;
    private bool isActivating;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CustomActionBasedController>();
        interactor = gameObject.GetComponent<XRDirectInteractor>();
        this.thisHand = gameObject.GetComponentInChildren<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        
        this.thisHand.SetGrip(controller.selectAction.action.ReadValue<float>());
        
        this.thisHand.SetTrigger(controller.activateAction.action.ReadValue<float>());
        this.thisHand.SetPress(controller.simulationPressAction.ReadValue<float>());

        if (isHolding && isActivating)
        {
            //Not ready until Grippable script is written: SMB - 09/06/21
            //interactor.selectTarget.gameObject.GetComponent<Grippable>().isUsing = true;
        }
    }



    public void OnPosition(InputAction.CallbackContext context)
    {

    }

    public void OnRotation(InputAction.CallbackContext context)
    {

    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<float>());
        //if (context.ReadValue<float>() > 0.4)
        //{
        //    isHolding = true;
        //}
        //else
        //{
        //    isHolding = false;
        //}
    }

    public void OnActivate(InputAction.CallbackContext context)
    {
        //if (context.ReadValue<float>() > 0.4)
        //{
        //    Debug.Log(context.ReadValue<float>());
        //    isHolding = true;
        //}
        //else
        //{
        //    isHolding = false;
        //}
    }

    public void OnUIPress(InputAction.CallbackContext context)
    {
        //Probably useful if we implement a pause menu
    }

    public void OnHapticDevice(InputAction.CallbackContext context)
    {

    }

    public void OnTeleportSelect(InputAction.CallbackContext context)
    {

    }

    public void OnTeleportModeActivate(InputAction.CallbackContext context)
    {

    }

    public void OnTeleportModeCancel(InputAction.CallbackContext context)
    {

    }

    public void OnTurn(InputAction.CallbackContext context)
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {

    }

    public void OnRotateAnchor(InputAction.CallbackContext context)
    {

    }

    public void OnTranslateAnchor(InputAction.CallbackContext context)
    {

    }

    public void OnSimulationPress(InputAction.CallbackContext context)
    {
        this.thisHand.SetPress(context.ReadValue<float>());
        Debug.Log($"PressInput: {context.ReadValue<float>()}");
    }
}
