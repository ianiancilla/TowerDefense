using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_LineVisualisation : MonoBehaviour
{
    [SerializeField] float pathY = 6f;

    // cache
    Pathfinding_GridManager gridManager;
    Kodama_Movement myMovement;
    LineRenderer lr;

    private void Awake()
    {
        // cache
        gridManager = FindObjectOfType<Pathfinding_GridManager>();
        myMovement = transform.parent.GetComponent<Kodama_Movement>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawPath();
    }

    private void DrawPath()
    {
        int stepsTillShrine = myMovement.pathQueue.Count;

        Vector3[] lineVertexes = new Vector3[stepsTillShrine + 1];    //+1 because we need current position, too.
        lr.positionCount = lineVertexes.Length;

        lineVertexes[0] = new Vector3(transform.parent.parent.position.x,
                                      pathY,
                                      transform.parent.parent.position.z);

        for (int i = 1; i < lineVertexes.Length; i++)
        {

            Vector3 pathStep = gridManager.GetWorldPosFromGridCoordinates(myMovement.pathQueue[i].coordinates);

            lineVertexes[i] = new Vector3(pathStep.x,
                                          pathY,
                                          pathStep.z);

        }

        lr.SetPositions(lineVertexes);
    }

}
