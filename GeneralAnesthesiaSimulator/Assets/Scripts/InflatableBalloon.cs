using System;
using UnityEngine;
using UnityEngine.Time;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(Squeezing))]
public class InflatableBalloon : MonoBehaviour
{
    [SerializeField]
    private Hose connectedHose;
    [SerializeField]
    private ValveDial yellowVaporizer;
    [SerializeField]
    private ValveDial purpleVaporizer;
    [SerializeField]
    private Squeezing squeezing;
    private bool isYellowChecked;
    private bool isPurpleChecked;

    private float timeCompressed;
    private float timeCompressionStarted;
    
    void Start()
    {
        timeCompressed = 0.0F;
        isPurpleChecked = false;
        isYellowChecked = false;
        squeezing.IsActivated = false;
    }

    void Update()
    {
        if (yellowVaporizer.GetActivationStatus() && !squeezing.autoInflateActivated && timeCompressed >= 10.0F)
        {
            isYellowChecked = true;
        }
        if (purpleVaporizer.GetActivationStatus() && !squeezing.autoInflateActivated && timeCompressed >= 10.0F)
        {
            isPurpleChecked = true;
        }
    }

    public void ManuallyCompress()
    {
        squeezing.isActivated = true;
        timeCompressed = 0F;
        timeCompressionStarted = Time.time;
    }

    public void DeCompress()
    {
        if ((yellowVaporizer.GetActivationStatus() || purpleVaporizer.GetActivationStatus()) && connectedHose.GetConnectionStatus)
        {
            squeezing.autoInflateActivated = false;
        }
        else
        {
            squeezing.autoInflateActivated = true;
            float timeCompressionStopped = Time.time;
            timeCompressed = timeCompressionStopped - timeCompressionStarted;
        }
    }

    public bool IsYellowCompleted()
    {
        return this.isYellowChecked;
    }

    public bool IsPurpleCompleted()
    {
        return this.IsPurpleCompleted;
    }
}