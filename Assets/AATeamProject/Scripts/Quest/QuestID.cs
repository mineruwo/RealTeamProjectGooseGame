using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestID : MonoBehaviour
{
    public int id;

    public void GiveId()
    {
        GameManager.instance.questMgr.GetQuestId(id);
    }
}
