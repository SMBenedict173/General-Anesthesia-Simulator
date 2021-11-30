using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellowsAnimation : MonoBehaviour
{
    public float MaximumScale = 1F;
    public float MinimumScale = 0.6F;
    public float AnimationDelta;
    private float currentScale;
    private float targetScale;
    // Start is called before the first frame update
    void Start()
    {
        currentScale = MaximumScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScale != targetScale)
        {
            Vector3 newScale = new Vector3(transform.localScale.x, transform.localScale.y, targetScale);
            transform.localScale = Vector3.Slerp(transform.localScale, newScale, Time.deltaTime * AnimationDelta);
            currentScale = transform.localScale.z;
        }
    }

    public void Expand()
    {
        targetScale = MaximumScale;
    }

    public void Collapse()
    {
        targetScale = MinimumScale;
    }
    
}
