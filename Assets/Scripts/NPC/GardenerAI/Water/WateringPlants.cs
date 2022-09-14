using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WateringPlants : BTAction
{
    private GameObject owner;
    private Animator animator;
    private GameObject item;
    private float timer;

    private bool isFinished = false;
    public WateringPlants(GameObject owner)
    {
        this.owner = owner;
    }
    public override void Initialize()
    {
        isFinished = false;
        animator = owner.GetComponent<Animator>();
        animator.Rebind();
        item = GameObject.FindGameObjectWithTag("Item");
        timer = 0f;
    }
    public override void Terminate() { }

    public override NodeState Update()
    {
        OnWaterPlants();
        if (isFinished)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }

    private void OnWaterPlants()
    {
        animator.SetBool("JobActive", true);
        animator.SetInteger("JobIndex", 0);

        timer += Time.deltaTime;
        if(timer>10f)
        {
            timer = 0f;
            isFinished = true;
            animator.SetFloat("RemainingDistance", 0f);
        }

    }
}
