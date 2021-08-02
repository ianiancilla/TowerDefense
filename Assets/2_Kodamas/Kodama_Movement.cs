using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Kodama))]
public class Kodama_Movement : MonoBehaviour
{
    [Tooltip("Tiles per second")] [SerializeField] [Range(0f, 5f)] float speed = 1f;

    // member variables
    private List<Pathfinding_Node> path = new List<Pathfinding_Node>();
    private List<Pathfinding_Node> pathRemaining = new List<Pathfinding_Node>();

    // cache
    Pathfinding_GridManager gridManager;
    Pathfinding_Pathfinder pathfinder;


    private void Awake()
    {
        // cache
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();
    }

    // Update is called once per frame
    void Start()
    {
        // initialise
        RecalculatePath();
        StartCoroutine(FollowPath());
    }

    private void RecalculatePath()
    {
        // stop following old path
        StopAllCoroutines();

        // get new shortest path
        path.Clear();
        Vector2Int currentCoordinates = gridManager.GetGridCoordinatesFromWorldPos(
                                                                transform.position);
        path = pathfinder.GetPath_BreadthFirstSearch(currentCoordinates);

        // start on new path
        StartCoroutine(FollowPath());
    }
    private IEnumerator FollowPath()
    {
        pathRemaining = path;

        for (int i = 1; i < path.Count; i++)
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
        // send back to the pool
        gameObject.SetActive(false);
    }

}
