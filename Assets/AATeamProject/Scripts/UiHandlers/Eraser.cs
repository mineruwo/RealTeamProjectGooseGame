using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    public int slotNum;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Eraser] 충돌한 물체{other.name}");
        if(other.gameObject.name == "savebook_yellow")
        {
            slotNum = 1;
        }
        if (other.gameObject.name == "savebook_green")
        {
            slotNum = 2;
        }
        if (other.gameObject.name == "savebook_red")
        {
            slotNum = 3;
        }
    }
}
