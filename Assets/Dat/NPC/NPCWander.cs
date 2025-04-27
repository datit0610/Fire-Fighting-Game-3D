using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum EState
{
    
    Waiting,
    run
}

public class NPCWander : NPCComponent
{
    
    public Area _area;
    private OpenCloseMainDoor _door;
    private Interactor _interactor;
   
   
    
    private void Start()
    {
        _interactor = FindObjectOfType<Interactor>();
        _door = FindObjectOfType<OpenCloseMainDoor>();
        
   
    }

    public void Update()
    {
        if (_interactor.currentState == EState.run)
        {
            SetPoint();
        }
        
    }

    
    
    bool HasArrived()
    {
        return npc.agent.remainingDistance <= npc.agent.stoppingDistance;
    }
    
    /*void SetRandomDestination()
    {
        Vector3 randomPoint = _area.GetRandomPoint();
        Debug.Log("Setting destination: " + randomPoint);
        npc.agent.SetDestination(randomPoint);
    }*/

    void SetPoint()
    {
        Vector3 point = _area.GetPointOnNavMesh();
        npc.agent.SetDestination(point);
    }
}
