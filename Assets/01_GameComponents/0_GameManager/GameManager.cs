using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PauseAndTutorial))]
public class GameManager : MonoBehaviour
{
    // properties
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject youWonMenu;
    [SerializeField] float levelEndDelay = 1f;

    // variables
    int totalKodama;
    int savedKodama;

    // Start is called before the first frame update
    void Awake()
    {
        totalKodama = FindObjectsOfType<Kodama>().Length;

        gameOverMenu.SetActive(false);
        youWonMenu.SetActive(false);

    }

    public void GameOver()
    {
        StartCoroutine(PlayGameOverSequence());
    }

    IEnumerator PlayGameOverSequence()
    {
        yield return new WaitForSecondsRealtime(levelEndDelay);

        // pause game
        GetComponent<PauseAndTutorial>().GamePause();
        gameOverMenu.SetActive(true);
    }

    public void ScoreKodama()
    {
        savedKodama++;
        if (savedKodama >= totalKodama)
        {
            StartCoroutine( WinGame());
        }
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSecondsRealtime(levelEndDelay);
        youWonMenu.SetActive(true);
    }

}
