using UnityEngine;
using System;

public class SimulatedLung : MonoBehaviour
{
    public float MaximumScale = 1.5F;
    public float MinimumScale = 1F;
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
        currentScale = MinimumScale;
        targetScale = MinimumScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (simulatedLungConnection.connectedTo == correctConnection &&
            bagVentLever.GetActivationStatus() &&
            o2FlowMeterDial.GetActivationStatus() &&
            powerButton.GetActivationStatus())
        {
            Debug.Log("All conditions required for the simulated lung to animate are met.");
            if (currentScale == MaximumScale)
            {
                Collapse();
            }
            else if (currentScale == MinimumScale)
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
            Vector3 newScale = new Vector3(gameObject.transform.localScale.x, targetScale, targetScale);
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