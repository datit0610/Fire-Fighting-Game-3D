using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class NPC : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    
    [HideInInspector]
    public Animator animator;

    public float CurrentSpeed
    {
        get {return agent.velocity.magnitude;}
    }

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
}
