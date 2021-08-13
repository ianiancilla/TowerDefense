using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_LineVisualisation : MonoBehaviour
{
    [SerializeField] float pathYPos = 6f;

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
        // find nr of vertexes needed
        int vertexNr = myMovement.PathRemaining.Count + 1;    //+1 because we need current position, too.

        Vector3[] lineVertexes = new Vector3[vertexNr];    
        lr.positionCount = lineVertexes.Length;

        // set starting point
        Vector3 kodamaPos = new Vector3(transform.parent.position.x,
                                        pathYPos,
                                        transform.parent.position.z);

        lineVertexes[0] = kodamaPos;

        for (int i = 1; i < lineVertexes.Length; i++)
        {
            // -1 is because PathRemaining starts at the next node, while the first vertex is at current object position,
            // so lineVertexes[0] is current pos, and from 1 on it is the remaining path steps.
            Vector3 pathStep = gridManager.GetWorldPosFromGridCoordinates(myMovement.PathRemaining[i-1].coordinates);

            lineVertexes[i] = new Vector3(pathStep.x,
                                          pathYPos,
                                          pathStep.z);

        }

        lr.SetPositions(lineVertexes);
    }

}