using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Area : MonoBehaviour
{
    public float Radius = 20f;
    public GameObject Target;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    public Vector3 GetRandomPoint()
    {
        Vector3 randomDirection = Target.transform.position * Radius;
        //Vector3 randomDirection = Random.insideUnitSphere * Radius; //Random vi tri
        randomDirection.y = 0f;
        
        Vector3 randomPoint = transform.position + randomDirection;
        
        NavMeshHit hit;
        Vector3 finalPosition = transform.position;

        if (NavMesh.SamplePosition(randomPoint, out hit, 5f, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public Vector3 GetPointOnNavMesh()
    {
        // Calculate direction TO the target
        Vector3 directionToTarget = (Target.transform.position - transform.position).normalized * Radius;
        directionToTarget.y = 0f;
    
        Vector3 targetPoint = transform.position + directionToTarget;
    
        NavMeshHit hit;
        Vector3 finalPosition = transform.position;
    
        if (NavMesh.SamplePosition(targetPoint, out hit, 5f, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
