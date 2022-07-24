using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollect : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[QuestCollect] 충돌한 물체{other.name}");
        if (other.gameObject.CompareTag("Stage1Col"))
        {
            Debug.Log($"[QuestCollect] 퀘스트 발동{other.name}");

            var questId = other.GetComponent<QuestID>();
            questId.GiveSubId();
        }
    }
}
