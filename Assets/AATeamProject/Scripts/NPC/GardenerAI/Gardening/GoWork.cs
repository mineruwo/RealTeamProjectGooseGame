using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class GoWork : BTAction
{
    private GameObject owner;
    private string pos;

    private GameObject workspot;
    private Vector3 waypoint;
    private float waypointRadius = 0.3f;
    private NavMeshAgent agent;
    private Animator animator;
    private float radius = 1f;

    public GoWork(GameObject owner, string pos)
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
    public override void Terminate()
    {

    }
    public override NodeState Update()
    {
        MoveAnim();
        agent.SetDestination(waypoint);
        float distance = Vector3.Distance(waypoint, owner.transform.position);
        if (distance < waypointRadius)
        {
            workspot = FindNearestWorkSpotByTag("WorkSpot");
            animator.SetFloat("RemainingDistance", 0f);
            owner.transform.LookAt(workspot.transform.position);
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;

    }

    private void MoveAnim()
    {
        animator.SetFloat("LocalVelocityZ", 0.5f);
        animator.SetFloat("RemainingDistance", 1f);
    }

    private GameObject FindNearestWorkSpotByTag(string tag)
    {
        // 탐색할 오브젝트 목록을 List 로 저장합니다.
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objects)
        {
            float distance = Vector3.Distance(owner.transform.position, obj.transform.position);
            if (distance < radius)
                return obj;
        }

        return null;
    }
}
