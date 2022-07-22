using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

public class Move : BTAction
{
    private GameObject owner;
    private Animator animator;
    private NavMeshAgent agent;

    private List<Vector3> listWaypoint = new List<Vector3>();
    private int currWaypoint = 0;

    public Move(GameObject owner)
    {
        this.owner = owner;
    }
    public override void Initialize()
    {
        AddWaypoint();
        animator = owner.GetComponent<Animator>();  
        agent = owner.GetComponent<NavMeshAgent>();
    }
    public override void Terminate() { }

    public override NodeState Update()
    {
        OnMove();
        return NodeState.RUNNING;
    }

    private void AddWaypoint()
    {
        listWaypoint.Add(GameObject.Find("WateringPos1").transform.position);
        listWaypoint.Add(GameObject.Find("WateringPos2").transform.position);
    }

    private void OnMove()
    {
        Vector3 waypoint = listWaypoint[currWaypoint];
        float distance = Vector3.Distance(waypoint, owner.transform.position);
        if(distance<0.1f)
        {
            animator.SetFloat("RemainingDistance", 0f);
        }
        animator.SetFloat("LocalVelocityZ", 0.5f);
        animator.SetFloat("RemainingDistance", 1f);

        //ÀÌµ¿
        agent.SetDestination(waypoint);
    }
}
