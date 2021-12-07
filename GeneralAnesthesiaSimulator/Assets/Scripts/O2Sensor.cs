using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HoseEnd))]
public class O2Sensor : MonoBehaviour
{
    private Quaternion originalRotation;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }


    public void Connect()
    {

        if (this.gameObject.GetComponent<HoseEnd>().IsConnected())
        {
            this.gameObject.transform.position = originalPosition;
            this.gameObject.transform.rotation = originalRotation;
        }
    }
}
