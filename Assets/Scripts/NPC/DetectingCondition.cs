using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class DetectingCondition : BTAction
{
    private GameObject owner;
    private GameObject goose;
    private bool isDectected = false;
    public static bool gotYou = false;

    private float maxAngle = 90f;

    public DetectingCondition(GameObject owner)
    {
        this.owner = owner;
    }

    public override void Initialize()
    {
        goose = GameObject.FindWithTag("Goose");
    }

    public override NodeState Update()
    {
        OnDetectGoose();
        if (!isDectected)
        {
            gotYou = false;
            return NodeState.FAILURE;
        }
        else
        {
            gotYou = true;
            return NodeState.SUCCESS;
        } 
    }

    private void OnDetectGoose()
    {
        float distance = 2f;
        Vector3 vec3 = goose.transform.position - owner.transform.position;
        if(vec3.magnitude < distance)
        {
            float angle = Vector3.Angle(owner.transform.forward, vec3);
            if(angle<maxAngle)
            {
                isDectected=true;
            }
        }

    }
}
