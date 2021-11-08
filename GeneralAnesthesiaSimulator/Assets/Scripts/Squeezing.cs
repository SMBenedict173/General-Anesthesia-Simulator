using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squeezing : MonoBehaviour
{

    public bool beingSqueezed { get; set; }
    public bool hasAirSupply { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        beingSqueezed = false;
        hasAirSupply = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (beingSqueezed)
        {
            //compress 
        }
        else if (hasAirSupply)
        {
            // expand
        }
    }
}
