using System;
using UnityEngine;
using UnityEngine.Events;

public class HoseEnd : MonoBehaviour
{
	[SerializeField]
	private SphereCollider triggerCollider;
	private bool canConnect;
	public HoseConnection connectedTo;
	private UnityAction<SphereCollider> onEnterConnectionRange;
	private UnityAction<SphereCollider> onExitConnectionRange;
	private HoseConnection connectionInRange;
	[SerializeField]
	private FixedJoint connectionJoint;

	void Start()
	{
		onEnterConnectionRange += onTriggerEnter;
		onExitConnectionRange += onTriggerExit;
		connectionInRange = null;
		canConnect = false;
		if (triggerCollider == null)
        {
			triggerCollider = gameObject.GetComponent<SphereCollider>();
			triggerCollider.isTrigger = true;
        }
	}

	public void Connect()
	{
		if (connectionInRange != null && canConnect)
		{
			this.connectionJoint.connectedBody = connectionInRange.gameObject.GetComponent<Rigidbody>();
			connectedTo = connectionInRange;
			canConnect = false;
		}
	}

	public void Disconnect()
	{
		this.connectionJoint.connectedBody = null;
		this.connectedTo = null;
	}

	private void onTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<HoseConnection>() != null)
		{
			canConnect = true;
			connectionInRange = other.gameObject.GetComponent<HoseConnection>();
		}
		else
		{
			canConnect = false;
		}
	}

	private void onTriggerExit(Collider other)
	{
		canConnect = false;
		connectionInRange = null;
	}
}
