using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallObject : PhysicObject
{
    public Vector3 setPos;
    public Rigidbody Rigidbody;
    private List<Collider> colliders;
    public GameObject handlePoint;

    private Rigidbody settngRb;

    public void Awake()
    {
        setPos = transform.position;
        Rigidbody = GetComponentInChildren<Rigidbody>();

        if (Rigidbody == null)
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
        Rigidbody.isKinematic = true;
        colliders =  new List<Collider>(GetComponentsInChildren<Collider>());

        settngRb = Rigidbody;

        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        isHeavy = false;
        isSound = false;
        //isGrab = true;
    }

    public override bool OnGrab(bool isgrab)
    {
        Rigidbody.isKinematic = false;

        switch (isgrab)
        {
            case true:
             
                isActive = true;
                Rigidbody.useGravity = true;

                return true;

            case false:

                Rigidbody.mass = 1;
                Rigidbody.useGravity = true;
                Rigidbody.constraints = RigidbodyConstraints.None;

                return false;
        }

    }

    public void Update()
    {
        if (isActive)
        {
            Rigidbody.isKinematic = false;

            foreach (var col in colliders)
            {
                col.enabled = true;
            }
        }
    }
    public override Vector3 OnTrigger()
    {

        return transform.position;
    }
}
