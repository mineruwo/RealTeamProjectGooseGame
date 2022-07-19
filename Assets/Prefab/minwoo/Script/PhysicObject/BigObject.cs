using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigObject : PhysicObject
{
    public Vector3 setPos;
    private Rigidbody Rigidbody;
    private List<Collider> colliders;
    public List<GameObject> handlePoint;

    public void Awake()
    {
        setPos = transform.position;
        Rigidbody = GetComponentInChildren<Rigidbody>();

        if (Rigidbody == null)
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        Rigidbody.isKinematic = true;
        colliders = new List<Collider>(GetComponentsInChildren<Collider>());

        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        isActive = false;
        isHeavy = true;
        isSound = false;
        isGrab = true;
    }

    public override bool OnGrab(bool isgrab)
    {
        Rigidbody.isKinematic = false;

        switch (isgrab)
        {
            case true:
                foreach (var col in colliders)
                {
                    col.enabled = true;
                }
                isActive = true;

                return true;

            case false:

                foreach (var col in colliders)
                {
                    col.enabled = false;
                }

                return false;
        }

    }

    public override bool OnTrigger()
    {
        return true;
    }
}
