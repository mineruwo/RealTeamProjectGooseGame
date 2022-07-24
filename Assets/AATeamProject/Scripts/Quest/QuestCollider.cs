using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollider : MonoBehaviour
{
    public string objectName = "Goose";

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[QuestCollect] 충돌한 물체{other.name}");

        if (other.gameObject.CompareTag(objectName))
        {
            Debug.Log($"[QuestCollect] 퀘스트 발동{other.name}");

            var questId = GetComponent<QuestID>();
            questId.GiveId();
        }
    }
}
