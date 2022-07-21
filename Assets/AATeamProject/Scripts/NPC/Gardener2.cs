using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public enum NPCState
{
    idle,
    work,
    chase,
}

public enum WorkTypes
{
    None,
    Water,
    Gardening,
    TakingVase,
}


public class Gardener2 : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;

    private RigBuilder rigBuilder;

    public NPCState initState;
    public NPCState currState;

    public Transform keyContainer;
    public Transform key;
    public Transform gardnerTwoHands;
    public Transform gardnerOneHand;
    public float pickUpRange = 3f;

    public GameObject goose;

    private GardenerJob working;

    public static bool equipped;
    public bool isArrived;
    public bool isTakenByGoose;

    private float timer;

    private NPCState currentState;

    
    private WorkTypes prevWorkType;
    private WorkTypes workType;
    private int prevWorkStep;
    private int workStep = 0;


    // Watrt
    // step 1: 물뿌리개로 가
    // step 2: 꽃으로 가
    // step 3: an

    private Vector3 targetPos;
    public bool isFinished = false;


    public NPCState CurrentState
    {
        get { return currentState; }
        private set
        { 
            currentState = value;
            switch (currentState)
            {
                case NPCState.idle:
                    timer = 0f;
                    animator.SetFloat("RemainingDistance", 0f);
                    break;
                case NPCState.work:
                    break;
                case NPCState.chase:

                    break;
            }
        }
    }

    public WorkTypes WorkType
    {
        get { return workType; }
        private set
        {
            CurrentState = NPCState.work;
            prevWorkType = workType;
            workType = value;
            switch (workType)
            {
                case WorkTypes.None:
                    Debug.LogError("!");
                    break;
                case WorkTypes.Water:
                    animator.SetFloat("LocalVelocityZ", 0.5f);
                    animator.SetFloat("RemainingDistance", 1f);

                    targetPos = GameObject.Find("WateringPos1").transform.position;
                    agent.SetDestination(targetPos);

                    break;
                case WorkTypes.Gardening:

                    break;
                case WorkTypes.TakingVase:

                    break;
            }
        }
    }

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        rigBuilder = GetComponent<RigBuilder>();
        working = GetComponent<GardenerJob>();
        timer = 0f;

        CurrentState = NPCState.idle;
    }



    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case NPCState.idle:
                UpdateIdle();
                break;
            case NPCState.work:
                UpdateWork();
                break;
            case NPCState.chase:
                UpdateChase();
                break;
        }
    }

    private void UpdateChase()
    {

    }

    private void UpdateWork()
    {
        switch (WorkType)
        {
            case WorkTypes.None:
                break;
            case WorkTypes.Water:
                UpdateWorkWater();
                break;
            case WorkTypes.Gardening:
                UpdateGardening();
                break;
            case WorkTypes.TakingVase:
                UpdateTakingVase();
                break;
        }
    }

    private void UpdateTakingVase()
    {
    }

    private void UpdateGardening()
    {
        switch(workStep)
        {
            case 1:
                break;
            case 2:
                break;
        }
    }

    private void UpdateWorkWater()
    {
        var distance = Vector3.Distance(transform.position, targetPos);
        prevWorkStep = workStep;
        //물뿌리개주워
        if(distance <= 0.1f)
        {
            CurrentState = NPCState.idle;
        }

        switch (workStep)
        {
            case 1:
                targetPos = GameObject.Find("WateringPos2").transform.position;
                agent.SetDestination(targetPos);

                break;

            case 2:
                //물주고
                //물주는애니메이션
                animator.SetBool("JobActive", true);
                animator.SetInteger("JobIndex", 0);
                UpdateIdle();
                break;

            case 3:
                //앞에서서돌고
                //idle
                UpdateIdle();
                break;

            case 4:
       

            case 5:
                //다시물뿌리개들고가고
                break;

            case 6:
                //다시물뿌리개제자리에둬
                //finish
                break;
        }



    }

    private void UpdateIdle()
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            timer = 0f;
            CurrentState = NPCState.work;
            WorkType = WorkTypes.Water;
        }
    }

    private void TurnSide()
    {
        var from = transform.forward;
        var to = GameObject.Find("platerman").transform.position - transform.position;
        to.Normalize();

        var angle = Vector3.SignedAngle(transform.forward, to, Vector3.up);
        Debug.Log(angle);

        animator.SetFloat("RemainingRotation", angle);
        animator.SetFloat("TurnSpeed", angle > 0f ? 2f : -2f);
    }
}
