using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour
{
    [Tooltip("Animator")]
    public Animator animator;

    public UnityAction grab;
    // Start is called before the first frame update
    void Start()
    {
        //animator = this.GetComponent<Animator>(); Only necessary if not assigned in inspector
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnimateGrab()
    {
        animator.SetTrigger("Hold");
    }
    
    public void AnimatePress()
    {
        animator.SetTrigger("Press");
    }

    public void AnimateUse()
    {
        animator.SetTrigger("UseHeld");
    }
}