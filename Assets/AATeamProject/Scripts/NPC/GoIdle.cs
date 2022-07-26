using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GoIdle : BTAction
{
    private GameObject owner;
    private NavMeshAgent agent;
    private Animator animator;
    private float timer =0f;

    private Vector3 waypoint;
    private float waypointRadius = 0.3f;

    public GoIdle(GameObject owner)
    {
        this.owner = owner;
    }

    public override void Initialize()
    {
        waypoint = GameObject.Find("IdlePos").transform.position;
        animator = owner.GetComponent<Animator>();
        animator.Rebind();
        agent = owner.GetComponent<NavMeshAgent>();
    }

    public override void Terminate(){ }

    public override NodeState Update()
    {
        MoveAnim();

        agent.SetDestination(waypoint);
        float distance = (waypoint - owner.transform.position).magnitude;
        if (distance < waypointRadius)
        {
            timer += Time.deltaTime;
            animator.SetFloat("RemainingDistance", 0f);
            Idle.isIdle = true;
            if(timer > 5f)
            {
                timer = 0f;
                return NodeState.SUCCESS;
            }
            return NodeState.RUNNING;
        }

        return NodeState.RUNNING;

    }

    private void MoveAnim()
    {
        animator.SetFloat("LocalVelocityZ", 0.5f);
        animator.SetFloat("RemainingDistance", 1f);

    }
}
