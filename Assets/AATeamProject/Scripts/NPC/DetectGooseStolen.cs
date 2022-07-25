using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System;
using UnityEngine.AI;

public class DetectGooseStolen : BTConditions
{
    private GameObject owner;
    private GameObject goose;
    private GameObject item;

    private float detectRadius = 8f;
    private float gooseRadius = 0.5f;
    private static bool isStolen;
    private NavMeshAgent agent;
    
    public DetectGooseStolen(GameObject owner)
    {
        this.owner = owner;
    }
    public override void Initialize()
    {
        goose = GameObject.FindGameObjectWithTag("Goose");
        agent = owner.GetComponent<NavMeshAgent>();
        item = DetectStealer("Beakable");
        isStolen = false;
    }

    public override NodeState Update()
    {
        if (isStolen)
        {
            return NodeState.FAILURE;
        }
        else
        {
            Debug.Log("¿‚¿Ω");
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;
    }

    private void OnChasing()
    {
        //agent.SetDestination(item.transform.position);
    }

    private GameObject DetectStealer(string tag)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach(var obj in objects)
        {
            float distance = Vector3.Distance(goose.transform.position, obj.transform.position);
            if(distance < gooseRadius)
                return obj; 
        }

        return null;
    }
}
