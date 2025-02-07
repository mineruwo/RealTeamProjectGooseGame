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

public enum ChasingSituations
{
    DefaultChase,
    GetWetfeet,
    WetChase,
    MissItem,
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
    public GameObject item;
    public Rigidbody rd;
    public ParticleSystem particle;

    public static bool equipped;
    public bool isArrived;
    public bool isTakenByGoose;

    private float timer;

    private NPCState currentState;
    
    private WorkTypes prevWorkType;
    private WorkTypes workType;
    private ChasingSituations chasingSituation;
    private int prevWorkStep;
    private int workStep = 0;

    private bool isWetted;


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
                    animator.SetFloat("LocalVelocityZ", 0.5f);
                    animator.SetFloat("RemainingDistance", 1f);

                    break;
                case WorkTypes.TakingVase:

                    break;
            }
        }
    }

    public ChasingSituations ChasingSituation
    {
        get { return chasingSituation; }
        private set
        {
            CurrentState = NPCState.chase;
            chasingSituation = value;
            switch (chasingSituation)
            {
                case ChasingSituations.DefaultChase:
                    break;
                case ChasingSituations.GetWetfeet:
                    gameObject.GetComponent<NavMeshAgent>().enabled = false;
                    animator.SetBool("ReactionActive", true);
                    animator.SetInteger("ReactionIndex", 1);
                    break;

                case ChasingSituations.WetChase:
                    gameObject.GetComponent<NavMeshAgent>().enabled = true;
                    agent.SetDestination(targetPos);
                    animator.SetBool("ReactionActive", false);
                    break;
                case ChasingSituations.MissItem:
                    //두리번거리는 애니메이션
                    //그냥 집에 돌아감
                    break;
            }
        }
    }

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        rigBuilder = GetComponent<RigBuilder>();
        timer = 0f;
        isWetted = false;

        CurrentState = NPCState.idle;
    }


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
        switch (ChasingSituation)
        {
            case ChasingSituations.DefaultChase:
                UpdateDefaultChase();
                break;
            case ChasingSituations.GetWetfeet:
                UpdateGetWetfeet();
                break;
            case ChasingSituations.WetChase:
                UpdateWetChase();
                break;
            case ChasingSituations.MissItem:
                break;
        }

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
        if(distance < 0.05f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            animator.SetFloat("RemainingDistance", 0f);
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                workStep += 1;
                if(workStep>4)
                    workStep = 0;
            }

        }

        switch (workStep)
        {
            case 1:
                if(item != null)
                {
                    if(item.CompareTag("Item"))
                    {
                        equipped = true;
                        rd = item.GetComponent<Rigidbody>();
                        item.transform.SetParent(gardnerOneHand, true);
                        rd.isKinematic = true;
                        
                    }
                }

                break;

            case 2:
                targetPos = GameObject.Find("WateringPos2").transform.position;
                animator.SetFloat("LocalVelocityZ", 0.5f);
                animator.SetFloat("RemainingDistance", 1f);
                agent.SetDestination(targetPos);
                break;

            case 3:
                transform.LookAt(GameObject.FindGameObjectWithTag("WorkSpot").transform);
                break;
            case 4:
                //물주기
                particle = item.GetComponentInChildren<ParticleSystem>();
                animator.SetBool("JobActive", true);
                animator.SetInteger("JobIndex", 0);

                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    isFinished = true;
                    if (isFinished)
                    {
                        workStep = 5;
                    }

                }
                break;

            case 5:
                UpdateIdle();
                Debug.Log("d");
                break;

            case 6:
                //다시물뿌리개제자리에둬
                //finish
                break;
        }



    }

    private void UpdateDefaultChase()
    {
        animator.SetFloat("LocalVelocityZ", 1f);
        animator.SetFloat("RemainingDistance", 1f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void UpdateGetWetfeet()
    {
        timer += Time.deltaTime;
        if (timer>3f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            ChasingSituation = ChasingSituations.WetChase;
        }
    }

    private void UpdateWetChase()
    {
        animator.SetFloat("LocalVelocityZ", 0.7f);
        animator.SetFloat("RemainingDistance", 1f);
    }

    private void UpdateIdle()
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            timer = 0f;
            CurrentState = NPCState.work;
            WorkType = WorkTypes.Water;
            agent.SetDestination(targetPos);
        }

        if(equipped)
        {
            agent.SetDestination(targetPos); //이전장소저장한 거 불러오기
            animator.SetFloat("LocalVelocityZ", 0.5f);
            animator.SetFloat("RemainingDistance", 1f);
        }
    }

    private void TurnSide()
    {
        var from = transform.forward;
        var to = GameObject.FindGameObjectWithTag("Item").transform.position - transform.position;
        to.Normalize();

        var angle = Vector3.SignedAngle(transform.forward, to, Vector3.up);
        Debug.Log(angle);

        transform.LookAt(to);
        animator.SetFloat("RemainingRotation", angle);
        animator.SetFloat("TurnSpeed", angle > 0f ? 2f : -2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isWetted && other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            timer = 0f;
            ChasingSituation = ChasingSituations.GetWetfeet;
            isWetted = true;
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
            Debug.Log("나감");
        }

    }
    public void Detect()
    {
        rigBuilder.layers[0].active = true;
    }
    public void Undetect()
    {
        rigBuilder.layers[0].active = false;
    }

}
