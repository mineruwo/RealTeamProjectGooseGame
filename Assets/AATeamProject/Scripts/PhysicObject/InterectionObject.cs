using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectionObject : PhysicObject
{
    public void Awake()
    {
        isActive = false;
        isHeavy = false;
        isSound = false;
        isGrab = true;
    }

    public override bool OnGrab(bool isgrab)
    {
        isActive = true;

        return true;
    }

    public override bool OnTrigger()
    {

        return true;
    }
}
