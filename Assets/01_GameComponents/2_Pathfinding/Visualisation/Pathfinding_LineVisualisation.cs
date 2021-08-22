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

        // set random color
        lr.colorGradient = FindRandomPathColor();
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

    private Gradient FindRandomPathColor()
    {
        Gradient gradient = new Gradient();

        Color randomColor = Random.ColorHSV(0f, 1f,    // hue
                                            0.8f, 1f,  // saturation
                                            0.6f, 1f); // value

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].color = randomColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = randomColor;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[4];
        alphaKey[0].alpha = 0.0f;
        alphaKey[0].time = 0f;

        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.05f;
        
        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 0.95f;
        
        alphaKey[3].alpha = 0.0f;
        alphaKey[3].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        return gradient;
    }
}
