using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunction : MonoBehaviour
{
    // properties
    [SerializeField] KeyCode pause_UnpauseButton;
    
    // variables
    bool isPaused = false;


    private void Update()
    {
        if (Input.GetKeyDown(pause_UnpauseButton))
        {
            GamePause();
        }
    }


    public void GamePause()
    {
        if (isPaused) { Time.timeScale = 1; }    // unpause if paused
        else { Time.timeScale = 0; }    // pause if running
        
        isPaused = !isPaused;
    }

}
