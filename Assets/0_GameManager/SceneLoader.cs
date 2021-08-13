using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        int thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (SceneManager.sceneCountInBuildSettings > thisSceneIndex + 1)
        {
            SceneManager.LoadScene(thisSceneIndex + 1);
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
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void LoadCredits()
    {
        if (Application.CanStreamedLevelBeLoaded("Credits"))
        {
            SceneManager.LoadScene("Credits");
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }


    public void QuitApp()
    {
        Application.Quit();
    }
}
