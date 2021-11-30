using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Sensor : MonoBehaviour
{
    [SerializeField]
    public O2SensorConnection connectedTo;
    private O2SensorConnection connectionInRange;
    [SerializeField]
    private FixedJoint connectionJoint;
    private bool canConnect;

    private Quaternion originalRotation;
    private Vector3 originalPosition;

    private bool isConnected;
    // Start is called before the first frame update
    void Start()
    {
        connectionInRange = connectedTo;
        canConnect = false;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        isConnected = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleConnection()
    {
        Debug.Log("here");

        if (isConnected)
        {
            Disconnect();
        }
        else
        {
            Connect();
        }
    }

    public void Connect()
    {

        bool connectability = connectionInRange != null && canConnect;
        if (connectability)
        {
            Debug.Log("Connection is possible");
            this.connectionJoint.connectedBody = connectionInRange.gameObject.GetComponent<Rigidbody>();
            connectedTo = connectionInRange;
            canConnect = false;
            this.transform.rotation = originalRotation;
            this.transform.position = originalPosition;

            this.isConnected = true;
        }
    }

    public void Disconnect()
    {
        this.connectionJoint.connectedBody = null;
        this.connectedTo = null;
        this.isConnected = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<O2SensorConnection>() != null)
        {
            canConnect = true;
            connectionInRange = other.gameObject.GetComponent<O2SensorConnection>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canConnect = false;
        connectionInRange = null;
    }
}
