using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SimulationButton : Toggleable
{
	[SerializeField]
	private Collider triggerCollider;
	public Toggleable whatThisToggles;
	private Hand pressingHand;

    [SerializeField]
    private Material offMaterial;
    [SerializeField]
    private Material onMaterial;

	void Start()
    {
		IsActivated = false;
		pressingHand = null;
		triggerCollider = gameObject.GetComponent<Collider>();
    }

	new public void ToggleActivation()
    {
		IsActivated = IsActivated ? false : true;
        if (whatThisToggles != null)
        {
            whatThisToggles.ToggleActivation();
        }
        if (offMaterial != null && onMaterial != null)
        {
            this.ChangeTexture();
        }
    }
	
	private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.GetComponent<Hand>() != null)// '!= null' is unnecessary, but it's clear what the intention is
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

    public void ChangeTexture()
    {
        gameObject.GetComponent<MeshRenderer>().material = IsActivated ? onMaterial : offMaterial;
    }
}
