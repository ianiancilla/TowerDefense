using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Enemy_Movement : MonoBehaviour
{
    [Tooltip("Tiles per second")] [SerializeField] [Range(0f, 5f)] float speed = 1f;

    // member variables
    private List<Pathfinding_Node> path = new List<Pathfinding_Node>();

    // cache
    Enemy myEnemy;
    Pathfinding_GridManager gridManager;
    Pathfinder pathfinder;


    private void Awake()
    {
        // cache
        myEnemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    // Update is called once per frame
    void OnEnable()
    {
        // initialise
        RecalculatePath();
        ResetMovementOnNewPath();
        StartCoroutine(FollowPath());
    }

    private void RecalculatePath()
    {
        path.Clear();
        path = pathfinder.GetPath_BreadthFirstSearch();
    }
    private IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 startingPosition = transform.position;
            Vector3 targetPosition = gridManager.GetWorldPosFromGridCoordinates(path[i].coordinates);

            transform.LookAt(targetPosition);

            float lerpPhase = 0f;

            while (lerpPhase < 1)
            {
                lerpPhase += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(startingPosition, targetPosition, lerpPhase);
                yield return new WaitForEndOfFrame();
            }
        }
        ReachEndOfPath();
    }

    private void ReachEndOfPath()
    {
        myEnemy.StealCurrency();

        // send back to the pool
        gameObject.SetActive(false);

    }

    private void ResetMovementOnNewPath()
    {
        transform.position = gridManager.GetWorldPosFromGridCoordinates(path[0].coordinates);
    }
}
