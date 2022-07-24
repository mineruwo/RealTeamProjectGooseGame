using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollect : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[QuestCollect] �浹�� ��ü{other.name}");
        if (other.gameObject.CompareTag("Stage1Col"))
        {
            Debug.Log($"[QuestCollect] ����Ʈ �ߵ�{other.name}");

            var questId = other.GetComponent<QuestID>();
            questId.GiveSubId();
        }
    }
}
