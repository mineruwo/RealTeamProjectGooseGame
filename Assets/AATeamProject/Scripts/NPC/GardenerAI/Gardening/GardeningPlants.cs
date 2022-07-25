using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class GardeningPlants : BTAction
{
    private GameObject owner;
    private Animator animator;
    private GameObject item;
    private float timer;

    private bool isFinished = false;
    public GardeningPlants(GameObject owner)
    {
        this.owner = owner;
    }
    public override void Initialize()
    {
        animator = owner.GetComponent<Animator>();
        item = GameObject.FindGameObjectWithTag("Item");
        timer = 0f;
    }
    public override void Terminate() { }

    public override NodeState Update()
    {
        OnGardenPlants();
        if (isFinished)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }

    private void OnGardenPlants()
    {
        animator.SetBool("JobActive", true);
        animator.SetInteger("JobIndex", 1);

        timer += Time.deltaTime;
        if (timer > 11f)
        {
            isFinished = true;
        }

    }
}
