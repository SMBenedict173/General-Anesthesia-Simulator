using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class Interactable : MonoBehaviour
{

    private bool completedInteraction = false;
    private bool isActive = false;

    [SerializeField]
    private bool autoComplete;


    [SerializeField]
    private List<GameObject> subInteractables;

    private bool isJointConnection;
    private bool isJointDisconnection;


    public void CompleteInteraction()
    {
        if (isActive)
        {
            this.completedInteraction = true;

            if (isJointConnection || isJointDisconnection)
            {
                this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;

                foreach (GameObject obj in subInteractables)
                {
                    obj.GetComponent<XRGrabInteractable>().enabled = true;
                    obj.GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

    public void SetIncomplete()
    {
        this.completedInteraction = false;
    }

    public bool GetCompletedInteraction()
    {
        return this.completedInteraction;
    }

    public void SetActive()
    {
        // if it is a joint interactable
        if (this.gameObject.GetComponent<HoseEnd>() != null)
        {
            //check if it's currently connected to something
            //(if it is, then this task is disconnect. If not, then this task is connect)

            if (this.gameObject.GetComponent<HoseEnd>().IsConnected())
            {
                this.isJointConnection = false;
                this.isJointDisconnection = true;
            }
            else
            {
                this.isJointConnection = true;
                this.isJointDisconnection = false;
            }

            foreach (GameObject obj in subInteractables)
            {
                obj.GetComponent<XRGrabInteractable>().enabled = false;
                obj.GetComponent<Collider>().enabled = false;
            }

            this.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
        }

        isActive = true;
        SetIncomplete();

        if (autoComplete)
        {
            CompleteInteraction();
        }

    }

    public void SetInactive()
    {
        isActive = false;

        if (isJointConnection || isJointDisconnection)
        {
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;

            foreach(GameObject obj in subInteractables)
            {
                obj.GetComponent<XRGrabInteractable>().enabled = true;
                obj.GetComponent<Collider>().enabled = true;
            }
        }
    }

    public void JointDisconnected()
    {
        if (this.isJointDisconnection)
            CompleteInteraction();
    }

    public void JointConnected()
    {
        if (this.isJointConnection)
            CompleteInteraction();
    }
}
