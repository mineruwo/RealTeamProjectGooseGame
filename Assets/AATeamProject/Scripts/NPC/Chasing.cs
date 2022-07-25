using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class Chasing : BTAction
{
    private GameObject owner;
    private Animator animator;

    public Chasing(GameObject owner)
    {
        this.owner = owner; 
    }

    public override void Initialize()
    {
        animator = owner.GetComponent<Animator>();
    }

    public override NodeState Update()
    {
        OnChase();
        return NodeState.RUNNING;
    }

    public void OnChase()
    {
        animator.SetFloat("LocalVelocityZ", 1f);
        animator.SetFloat("RemainingDistance", 1f);
    }

}
