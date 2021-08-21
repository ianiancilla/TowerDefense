using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

[RequireComponent(typeof(Kodama_Movement))]

public class Kodama : MonoBehaviour
{
    [SerializeField] MMFeedback arrivingFeedback;
    [SerializeField] MMFeedback deathFeedback;

    // vaiables
    private const string ANIMATOR_ARRIVED_TRIGGER = "Arrived";
    private const string ANIMATOR_DYING_TRIGGER = "Dying";


    // cache 
    Pathfinding_GridManager gridManager;
    Animator myAnimator;

    private void Start()
    {
        // cache
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    public Vector2Int GetCurrentGridTile()
    {
        Vector3 worldPos = transform.position;

        return gridManager.GetGridCoordinatesFromWorldPos(worldPos);
    }


    public void Die()
    {
        StartCoroutine(DeathSequence());
    }
    private IEnumerator DeathSequence()
    {
        deathFeedback ?.Play(this.transform.position);

        myAnimator.SetTrigger(ANIMATOR_DYING_TRIGGER);

        yield return new WaitForSecondsRealtime(.5f);
        
        // send back to the pool
        gameObject.SetActive(false);

        FindObjectOfType<GameManager>().GameOver();
    }

    public IEnumerator ReachEndOfPath()
    {
        FindObjectOfType<GameManager>().ScoreKodama();

        arrivingFeedback?.Play(this.transform.position);

        myAnimator.SetTrigger(ANIMATOR_ARRIVED_TRIGGER);

        yield return new WaitForSecondsRealtime(.5f);
        // send back to the pool
        gameObject.SetActive(false);
    }

}
