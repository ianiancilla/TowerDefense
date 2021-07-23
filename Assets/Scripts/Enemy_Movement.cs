using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] List<Tile_Waypoint> path;
    [Tooltip("Tiles per second")] [SerializeField] [Range(0f, 5f)] float speed = 1f;


    // Update is called once per frame
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (Tile_Waypoint waypoint in path)
        {
            Vector3 startingPosition = transform.position;
            Vector3 targetPosition = waypoint.transform.position;

            float lerpPhase = 0f;

            while (lerpPhase < 1)
            {
                lerpPhase += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(startingPosition, targetPosition, lerpPhase);
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
