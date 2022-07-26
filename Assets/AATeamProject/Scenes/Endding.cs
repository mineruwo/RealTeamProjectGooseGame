using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endding : MonoBehaviour
{
    public void NextScene()
    {
        GameManager.instance.sceneMgr.NextScene(3);
    }
}
