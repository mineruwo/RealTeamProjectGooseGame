using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class ShooDuck : BTAction
{
    private GameObject owner;
    private GameObject goose;
    private Animator animator;

    public ShooDuck(GameObject owner)
    {
        this.owner = owner; 
    }

    public override void Initialize()
    {
        goose = GameObject.FindGameObjectWithTag("Goose");
        animator = owner.GetComponent<Animator>();
    }

    public override NodeState Update()
    {
        float distance = Vector3.Distance(owner.transform.position, goose.transform.position);

        if (distance > 0.5f)
            return NodeState.FAILURE;
        else if (distance < 0.5f)
        {
            if (!GrabItem.isGrabbed)
            {
                animator.SetBool("ReactionActive", true);
                animator.SetInteger("ReactionIndex", 0);
                return NodeState.FAILURE;
            }

            else
            {
                return NodeState.FAILURE;

            }

        }
        return NodeState.RUNNING;
    }
}
