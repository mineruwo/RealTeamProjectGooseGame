using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class Gardener : NPC
{
    public Animator animator;

    private RigBuilder rigBuilder;
    private NavMeshAgent agent;

    public List<NPCState> states;
    public NPCState initState;
    public NPCState currState;

    public Transform keyContainer;
    public Transform key;
    public Transform gardnerTwoHands;
    public Transform gardnerOneHand;
    public float pickUpRange = 3f;

    public Transform[] workPos;
    public GameObject goose;
    public GameObject[] items;

    public List<Job> jobs = new List<Job>();
    private Job Waterring = new Job();
    private Job Gardening = new Job();
    private Job Planting = new Job();

    public bool equipped;
    public bool isArrived;
    public bool isTakenByGoose;

    private NPCState state;
    private int index;

    private float timer;

    private void Start()
    {
        rigBuilder = GetComponent<RigBuilder>();
        agent = GetComponent<NavMeshAgent>();

        jobs.Add(Waterring);
        jobs.Add(Gardening);
        jobs.Add(Planting);

        Idle();
        Undetect();
    }
    private void Update()
    {
        if (state == NPCState.idle)
        {
            After5sec();
            Move();
        }

        if (state == NPCState.move)
        {
            var distance = Vector3.Distance(transform.position, workPos[index].position);
            if (agent.stoppingDistance > distance + 0.1f)
            {
                isArrived = true;
                Idle();
                index += 1;
            }
        }

        TouchGoose();

        if(index>workPos.Length-1)
        {
            index = 0;
        }
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
        state = NPCState.idle;
        animator.SetFloat("RemainingDistance", 0f);

        if(equipped)
        {

        }
    }
    public override void Move()
    {
        state = NPCState.move;
        animator.SetFloat("LocalVelocityZ", 0.5f);
        animator.SetFloat("RemainingDistance", 1f);

        agent.SetDestination(workPos[index].position);
    }

    public override void Chase()
    {
        state = NPCState.chase;
        isTakenByGoose = true;

        animator.SetFloat("LocalVelocityZ", 1f);
        animator.SetFloat("RemainingDistance", 1f);
    }

    public override void DoWork()
    {
        state = NPCState.work;

    }

    public override void PickUp()
    {
        foreach(var item in items)
        {
            item.transform.SetParent(gardnerOneHand.transform);
        }
        equipped = true;
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

    private void After5sec()
    {
        timer += Time.deltaTime;
        if (timer > 8f)
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
        //if(other.CompareTag("Item"))
        //{
        //    if(!equipped)
        //    {
        //        foreach (var item in items)
        //        {
        //            var distance = Vector3.Distance(transform.position, item.transform.position);
        //            if (distance < 1f)
        //            {
        //                PickUp();
        //            }

        //        }
        //    }
        //    else
        //    {
        //        DropDown();
        //    }
        //}

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
