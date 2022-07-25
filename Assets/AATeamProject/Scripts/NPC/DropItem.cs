using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System;

public class DropItem : BTAction
{
    private GameObject owner;
    private GameObject item;
    private Rigidbody rb;

    private bool isDropped = false;
    public static bool isFinishedWork = false;

    private float detectRadius = 1f;


    public DropItem(GameObject owner)
    {
        this.owner = owner;
    }

    public override void Initialize()
    {
        item = FindNearestObjectByTag("Item");
        rb = item.GetComponent<Rigidbody>();
    }
    public override void Terminate() { }
    public override NodeState Update()
    {

        if (item == null)
        {
            return NodeState.FAILURE;
        }

        OnDropItem();

        if (!isDropped)
            return NodeState.FAILURE;
        else if(isDropped && !isFinishedWork)
        {
            return NodeState.RUNNING;
        }
        else if(isDropped && isFinishedWork)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;

    }

    private void OnDropItem()
    {
        rb.isKinematic = false;
        isDropped = true;
        isFinishedWork = true;
        item.transform.SetParent(null);

    }

    private GameObject FindNearestObjectByTag(string tag)
    {
        // 탐색할 오브젝트 목록을 List 로 저장합니다.
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objects)
        {
            float distance = Vector3.Distance(owner.transform.position, obj.transform.position);
            if (distance < detectRadius)
                return obj;
        }

        return null;
    }
}
