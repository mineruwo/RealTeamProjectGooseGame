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
        else if(DetectingCondition.gotYou && GooseGrab.isGrap)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
