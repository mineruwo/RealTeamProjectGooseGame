using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollider : MonoBehaviour
{
    public string objectName = "Goose";

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(objectName))
        {
            var questId = GetComponent<QuestID>();
            questId.GiveId();
        }
    }
}
