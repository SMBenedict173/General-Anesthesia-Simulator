using System;
using UnityEngine;
using UnityEngine.Events;

public class HoseEnd : MonoBehaviour
{
	[SerializeField]
	private SphereCollider triggerCollider;
	private bool canConnect;
	public HoseConnection connectedTo;
	//[SerializeField]
	//private UnityAction<SphereCollider> onEnterConnectionRange;
	//[SerializeField]
	//private UnityAction<SphereCollider> onExitConnectionRange;
	private HoseConnection connectionInRange;
	[SerializeField]
	private FixedJoint connectionJoint;

	void Start()
	{
		//onEnterConnectionRange += onTriggerEnter;
		//onExitConnectionRange += onTriggerExit;
		connectionInRange = null;
		canConnect = false;
		if (triggerCollider == null)
        {
			triggerCollider = gameObject.GetComponent<SphereCollider>();
			triggerCollider.isTrigger = false;
        }
	}

	void Update()
    {
		//Debug.Log($"{connectionInRange != null}");
    }

	public void Connect()
	{
		
		bool connectability = connectionInRange != null && canConnect;
		if (connectability)
		{
			this.connectionJoint.connectedBody = connectionInRange.gameObject.GetComponent<Rigidbody>();
			connectedTo = connectionInRange;
			canConnect = false;
			this.transform.rotation = connectionInRange.transform.rotation;
			this.transform.position = connectionInRange.transform.position;
		}
	}

	public void Disconnect()
	{
		this.connectionJoint.connectedBody = null;
		this.connectedTo = null;
	}

	private void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.GetComponent<HoseConnection>() != null)
		{
			canConnect = true;
			connectionInRange = other.gameObject.GetComponent<HoseConnection>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		canConnect = false;
		connectionInRange = null;
	}
}
