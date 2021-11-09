using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    public bool isActivated;
    public bool clockwiseTurn;
    public float changeAmount;

    public void Start()
    {
        isActivated = false;
        clockwiseTurn = true;
    }

    public void Update()
    {
        if (isActivated && clockwiseTurn)
            transform.Rotate(Vector3.up * changeAmount);
        else if (isActivated && !clockwiseTurn)
            transform.Rotate(Vector3.down * changeAmount);
    }
}
