using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_EnemyTracking : MonoBehaviour
{
    [SerializeField] Transform trackingPart;
    [SerializeField] bool canTilt = false;

    [SerializeField] ParticleSystem projectiles;
    [SerializeField] float range = 15f;

    // member variables
    GameObject currentTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        FindClosestTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // if there is no target or the current one is out of range, try finding a new one
        if (!currentTarget || !IsTargetViable(currentTarget))
        {
            FindClosestTarget();
        }

        if (currentTarget)
        {
            AimForTarget();
            Attacking(IsTargetViable(currentTarget));
        }
        else { Attacking(false); }

    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = DistanceOnXZPlane(this.transform.position, enemy.transform.position);
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy.gameObject;
    }

    private void AimForTarget()
    {
        // aim the direction
        Vector3 targetForLook = currentTarget.transform.position;

        if (!canTilt)
        {
            targetForLook = new Vector3(targetForLook.x,
                                        trackingPart.transform.position.y,
                                        targetForLook.z);
        }

        trackingPart.LookAt(targetForLook);
    }

    private bool IsTargetViable(GameObject target)
    {
        if (!target || !target.activeInHierarchy)
        {
            return false;
        }

        // check if in range
        float targetDistance = DistanceOnXZPlane(this.transform.position, target.transform.position);
        
        if (targetDistance > range) { return false; }

        return true;
    }

    private void Attacking(bool isActive)
    {
        var emissionModule = projectiles.emission;
        emissionModule.enabled = isActive;
    }

    private float DistanceOnXZPlane(Vector3 a, Vector3 b)
    {
        return Vector2.Distance(new Vector2(a.x, a.z),
                                new Vector2(b.x, b.z));

    }
}
