using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestID : MonoBehaviour
{
    public int id;

    public void GiveId()
    {
        Debug.Log($"[QuestID] ���̵� �ֱ� ����{id}");

        GameManager.instance.questMgr.GetQuestId(id);
    }

    public void GiveSubId()
    {
        Debug.Log($"[QuestID] ���̵� �ֱ� ����{id}");

        GameManager.instance.questMgr.GetSubQuestId(id);
    }
}
