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
    private bool isFinishedWork = false;

    private float detectRadius = 0.5f;


    public DropItem(GameObject owner)
    {
        this.owner = owner;
    }

    public override void Initialize()
    {
        item = FindNearestObjectByTag("Item");
        //rb = item.GetComponent<Rigidbody>();
    }
    public override void Terminate() { }
    public override NodeState Update()
    {

        if (item == null)
        {
            Debug.Log(item);
            Debug.Log("!");
            return NodeState.FAILURE;
        }

        OnDropItem();


        if(isDropped && isFinishedWork)
        {
            Debug.Log("제발요");
            isFinishedWork = false;
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;

    }

    private void OnDropItem()
    {
        //rb.isKinematic = false;
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
