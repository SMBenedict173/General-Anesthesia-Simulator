using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenActivation : Toggleable
{
    [SerializeField]
    private Material offMaterial;
    [SerializeField]
    private Material screenOneOnMaterial;
    [SerializeField]
    private Material screenTwoOnMaterial;

    [SerializeField]
    private MeshRenderer screenOneRenderer;
    [SerializeField]
    private MeshRenderer screenTwoRenderer;
    // Start is called before the first frame update
    void Start()
    {
        screenOneRenderer.material = offMaterial;
        screenTwoRenderer.material = offMaterial;
        this.IsActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Activate()
    {
        IsActivated = true;
        screenOneRenderer.material = screenOneOnMaterial;
        screenTwoRenderer.material = screenTwoOnMaterial;
    }
    
    protected override void Deactivate()
    {
        IsActivated = false;
        screenOneRenderer.material = offMaterial;
        screenTwoRenderer.material = offMaterial;
    }
}
