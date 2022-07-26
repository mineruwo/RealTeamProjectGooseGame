using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUI : MonoBehaviour
{
   
    private void NextScene()
    {
        GameManager.instance.sceneMgr.NextScene();
    }
}
