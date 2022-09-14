using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GoPos : BTAction
{
    private GameObject owner;
    private NavMeshAgent agent;
    private Animator animator;
    private string pos;

    private Vector3 waypoint;
    private float waypointRadius = 0.3f;

    public GoPos(GameObject owner, string pos)
    {
        this.owner = owner;
        this.pos = pos;
    }

    public override void Initialize()
    {
        waypoint = GameObject.Find(pos).transform.position;
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
            Idle.isIdle = true;
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
