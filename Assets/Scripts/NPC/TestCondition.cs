using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TestCondition : BTConditions
{
    private GameObject owner;

    public TestCondition(GameObject owner)
    {
        this.owner = owner;
    }

    public override NodeState Update()
    {
        //여기서 bool 변수 받아서 true false 넘겨주고 해머로 들어갈 수 있게 처리
        if(!GameManager.instance.questMgr.testbool)
        {
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.SUCCESS;
        }

    }
}
