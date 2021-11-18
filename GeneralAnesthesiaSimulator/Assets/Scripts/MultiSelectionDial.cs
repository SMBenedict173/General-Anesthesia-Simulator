using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MultiSelectionDial : MonoBehaviour
{
    [SerializeField]
    private List<float> selectionRotations;
    private float targetRotation;
    private float currentRotation;
    public char AxisOfRotation;
    public float AnimationDelta;
    private int selectionIndex; 
    private float x;
    private float y;
    private float z;
    
    // Start is called before the first frame update
    void Start()
    {
        if (char.IsWhiteSpace(AxisOfRotation))
        {
            throw new Exception("Axis of rotation must be specified.");
        }
        selectionIndex = 0;
        targetRotation = selectionRotations[selectionIndex];
        x = gameObject.transform.eulerAngles.x;
        y = gameObject.transform.eulerAngles.y;
        z = gameObject.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion newRotation = Quaternion.Euler(x, y, z);
        switch (AxisOfRotation)
        {
            case 'x':
            case 'X':
                newRotation = Quaternion.Euler(targetRotation, y, z);
                break;
            case 'y':
            case 'Y':
                newRotation = Quaternion.Euler(x, targetRotation, z);
                break;
            case 'z':
            case 'Z':
                newRotation = Quaternion.Euler(x, y, targetRotation);
                break;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * AnimationDelta);
    }

    public void ChangeSelection()
    {
        Debug.Log(selectionIndex);
        selectionIndex = (selectionIndex) < selectionRotations.Count - 1 ? selectionIndex + 1 : 0;
        targetRotation = selectionRotations[selectionIndex];
    }
}
