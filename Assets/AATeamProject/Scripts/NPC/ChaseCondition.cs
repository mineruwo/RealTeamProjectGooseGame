using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class ChaseCondition : BTConditions
{
    private GameObject owner;

    public ChaseCondition(GameObject owner)
    {
        this.owner = owner;
    }

    public override NodeState Update()
    {
        if(!DetectingCondition.gotYou)
        {
            return NodeState.FAILURE;
        }
        else
        {
            Debug.Log("컨디션통과");
            return NodeState.SUCCESS;
        }
    }
}
