using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private string moveXName = "Vertical";
    private string moveZName = "Horizontal";

    public float moveX { get; private set; }
    public float moveZ { get; private set; }

    public GameObject joyStick;
    private VirtualJoyStick stickCon;

    private void Update()
    {
        // moveX = Input.GetAxis(moveXName);
        //moveZ = Input.GetAxis(moveZName);


        //#if UNITY_EDITOR
        //moveX = Input.GetAxis(moveXName);
        //moveZ = -1 * Input.GetAxis(moveZName);
        //#endif
        //#if UNITY_ANDROID


        if (joyStick != null)
        {
            //Debug.Log($"[InputManager] 조이스틱 찾음?{joyStick.name}");
            //if (stickCon == null)
            //{
            var stickCon = joyStick.GetComponent<VirtualJoyStick>();
            //Debug.Log($"[InputManager] 스크립트 찾음?{stickCon.name}");

            //}
            //Debug.Log($"[InputManager] 값 출력?{moveX}");
            moveX = stickCon.GetAxis(moveXName);
            moveZ = stickCon.GetAxis(moveZName);

        }


        //#endif
    }

    public void SetJoyStick(GameObject stick)
    {
        joyStick = stick;
    }

    //public float MoveValue()
    //{
    //    return Mathf.Abs(moveX) + Mathf.Abs(moveZ);
    //}


    //public bool IsMove()
    //{
    //    if (moveX == 0 && moveZ == 0)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
}
