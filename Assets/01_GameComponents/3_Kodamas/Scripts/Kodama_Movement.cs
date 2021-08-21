using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Kodama))]
public class Kodama_Movement : MonoBehaviour
{
    [Tooltip("Tiles per second")] [SerializeField] [Range(0f, 5f)] float speed = 1f;

    // member variables
    private List<Pathfinding_Node> path = new List<Pathfinding_Node>();
    private List<Pathfinding_Node> pathRemaining = new List<Pathfinding_Node>();
    public List<Pathfinding_Node> PathRemaining { get { return pathRemaining; } }

    float hazardCheckPoint = 0.5f;    // at what point between the centre of two
                                      // tiles will the hazard checke be done.
                                      // 0 is when startig from centre of first tile,
                                      // 1 at centre of target tile, 0.5 exactly when
                                      // stepping on second tile

    // cache
    Pathfinding_GridManager gridManager;
    Pathfinding_Pathfinder pathfinder;
    Kodama myKodama;


    private void Awake()
    {
        // cache
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        pathfinder = FindObjectOfType<Pathfinding_Pathfinder>();
        myKodama = GetComponent<Kodama>();
    }

    // Update is called once per frame
    void Start()
    {
        // initialise
        RecalculatePath();
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
        pathRemaining = path.ToList();

        // in case Kodama is blocked (by pre-existing blocks for example), stop until a new path is calculated
        if (pathRemaining.Count == 0) { yield break; }

        pathRemaining.RemoveAt(0);    // removes starting position as it is already reached by default

        // for each step in the path, except for the last one,
        // so they can play arriving event while not clipping inside the Shrine
        for (int i = 1; i < path.Count - 1; i++)
        {
            Vector3 startingPosition = transform.position;
            Vector3 targetPosition = gridManager.GetWorldPosFromGridCoordinates(path[i].coordinates);

            transform.LookAt(targetPosition);

            float lerpPhase = 0f;

            while (lerpPhase < 1)
            {
                lerpPhase += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(startingPosition, targetPosition, lerpPhase);

                if (lerpPhase > hazardCheckPoint)
                {
                    CheckForHazard(path[i]);
                }

                yield return new WaitForEndOfFrame();
            }

            pathRemaining.RemoveAt(0);    // removes node once it is reached

        }

        // look towards shrine before playing arrival
        Vector3 shrinePos = gridManager.GetWorldPosFromGridCoordinates(path[path.Count - 1].coordinates);
        transform.LookAt(shrinePos);

        StartCoroutine(myKodama.ReachEndOfPath());
    }

    private void CheckForHazard(Pathfinding_Node nextNode)
    {
        // check if death due to hazard
        if (nextNode.IsHazard)
        {
            myKodama.Die();
            Debug.Log(this.gameObject.name + " died at " + nextNode.coordinates + nextNode.IsHazard.ToString());
        }
    }

}
