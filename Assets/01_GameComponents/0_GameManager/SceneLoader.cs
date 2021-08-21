using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] Animator transitionAnimator;

    private const string TRANSITION_TRIGGER_NAME = "FadeToBlack";



    IEnumerator TransitionToScene(int sceneNumber)
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger(TRANSITION_TRIGGER_NAME);
        }

        yield return new WaitForSecondsRealtime(loadDelay);

        // unpause the game if it is paused
        Time.timeScale = 1;

        SceneManager.LoadScene(sceneNumber);
    }

    // overload for string
    IEnumerator TransitionToScene(string sceneName)
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger(TRANSITION_TRIGGER_NAME);
        }

        yield return new WaitForSecondsRealtime(loadDelay);

        // unpause the game if it is paused
        Time.timeScale = 1;

        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        StartCoroutine(TransitionToScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextScene()
    {
        int thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (SceneManager.sceneCountInBuildSettings > thisSceneIndex + 1)
        {
            StartCoroutine(TransitionToScene(thisSceneIndex + 1));
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        if (Application.CanStreamedLevelBeLoaded("MainMenu"))
        {
            StartCoroutine(TransitionToScene("MainMenu"));
        }
        else
        {
            StartCoroutine(TransitionToScene(0));
        }
    }

    public void LoadCredits()
    {
        if (Application.CanStreamedLevelBeLoaded("Credits"))
        {
            StartCoroutine(TransitionToScene("Credits"));
        }
        else
        {
            StartCoroutine(TransitionToScene(0));
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

}
