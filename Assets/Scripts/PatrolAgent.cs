using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour
{
    [SerializeField]
    private Transform[] _points;

    [SerializeField]
    private float _minRemainingDistance = 5f;
    private int _destinationPoint = 0;
    private NavMeshAgent _agent;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = true;
        GoToNextPoint();
    }

    void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < _minRemainingDistance)
        {
            GoToNextPoint();
        }
        //Debug.Log(agent.remainingDistance);
    }
    
    public void Pause(bool pause)
    {
        _agent.isStopped = pause;
    }

    private void GoToNextPoint()
    {
        if(_points.Length == 0)
        {
            Debug.LogError("no patrol points set to use in the patrolling script");
            enabled = false;
            return;
        }
        Vector3 newDestination = _points[_destinationPoint].position;
        newDestination.y = transform.position.y;
        _agent.destination = newDestination;
        _destinationPoint = (_destinationPoint + 1) % _points.Length;
    }

   
}
