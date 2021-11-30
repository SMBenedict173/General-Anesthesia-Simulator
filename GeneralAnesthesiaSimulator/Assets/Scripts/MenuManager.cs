using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool IsMenuOpen { get; private set; }
    public GameObject startMenu;
    public GameObject pauseMenu;

    private float toggleDelay;

    //public SimulationFlowManager SimulationFlowManager; Not ready yet.
    // Start is called before the first frame update
    void Start()
    {
        this.IsMenuOpen = true;
        this.startMenu.SetActive(true);
        this.pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleDelay > 0) 
        {
            toggleDelay -= 0.02F;
        }
    }

    public void OpenMenu()
    {
        if (toggleDelay <= 0.0F)
        {
            IsMenuOpen = true;
            this.pauseMenu.SetActive(true);
            toggleDelay = 1.0F;
            //this.SimulationFlowManager.StopTimer(); Not ready yet
        }
    }

    public void CloseMenu()
    {
        if (toggleDelay <= 0.0F)
        {
            IsMenuOpen = false;
            this.startMenu.SetActive(false);
            this.pauseMenu.SetActive(false);
            toggleDelay = 1.0F;
            //this.SimulationFlowManager.RestartTimer(); Not ready yet
        }
    }

    private void DisableMenuToggling()
    {
        
    }
}
