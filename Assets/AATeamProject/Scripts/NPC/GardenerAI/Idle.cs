using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class Idle : BTAction
{ 
    private GameObject owner;
    private Animator animator;

    public Idle(GameObject owner)
    {
        this.owner = owner;
    }

    public override void Initialize()
    {
        animator = owner.GetComponent<Animator>();
    }

    public override NodeState Update()
    {
        OnIdle();
        return NodeState.RUNNING;
    }

    public void OnIdle()
    {
        animator.SetFloat("TurnSpeed", 0f);
        animator.SetFloat("RemainingDistance", 0f);
        animator.SetFloat("LocalVelocityZ", 0f);
    }
}
