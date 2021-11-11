using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    public bool isActivated;
    public bool clockwiseTurn;
    public float changeAmount;
    public float minRotation;
    public float maxRotation;
    public char axis;
    private float currentRotation;

    public void Start()
    {
        isActivated = false;
        clockwiseTurn = true;
    }

    public void Update()
    {
        switch(axis){
            case 'x':
                currentRotation = gameObject.transform.eulerAngles.x;
                break;
            case 'y':
                currentRotation = gameObject.transform.eulerAngles.y;
                break;
            case 'z':
                currentRotation = gameObject.transform.eulerAngles.z;
                break;

        }

        Debug.Log(currentRotation);



        if (isActivated && clockwiseTurn && currentRotation + changeAmount < maxRotation)
            transform.Rotate(Vector3.up * changeAmount);

        else if (isActivated && !clockwiseTurn && currentRotation - changeAmount > minRotation)
            transform.Rotate(Vector3.down * changeAmount);



        float test = gameObject.transform.rotation.x;
    }
}
