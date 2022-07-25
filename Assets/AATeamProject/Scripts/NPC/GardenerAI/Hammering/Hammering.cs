using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class Hammering : BTAction
{
    private GameObject owner;
    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 waypoint;

    private bool isGooseQuacked;
    public static bool inHammer;
    private bool isFinished;
    private float timer = 0f;

    public Hammering(GameObject owner)
    {
        this.owner = owner;
    }

    public override void Initialize()
    {
        agent = owner.GetComponent<NavMeshAgent>();
        animator = owner.GetComponent<Animator>();
        isGooseQuacked = false;
        isFinished = false;
    }

    public override void Terminate()
    {
    }

    public override NodeState Update()
    {
        OnHammering();

        if(isFinished)
        {
            animator.SetBool("JobActive", false);
            animator.SetFloat("CurrentSpeed", 0);
            return NodeState.FAILURE;
        }
        return NodeState.RUNNING;
    }

    public void OnHammering()
    {
        inHammer = true;
        timer += Time.deltaTime;

        Debug.Log(timer);
        animator.SetBool("JobActive", true);
        animator.SetInteger("JobIndex", 4);
        animator.SetFloat("CurrentSpeed", timer);


        if(timer>5f && inHammer)
        {
            animator.SetBool("GooseQuacked", true);
            isGooseQuacked = true;
        }
        else if(timer>10f && !isGooseQuacked)
        {
            isFinished = true;
        }

    }
}
