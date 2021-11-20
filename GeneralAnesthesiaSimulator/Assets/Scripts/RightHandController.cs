using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class RightHandController : MonoBehaviour//, XRIDefaultInputActions.IXRIRightHandActions
{
    [Tooltip("Hand associated with this controller")]
    public Hand thisHand;
    public ActionBasedController controller;

    [SerializeField]
    private InputActionReference simulationPressAction;
    public bool isHolding;
    [SerializeField]
    private XRDirectInteractor interactor;
    private bool isPressing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float selectInputValue = controller.selectAction.action.ReadValue<float>();
        this.thisHand.SetGrip(selectInputValue);
        try
        {
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
                    thisHand.EnableRenderer();
                }
            }
        }
        catch (NullReferenceException)
        {
            isHolding = false;
            if (thisHand.transform.parent != gameObject.transform)
            {
                thisHand.transform.SetParent(gameObject.transform, false);
                thisHand.EnableRenderer();
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
                thisHand.DisableRenderer();
            }
            MultiSelectionDial heldMultiDial = interactor.selectTarget.gameObject.GetComponent<MultiSelectionDial>();
            if (heldMultiDial != null && thisHand.transform.parent != heldMultiDial.transform)
            {
                thisHand.transform.SetParent(heldMultiDial.transform, false);
                thisHand.DisableRenderer();
            }
        }

        this.thisHand.SetPress(simulationPressAction.action.ReadValue<float>());
    }
}
