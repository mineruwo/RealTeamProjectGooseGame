using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollect : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stage1Col"))
        {
            var questId = other.GetComponent<QuestID>();
            questId.GiveId();
        }
    }
}
