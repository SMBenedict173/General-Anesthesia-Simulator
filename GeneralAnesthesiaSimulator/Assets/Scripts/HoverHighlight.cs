using UnityEngine;

[RequireComponent(typeof(Light))]
public class HoverHighlight : MonoBehaviour
{
    public Light Halo;
    public float MaximumIntensity;

    public float IntensityDelta;
    private float CurrentIntensity;
    // Start is called before the first frame update
    void Start()
    {
        CurrentIntensity = 0F;
        Halo.intensity = CurrentIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHighlight()
    {
        while(CurrentIntensity < MaximumIntensity)
        {
            CurrentIntensity = Mathf.MoveTowards(CurrentIntensity, MaximumIntensity, Time.time * IntensityDelta);
            Halo.intensity = CurrentIntensity;
        }
    }

    public void DisableHighlight()
    {
        while (CurrentIntensity > 0F)
        {
            CurrentIntensity = Mathf.MoveTowards(CurrentIntensity, 0F, Time.time * IntensityDelta);
            Halo.intensity = CurrentIntensity;
        }
    }
}
