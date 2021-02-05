using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private float minRemainingDistance = 5f;
    private int destinationPoint = 0;
    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        GoToNextPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < minRemainingDistance)
        {
            GoToNextPoint();
        }
        //Debug.Log(agent.remainingDistance);
    }
    
      void GoToNextPoint()
    {
        if(points.Length == 0)
        {
            Debug.LogError("no patrol points set to use in the patrolling script");
            enabled = false;
            return;
        }
        agent.destination = points[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % points.Length;
    }

   
}
