using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class Chasing : BTAction
{
    private GameObject owner;
    private Animator animator;
    private NavMeshAgent agent;
    private GameObject goose;

    public Chasing(GameObject owner)
    {
        this.owner = owner; 
    }

    public override void Initialize()
    {
        goose = GameObject.FindGameObjectWithTag("Goose");
        animator = owner.GetComponent<Animator>();
        agent = owner.GetComponent<NavMeshAgent>();
    }

    public override NodeState Update()
    {
        OnChase();
        if(!GooseGrab.isGrap)
        {
            Debug.Log("right");
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;
    }

    public void OnChase()
    {
        agent.SetDestination(goose.transform.position);
        animator.SetFloat("LocalVelocityZ", 1f);
        animator.SetFloat("RemainingDistance", 1f);
    }


}
