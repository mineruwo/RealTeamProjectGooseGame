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
        //���⼭ bool ���� �޾Ƽ� true false �Ѱ��ְ� �ظӷ� �� �� �ְ� ó��
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
