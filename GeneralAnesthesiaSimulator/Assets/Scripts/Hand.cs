using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Serializable]
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<animator>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }
}