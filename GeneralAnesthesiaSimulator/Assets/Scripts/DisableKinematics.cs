using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableKinematics : MonoBehaviour
{
    public Rigidbody rigid;
    public Collider Collider;

    // Start is called before the first frame update
    void Start()
    {
        rigid.isKinematic = false;
        Collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableKinematicProperty()
    {
        rigid.isKinematic = false;
        Collider.enabled = true;
    }

    public void ReenableKinematicProperty()
    {
        rigid.isKinematic = true;
        Collider.enabled = false;
    }
}
