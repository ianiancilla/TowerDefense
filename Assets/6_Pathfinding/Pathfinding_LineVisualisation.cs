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
        Vector3[] lineVertexes = new Vector3[myMovement.Path.Count];

        for (int i = 0; i < myMovement.Path.Count; i++)
        {
            lr.positionCount = myMovement.Path.Count;

            Vector3 pathStep = gridManager.GetWorldPosFromGridCoordinates(myMovement.Path[i].coordinates);

            lineVertexes[i] = new Vector3(pathStep.x,
                                          pathY,
                                          pathStep.z);

        }

        lr.SetPositions(lineVertexes);
    }

}
