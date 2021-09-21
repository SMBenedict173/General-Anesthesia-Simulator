using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableKinematics : MonoBehaviour
{
    public Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisablKinematicProperty()
    {
        rigid.isKinematic = false;
    }

    public void ReenableKinematicProperty()
    {
        rigid.isKinematic = true;
    }
}
