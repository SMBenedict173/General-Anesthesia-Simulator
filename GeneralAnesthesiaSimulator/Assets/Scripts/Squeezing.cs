using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squeezing : MonoBehaviour
{
    public bool isActivated;

    public bool autoInflateActivated;

    public Vector3 changeAmount;

    public void Start()
    {
        isActivated = false;
        autoInflateActivated = false;
    }

    public void Update()
    {
        if (isActivated)
            transform.localScale -= changeAmount;
        else if (autoInflateActivated)
            transform.localScale += changeAmount;
    }
}
