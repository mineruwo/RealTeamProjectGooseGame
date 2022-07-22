using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class FindItem : BTAction
{
    private GameObject owner;
    public FindItem(GameObject owner)
    {
        this.owner = owner;
    }
    public override void Initialize() { }
    public override void Terminate() { }
    public override NodeState Update()
    {
        OnLookAt();
        return NodeState.RUNNING;
    }

    private void OnLookAt()
    {
        GameObject item = GameObject.FindGameObjectWithTag("Tag");
        if(item)
        {
            Vector3 dir = item.transform.position - owner.transform.position;
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
            Debug.Log("got");
        }
    }
}
