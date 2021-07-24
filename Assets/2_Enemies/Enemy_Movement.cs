using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [Tooltip("Tiles per second")] [SerializeField] [Range(0f, 5f)] float speed = 1f;

    // member variables
    private List<Tile_Waypoint> path = new List<Tile_Waypoint>();

    // Update is called once per frame
    void OnEnable()
    {
        // initialise
        FindPath();
        PlaceAtStartOfPath();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();

        GameObject pathParent = GameObject.FindGameObjectWithTag("EnemyPath");
        
        foreach (Transform child in pathParent.transform)
        {
            path.Add(child.GetComponent<Tile_Waypoint>());
        }
    }
    private IEnumerator FollowPath()
    {
        foreach (Tile_Waypoint waypoint in path)
        {
            Vector3 startingPosition = transform.position;
            Vector3 targetPosition = waypoint.transform.position;

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

    private void PlaceAtStartOfPath()
    {
        transform.position = path[0].transform.position;
    }
}
