using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool IsMenuOpen { get; private set; }
    public Canvas MenuCanvas;
    //public SimulationFlowManager SimulationFlowManager; Not ready yet.
    // Start is called before the first frame update
    void Start()
    {
        this.IsMenuOpen = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        IsMenuOpen = true;
        this.MenuCanvas.enabled = true;
        //this.SimulationFlowManager.StopTimer(); Not ready yet
    }

    public void CloseMenu()
    {
        IsMenuOpen = true;
        this.MenuCanvas.enabled = false;
        //this.SimulationFlowManager.RestartTimer(); Not ready yet
    }
}
