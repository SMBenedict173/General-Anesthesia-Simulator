using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool IsMenuOpen { get; private set; }
    public GameObject menuRoot;
    //public SimulationFlowManager SimulationFlowManager; Not ready yet.
    // Start is called before the first frame update
    void Start()
    {
        this.IsMenuOpen = false;
        this.menuRoot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        IsMenuOpen = true;
        this.menuRoot.SetActive(true);
        //this.SimulationFlowManager.StopTimer(); Not ready yet
    }

    public void CloseMenu()
    {
        IsMenuOpen = true;
        this.menuRoot.SetActive(false);
        //this.SimulationFlowManager.RestartTimer(); Not ready yet
    }
}
