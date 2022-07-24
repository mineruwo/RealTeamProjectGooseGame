using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestID : MonoBehaviour
{
    public int id;

    public void GiveId()
    {
        Debug.Log($"[QuestID] 아이디 주기 실행{id}");

        GameManager.instance.questMgr.GetQuestId(id);
    }

    public void GiveSubId()
    {
        Debug.Log($"[QuestID] 아이디 주기 실행{id}");

        GameManager.instance.questMgr.GetSubQuestId(id);
    }
}
