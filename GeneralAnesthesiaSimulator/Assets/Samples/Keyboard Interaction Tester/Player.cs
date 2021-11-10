using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputMaster controls;

    [SerializeField]
    public InputMaster test;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Activate.performed += ctx => Activate();
        controls.Player.Deactivate.performed += ctx => Deactivate();
    }

    void Activate()
    {
        gameObject.GetComponent<Rotation>().isActivated = true;
    }

    void Deactivate()
    {
        gameObject.GetComponent<Rotation>().isActivated = false;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
