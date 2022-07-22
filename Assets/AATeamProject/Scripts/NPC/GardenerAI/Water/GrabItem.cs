using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using System;

public class GrabItem : BTAction
{
    private GameObject owner;
    private GameObject item;
    private Rigidbody rb;
    private bool isGrabbed = false;

    private float detectRadius = 1f;
    private Animator animator;

    public GrabItem(GameObject owner)
    {
        this.owner = owner;
    }
    public override void Initialize()
    {
        item = GameObject.FindGameObjectWithTag("Item");
        rb = item.GetComponent<Rigidbody>();
        animator = owner.GetComponent<Animator>();
    }
    public override void Terminate() { }

    public override NodeState Update()
    {
        OnFindItem();
        if (isGrabbed)
            return NodeState.SUCCESS;
        return NodeState.RUNNING;
    }

    private void OnFindItem()
    {
        if(item == null)
        {
            
        }
        else
        {
            rb.isKinematic = true;
            item.transform.SetParent(GameObject.Find("rightGrasper").transform);
            isGrabbed = true;
        }
    }
}
