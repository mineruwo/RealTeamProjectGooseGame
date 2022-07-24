using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private string moveXName = "Vertical";     
    private string moveZName = "Horizontal";   

    public float moveX { get; private set; }
    public float moveZ { get; private set; }

    private void Update()
    {
#if UNITY_EDITOR
        moveX = Input.GetAxis(moveXName);
        moveZ = -1 * Input.GetAxis(moveZName);
#endif
#if UNITY_ANDROID
        //moveX = VirutalJoystick.instance.GetAxis(moveXName);
        //moveZ = -1 * VirutalJoystick.instance.GetAxis(moveZName);
#endif
    }

    public float MoveValue()
    {
        return Mathf.Abs(moveX) + Mathf.Abs(moveZ);
    }


    public bool IsMove()
    {
        if (moveX == 0 && moveZ == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
