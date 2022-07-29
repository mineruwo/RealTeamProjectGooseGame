using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class DetectGooseSound : BTAction
{ 
    private GameObject owner;
    private Animator animator;
    private NavMeshAgent agent;
    private GameObject goose;
    private float timer = 0f;

    public DetectGooseSound(GameObject owner)
    {
        this.owner = owner;
    }

    public override void Initialize()
    {
        animator = owner.GetComponent<Animator>();
        agent = owner.GetComponent<NavMeshAgent>();
        goose = GameObject.FindGameObjectWithTag("Goose");
        animator.Rebind();  
    }

    public override NodeState Update()
    {
        if(!PlayerController.isHonk)
        {
            return NodeState.FAILURE;
        }
        else if(PlayerController.isHonk && !Hammering.inHammer)
        {
            OnAggro();
            timer += Time.deltaTime;

            if(timer>3f)
            {
                timer = 0f;
                agent.isStopped = false;
                PlayerController.isHonk = false;
                return NodeState.SUCCESS;
            }
        }
        return NodeState.RUNNING;
    }

    public void OnAggro()
    {
        owner.transform.LookAt(goose.transform.position);
        agent.isStopped = true;
        animator.SetBool("ReactionActive", true);
        animator.SetInteger("ReactionIndex", 0);
    }
}
