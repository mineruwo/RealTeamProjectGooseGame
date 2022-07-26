using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System;
using UnityEngine.AI;

public class DetectGoosePos : BTConditions
{
    private GameObject owner;
    private GameObject goose;
    private GameObject item;
    
    private float gooseRadius = 0.5f;
    private static bool isStolen = false;
    
    public DetectGoosePos(GameObject owner)
    {
        this.owner = owner;
    }
    public override void Initialize()
    {
        goose = GameObject.FindGameObjectWithTag("Goose");
        item = DetectStealer("Item");

    }

    public override NodeState Update()
    {
        if (!isStolen)
        {
            return NodeState.FAILURE;
        }
        else
        {
            Debug.Log("see");
            return NodeState.SUCCESS;
        }
    }

    private GameObject DetectStealer(string tag)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach(var obj in objects)
        {
            float distance = Vector3.Distance(goose.transform.position, obj.transform.position);
            if(distance < gooseRadius)
            {
                isStolen = true;
                return obj;
            }

        }

        return null;
    }
}
