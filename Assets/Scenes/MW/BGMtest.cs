using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMtest : MonoBehaviour
{

   
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.instance.audioMgr.BGMPlay();
        }
    }
}
