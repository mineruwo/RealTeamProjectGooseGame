using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class QuestManager : MonoBehaviour
{
    public int currentQuestId;

    public event Action ClearEvent;

    public void GetQuestId(int id)
    {
        currentQuestId = id;
        CheckIsClear();
    }

    private void CheckIsClear()
    {
        int num = GameManager.instance.dataMgr.currentQuestDataList.
            FindIndex(x => x.id == currentQuestId);

        if (!GameManager.instance.dataMgr.currentQuestDataList[num].isClear)
        {
            GameManager.instance.dataMgr.currentQuestDataList[num].isClear = true;
            GameManager.instance.uiMgr.GetClearQuest(GameManager.instance.dataMgr.currentQuestDataList[num].questName);
            GameManager.instance.dataMgr.currentQuestCount.stage1--;
            if (GameManager.instance.dataMgr.currentQuestCount.stage1 == 1)
            {
                GameManager.instance.uiMgr.WriteMainQuestNote();
            }
            if (ClearEvent != null)
            {
                ClearEvent();
            }
        }
    }

    public void GetSubQuestId(int id)
    {
        currentQuestId = id;
        CheckSubIsClear();
    }

    private void CheckSubIsClear()
    {
        int num = GameManager.instance.dataMgr.currenSubQuestDataList.
            FindIndex(x => x.id == currentQuestId);

        if (!GameManager.instance.dataMgr.currenSubQuestDataList[num].isClear)
        {
            GameManager.instance.dataMgr.currenSubQuestDataList[num].isClear = true;

            int mainNum = GameManager.instance.dataMgr.currentQuestDataList.
            FindIndex(x => x.id == GameManager.instance.dataMgr.currenSubQuestDataList[num].surQuestId);

            GameManager.instance.dataMgr.currentQuestDataList[mainNum].subQuestCount--;

            int count = (int)GameManager.instance.dataMgr.currentQuestDataList[mainNum].subQuestCount;
            if (count == 0)
            {
                GetQuestId(GameManager.instance.dataMgr.currentQuestDataList[mainNum].id);
            }
        }
    }

    public void OpenMainQuest(bool setTrigger)
    {
        setTrigger = true;
    }
}
