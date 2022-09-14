using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenningScript : MonoBehaviour
{
    public bool isSkip = false;
    public void Update()
    {
        if (Input.anyKeyDown && !isSkip)
        {
            NextScene();
            isSkip = true;
        }
    }
    public void NextScene()
    {
        GameManager.instance.sceneMgr.NextScene();
    }
}
