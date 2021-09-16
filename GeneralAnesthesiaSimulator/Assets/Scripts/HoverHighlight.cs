using UnityEngine;

[RequireComponent(typeof(Light))]
public class HoverHighlight : MonoBehaviour
{
    public Light Halo;
    public float MaximumIntensity;

    private float IntensityDelta;
    //private float CurrentIntensity;

    public int SmoothingFactor;
    // Start is called before the first frame update
    void Start()
    {
        IntensityDelta = MaximumIntensity / (float)SmoothingFactor;
        Halo.intensity = 0F;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHighlight()
    {
        for (int i = 0; i < SmoothingFactor; i++)
        {
            Halo.intensity += IntensityDelta;
        }

        //while(CurrentIntensity < MaximumIntensity)
        //{
        //    CurrentIntensity = Mathf.MoveTowards(CurrentIntensity, MaximumIntensity, Time.time * IntensityDelta);
        //    Halo.intensity = CurrentIntensity;
        //}
    }

    public void DisableHighlight()
    {
        for (int i = 0; i < SmoothingFactor; i++)
        {
            Halo.intensity -= IntensityDelta;
        }
    }
}
