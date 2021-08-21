using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndTutorial : MonoBehaviour
{
    // properties
    [SerializeField] KeyCode pause_UnpauseButton;
    [SerializeField] KeyCode tutorial_toggleButton;
    [SerializeField] bool tutorialActiveAtStart;

    // variables
    bool isPaused = false;
    bool isTutorialActive = false;

    // cache
    CanvasGroup tutorialCanvas;

    private void Awake()
    {
        // cache
        tutorialCanvas = GameObject.FindGameObjectWithTag("TutorialCanvas").GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (tutorialActiveAtStart)
        {
            ActivateTutorial();
            isTutorialActive = true;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(pause_UnpauseButton))
        {
            GamePause();
        }

        if (Input.GetKeyDown(tutorial_toggleButton))
        {
            ToggleTutorial();
        }
    }


    public void GamePause()
    {
        if (isPaused) { Time.timeScale = 1; }    // unpause if paused
        else { Time.timeScale = 0; }    // pause if running
        
        isPaused = !isPaused;
    }

    public void ToggleTutorial()
    {
        if (isTutorialActive) { tutorialCanvas.alpha = 0; }    // hide if active
        else
        {
            ActivateTutorial();
        }

        isTutorialActive = !isTutorialActive;
    }

    private void ActivateTutorial()
    {
        tutorialCanvas.alpha = 1;  // show if unactive
        Time.timeScale = 0;   // pause game while tutorial is active
        isPaused = true;
    }
}
