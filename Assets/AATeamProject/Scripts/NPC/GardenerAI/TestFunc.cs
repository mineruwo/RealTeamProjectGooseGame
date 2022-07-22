using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunc : MonoBehaviour
{
    public static bool isFinishedWater = false;
    public void FinishWatering()
    {
        isFinishedWater = true;
    }
}
