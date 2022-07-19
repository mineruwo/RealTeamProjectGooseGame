using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int currentQuestId;

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
            //GameManager.instance.uiMgr.ShowNoteScrap(GameManager.instance.dataMgr.currentQuestDataList[num].questName);
            
        }
    }
}
