using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PauseFunction))]
public class GameManager : MonoBehaviour
{
    // properties
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject youWonMenu;

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
        // pause game
        GetComponent<PauseFunction>().GamePause();
        gameOverMenu.SetActive(true);
    }

    public void ScoreKodama()
    {
        savedKodama++;
        if (savedKodama >= totalKodama)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        youWonMenu.SetActive(true);
    }

}
