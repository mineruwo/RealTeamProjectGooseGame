using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class Gardener : NPC
{
    public Animator animator;

    private RigBuilder rigBuilder;

    public List<NPCState> states;
    public NPCState initState;
    public NPCState currState;

    public Transform keyContainer;
    public Transform key;
    public Transform gardnerTwoHands;
    public Transform gardnerOneHand;
    public float pickUpRange = 3f;

    public GameObject goose;
    public GameObject[] items;

    private GardenerJob working;

    public static bool equipped;
    public bool isArrived;
    public bool isTakenByGoose;

    private NPCState state;

    private float timer;

    private void Start()
    {
        rigBuilder = GetComponent<RigBuilder>();
        working = GetComponent<GardenerJob>();
        timer = 0f;

        Idle();
        Undetect();
    }
    private void Update()
    {
        //if(state==NPCState.idle)
        //{

        //}
        Water();
        //if(state==NPCState.work)
        //{

        //}

        TouchGoose();
    }
    public override void Detect()
    {
        rigBuilder.layers[0].active = true;
    }
    public override void Undetect()
    {
        rigBuilder.layers[0].active = false;
    }
    public override void Idle()
    {
        animator.SetFloat("RemainingDistance", 0f);
    }
    public override void Move()
    {
        animator.SetFloat("LocalVelocityZ", 0.5f);
        animator.SetFloat("RemainingDistance", 1f);
    }

    public override void Chase()
    {
        state = NPCState.chase;
        isTakenByGoose = true;

        animator.SetFloat("LocalVelocityZ", 1f);
        animator.SetFloat("RemainingDistance", 1f);
    }

    public override void PickUp()
    {
        equipped = true;
        foreach (var item in items)
        {
            item.transform.SetParent(gardnerOneHand.transform);
        }

    }

    public void Water()
    {
        state = NPCState.work;
        working.Watering();
    }

    public override void DropDown()
    {
        base.DropDown();
        equipped = false;
    }
    public override void TouchGoose()
    {
        base.TouchGoose();

        var gooseVec = transform.position - goose.transform.position;
        var backward = transform.forward * -1f;
        var offset = backward - gooseVec;

        float deg = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        if(deg>45 && gooseVec.y < 1f)
        {
            //if(gooseVec.x > 0f)
            //{
            //    animator.SetFloat("RemainingRotation", -50f);
            //    animator.SetFloat("TurnSpeed", 1f);
            //    agent.transform.LookAt(goose.transform.position);
            //}
            //else if(gooseVec.x < 0f)
            //{
            //    animator.SetFloat("RemainingRotation", 50f);
            //    animator.SetFloat("TurnSpeed", 1f);
            //}
        }

    }

    public void After5sec()
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            timer = 0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Goose"))
        {
            Detect();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Goose"))
        {
            Undetect();
        }
    }

    public void SetJobs()
    {

    }
}
