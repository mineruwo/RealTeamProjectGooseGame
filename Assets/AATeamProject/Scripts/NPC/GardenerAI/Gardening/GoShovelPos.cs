using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GoShovelPos : BTAction
{
    private GameObject owner;
    private NavMeshAgent agent;
    private Animator animator;

    private Vector3 waypoint;
    private float waypointRadius = 0.3f;

    public GoShovelPos(GameObject owner)
    {
        this.owner = owner;
    }

    public override void Initialize()
    {
        waypoint = GameObject.Find("GardeningPos2").transform.position;
        animator = owner.GetComponent<Animator>();
        animator.Rebind();
        agent = owner.GetComponent<NavMeshAgent>();
    }

    public override void Terminate(){ }

    public override NodeState Update()
    {
        MoveAnim();
        agent.SetDestination(waypoint);
        float distance = Vector3.Distance(waypoint, owner.transform.position);
        if (distance < waypointRadius)
        {
            animator.SetFloat("RemainingDistance", 0f);
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;

    }

    private void MoveAnim()
    {
        animator.SetFloat("LocalVelocityZ", 0.5f);
        animator.SetFloat("RemainingDistance", 1f);

    }
}
