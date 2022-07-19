using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goose"))
        {
            var questId = GetComponent<QuestID>();
            questId.GiveId();
        }
    }
}
