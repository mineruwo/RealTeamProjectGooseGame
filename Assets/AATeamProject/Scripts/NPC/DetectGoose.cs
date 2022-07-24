using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class DetectGoose : MonoBehaviour
{
    private RigBuilder rigBuilder;
    public GameObject goose;

    private float distance;

    private void Start()
    {
        rigBuilder = GetComponentInParent<RigBuilder>();
    }
    private void Update()
    {
        
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
}
