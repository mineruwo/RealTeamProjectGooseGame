using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenningScript : MonoBehaviour
{
    public void NextScene()
    {
        GameManager.instance.sceneMgr.NextScene();
    }
}
