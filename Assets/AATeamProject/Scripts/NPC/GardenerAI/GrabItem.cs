using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System;

public class GrabItem : BTAction
{
    private GameObject owner;
    private GameObject item;
    private GameObject handle;
    private Rigidbody rb;
    private bool isGrabbed = false;
    public static Transform transform;

    private float detectRadius = 1f;

    public GrabItem(GameObject owner)
    {
        this.owner = owner;
    }
    public override void Initialize()
    {
        item = FindNearestObjectByTag("Item");
        rb = item.GetComponent<Rigidbody>();
        transform = item.transform;
    }
    public override void Terminate() { }

    public override NodeState Update()
    {
        if (item == null)
        {
            Debug.Log("실패");
            return NodeState.FAILURE;
        }
        else
        {
            Debug.Log(item.gameObject.name);
        }

        OnFindItem();
        if (isGrabbed)
            return NodeState.SUCCESS;
        return NodeState.RUNNING;
    }

    private void OnFindItem()
    {
        float distance = Vector3.Distance(item.transform.position, owner.transform.position);
        Debug.Log(distance);
        if (distance < detectRadius)
        {
            rb.isKinematic = true;
            item.transform.SetParent(GameObject.Find("leftGrasper").transform);
            isGrabbed = true;
        }
    }
    private GameObject FindNearestObjectByTag(string tag)
    {
        // 탐색할 오브젝트 목록을 List 로 저장합니다.
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach(var obj in objects)
        {
            float distance = Vector3.Distance(owner.transform.position, obj.transform.position);
            if(distance < detectRadius)
                return obj;
        }

        return null;
    }

    //private GameObject FindHandle(string handle)
    //{
    //    var objects = GameObject.Find(handle);
    //    return objects;
    //}
}
