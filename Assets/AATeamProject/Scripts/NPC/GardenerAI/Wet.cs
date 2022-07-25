using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
using System;

public class Wet : BTAction
{
    private GameObject owner;
    private Animator animator;

    public Wet(GameObject owner)
    {
        this.owner = owner;
    }

    public override void Initialize()
    {
        animator = owner.GetComponent<Animator>();
    }

    public override NodeState Update()
    {
        if (DetectGoose.isWetted)
        {
            OnWet();
            Debug.Log("d");
        }
        else
        {
            return NodeState.FAILURE;
        }
        return base.Update();
    }

    private void OnWet()
    {
        owner.GetComponent<NavMeshAgent>().enabled = false;
        animator.SetBool("ReactionActive", true);
        animator.SetInteger("ReactionIndex", 1);
        GameManager.instance.questMgr.GetQuestId(2);
    }



}
