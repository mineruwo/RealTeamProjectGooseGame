using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollider : MonoBehaviour
{
    public string objectName = "Goose";

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[QuestCollect] �浹�� ��ü{other.name}");

        if (other.gameObject.CompareTag(objectName))
        {
            Debug.Log($"[QuestCollect] ����Ʈ �ߵ�{other.name}");

            var questId = GetComponent<QuestID>();
            questId.GiveId();
        }
    }
}
