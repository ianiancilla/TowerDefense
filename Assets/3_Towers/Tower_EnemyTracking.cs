using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_EnemyTracking : MonoBehaviour
{
    [SerializeField] Transform trackingPart;
    [SerializeField] bool canTilt = false;

    // member variables
    GameObject currentTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget)
        {
            AimForTarget();
        }
    }

    private void FindTarget()
    {
        currentTarget = FindObjectOfType<Enemy_Movement>().gameObject;
    }

    private void AimForTarget()
    {
        Vector3 targetForLook = currentTarget.transform.position;

        if (!canTilt)
        {
            targetForLook = new Vector3(targetForLook.x,
                                        trackingPart.transform.position.y,
                                        targetForLook.z);
        }

        trackingPart.LookAt(targetForLook);
    }

}
