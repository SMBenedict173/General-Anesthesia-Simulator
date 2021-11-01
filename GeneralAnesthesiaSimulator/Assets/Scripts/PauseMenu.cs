using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {

        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;

        // free time / timer here
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;

    }
}
