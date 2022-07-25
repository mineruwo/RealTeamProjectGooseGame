using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.AI;

public class DetectGoose : MonoBehaviour
{
    private RigBuilder rigBuilder;
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject goose;
    public static bool isWetted;

    private float distance;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rigBuilder = GetComponentInParent<RigBuilder>();
    }
    private void Update()
    {
    }

    public void AggroFromGoose()
    {
        transform.forward = goose.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Goose"))
        {
            rigBuilder.layers[0].active = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Goose"))
        {
            rigBuilder.layers[0].active = false;
            Debug.Log("³ª°¨");

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isWetted && other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            isWetted = true;
        }
    }

}
