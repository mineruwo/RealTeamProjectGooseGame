using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMtest : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GlobalGameMgr.instance.audioMgr.BGMPlay();
        }
    }
}
