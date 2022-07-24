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
        Debug.Log($"[QuestManage] 받아온 id : {currentQuestId}");
        CheckIsClear();
    }

    private void CheckIsClear()
    {
        int num = GameManager.instance.dataMgr.currentQuestDataList.
            FindIndex(x => x.id == currentQuestId);

        if (GameManager.instance.dataMgr.currentQuestDataList[num].isClear)
        {
            Debug.Log($"[QuestManage] 이미 클리어한 퀘스트 {GameManager.instance.dataMgr.currentQuestDataList[num].questName}");
        }
        else
        {
            Debug.Log($"[QuestManage] 퀘스트 클리어 {GameManager.instance.dataMgr.currentQuestDataList[num].questName}");
            GameManager.instance.dataMgr.currentQuestDataList[num].isClear = true;
            GameManager.instance.uiMgr.GetClearQuest(GameManager.instance.dataMgr.currentQuestDataList[num].questName);
            GameManager.instance.dataMgr.currentQuestCount.stage1--;
            if(GameManager.instance.dataMgr.currentQuestCount.stage1 == 1)
            {
                Debug.Log($"[QuestManage] 메인퀘스트 생성");
                GameManager.instance.uiMgr.WriteMainQuestNote();
            }
            if (ClearEvent == null)
            {
                Debug.Log($"[QuestManage] 이벤트 비어있음");
            }
            if (ClearEvent != null)
            {
                Debug.Log($"[QuestManage] 이벤트 발동");

                ClearEvent();
            }
        }
    }

    public void GetSubQuestId(int id)
    {
        currentQuestId = id;
        Debug.Log($"[QuestManage] 받아온 id : {currentQuestId}");
        CheckSubIsClear();
    }

    private void CheckSubIsClear()
    {
        int num = GameManager.instance.dataMgr.currenSubQuestDataList.
            FindIndex(x => x.id == currentQuestId);

        if (GameManager.instance.dataMgr.currenSubQuestDataList[num].isClear)
        {
            Debug.Log($"[QuestManage] 이미 클리어한 퀘스트 {GameManager.instance.dataMgr.currenSubQuestDataList[num].questName}");
        }
        else
        {
            Debug.Log($"[QuestManage] 퀘스트 클리어 {GameManager.instance.dataMgr.currenSubQuestDataList[num].questName}");
            GameManager.instance.dataMgr.currenSubQuestDataList[num].isClear = true;

            int mainNum = GameManager.instance.dataMgr.currentQuestDataList.
            FindIndex(x => x.id == GameManager.instance.dataMgr.currenSubQuestDataList[num].surQuestId);

            Debug.Log($"[QuestManage] 퀘스트 남은거 까였는지 확인 {GameManager.instance.dataMgr.currentQuestDataList[mainNum].subQuestCount}");
            GameManager.instance.dataMgr.currentQuestDataList[mainNum].subQuestCount--;
            Debug.Log($"[QuestManage] 퀘스트 남은거 까였는지 확인 {GameManager.instance.dataMgr.currentQuestDataList[mainNum].subQuestCount}");

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
