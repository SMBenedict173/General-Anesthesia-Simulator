using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellowsAnimation : MonoBehaviour
{
    public float MaximumScale = 1F;
    public float MinimumScale = 0.1F;
    public float AnimationDelta;
    private float currentScale;
    private float targetScale;
    [SerializeField]
    private HoseEnd simulatedLungConnection;
    [SerializeField]
    private HoseConnection correctConnection;
    [SerializeField]
    private ValveDial bagVentLever;
    [SerializeField]
    private ValveDial o2FlowMeterDial;
    [SerializeField]
    private SimulationButton powerButton;

    // Start is called before the first frame update
    void Start()
    {
        currentScale = MaximumScale;
        targetScale = MaximumScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (simulatedLungConnection.connectedTo == correctConnection &&
            bagVentLever.GetActivationStatus() &&
            o2FlowMeterDial.GetActivationStatus() &&
            powerButton.GetActivationStatus())
        {
            if (currentScale == MaximumScale)
            {
                Collapse();
            }
            else if(currentScale == MinimumScale)
            {
                Expand();
            }
            Animate();
        }
    }

    private void Animate()
    {
        if (currentScale != targetScale)
        {
            Vector3 newScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, targetScale);
            gameObject.transform.localScale = Vector3.Slerp(gameObject.transform.localScale, newScale, Time.deltaTime * AnimationDelta);
            currentScale = gameObject.transform.localScale.z;
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
