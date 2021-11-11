﻿using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SimulationButton : Toggleable
{
	[SerializeField]
	private Collider triggerCollider;
	public Toggleable whatThisToggles;
	private Hand pressingHand;

	void Start()
    {
		IsActivated = false;
		pressingHand = null;
		triggerCollider = gameObject.GetComponent<Collider>();
    }

	new public void ToggleActivation()
    {
        Debug.Log("Morestuff");
		IsActivated = IsActivated ? false : true;
		whatThisToggles.ToggleActivation();
    }
	
	private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Stuff");
		if (other.gameObject.GetComponent<Hand>() != null)
        {
			pressingHand = other.gameObject.GetComponent<Hand>();
			this.ToggleActivation();
        }
        else
        {
			pressingHand = null;
        }
    }

	private void OnTriggerExit(Collider other)
    {
		pressingHand = null;
    }

    protected override void Activate()
    {
        throw new NotImplementedException();
    }

    protected override void Deactivate()
    {
        throw new NotImplementedException();
    }
}