using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System;
using UnityEngine.AI;

public class DetectGoosePos : BTAction
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

    }

    public override NodeState Update()
    {
        item = DetectStealer();

        if (item != null)
        {
            if (!isStolen)
            {
                Debug.Log("µµµ¿³ð");
                return NodeState.FAILURE;
            }
            else
            {
                return NodeState.SUCCESS;
            }
        }
        return NodeState.RUNNING;

    }

    private GameObject DetectStealer()
    {
        var gooseItem = goose.GetComponentInChildren<GooseGrab>().grabObject;

        if (gooseItem != null)
        {
            Debug.Log("detected item is " + gooseItem.name);
            isStolen = true;
            return gooseItem;
        }

        return null;
    }
}
