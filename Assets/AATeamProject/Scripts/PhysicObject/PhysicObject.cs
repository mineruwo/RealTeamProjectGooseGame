using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicObject : MonoBehaviour
{

    public bool isGrab;

    public bool isSound;

    public bool isHeavy;

    public bool isActive;

    
    public abstract bool OnTrigger();

    public abstract bool OnGrab(bool isgrab);

}
