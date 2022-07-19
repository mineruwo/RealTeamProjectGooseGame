using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    // 타이틀씬
    public void OnClickGameSlot(int slotNum)
    {
        Debug.Log($"[UiManager] {slotNum}번 슬롯 누름");

        GameManager.instance.dataMgr.LoadQuestData(slotNum);
        GameManager.instance.sceneMgr.LoadGameScene();
    }

    public void OnClickDeleteButton(int slotNum)
    {
        GameManager.instance.dataMgr.DeleteQuestData(slotNum);

    }

    // 게임씬

    private int currentCursor = 1;

    private Vector2 sizeUp = new Vector2(1.3f, 1.3f);
    private Vector2 sizeDown = new Vector2(1f, 1f);

    private bool Stage1SubQuest = false;
    private bool Stage2SubQuest = false;


    public void OnClickSaveButton()
    {
        GameManager.instance.dataMgr.SaveQuestData();
    }

    public void OnClickQuestButton(GameObject upMenu, GameObject downMenu)
    {
        if (upMenu.activeSelf)
        {
            upMenu.SetActive(false);
            downMenu.SetActive(false);
        }
        else
        {
            //WirteQuestNote();
            upMenu.SetActive(true);
            downMenu.SetActive(true);
        }
    }




    public void OnClickLeftArrow(GameObject[] cursors)
    {
        if (currentCursor > 0)
        {
            CursorSizeDown(cursors);
            //questNoteLists[currentCursor].SetActive(false);

            currentCursor--;

            CursorSizeUp(cursors);
            //questNoteLists[currentCursor].SetActive(true);
        }
    }

    //public void OnClickRightArrow()
    //{
    //    if (currentCursor < cursors.Length - 1)
    //    {
    //        CursorSizeDown(currentCursor);
    //        questNoteLists[currentCursor].SetActive(false);

    //        currentCursor++;

    //        CursorSizeUp(currentCursor);
    //        questNoteLists[currentCursor].SetActive(true);
    //    }
    //}

    public void CursorSizeUp(GameObject[] cursors)
    {
        cursors[currentCursor].gameObject.transform.localScale = sizeUp;
    }

    public void CursorSizeDown(GameObject[] cursors)
    {
        cursors[currentCursor].gameObject.transform.localScale = sizeDown;
    }

    GameObject questTextObj;
    GameObject subQuestTextObj;

    public void ClearNote(GameObject[] questNoteLists)
    {
        GameObject[] allChildren = questNoteLists[1].GetComponentsInChildren<GameObject>();
        foreach (GameObject child in allChildren)
        {
            Debug.Log($"[UiManager] 삭제함");
            if (child != questNoteLists[1])
            {
                child.SetActive(false);
            }
        }
    }

    private string text;
    public void GetClearQuest(string text)
    {
        this.text = text;
    }

    GameObject scrapNote;
    public void ShowNoteScrap(string name)
    {
        if (scrapNote == null)
        {
            scrapNote = GameObject.FindGameObjectWithTag("ScrapNote");
        }
        var text = scrapNote.transform.GetChild(0);
        text.GetComponent<TextMeshProUGUI>().text = "<s>" + name + "</s>";
        scrapNote.SetActive(true);
        StartCoroutine(ScrapeDown());
    }

    public IEnumerator ScrapeDown()
    {
        yield return new WaitForSeconds(2.5f);
        scrapNote.SetActive(false);
    }

    public void SetNoteScrap(GameObject note)
    {
        scrapNote = note;
    }

    public void NoteScrapeUp(GameObject NoteScrap)
    {
        var text = NoteScrap.transform.GetChild(0);
        text.GetComponent<TextMeshProUGUI>().text = "<s>" + text + "</s>";
        NoteScrap.SetActive(true);
    }

    public List<GameObject> deList;

    public void WriteQuestNote(GameObject[] questNoteLists)
    {
        foreach (var del in deList)
        {
            var obj = del;
            obj.SetActive(false);
        }
        deList.Clear();

        foreach (var quest in GameManager.instance.dataMgr.currentQuestDataList.FindAll(x => x.stage == 1))
        {
            if (quest.id.ToString().Length != 4 && !quest.isMainQuest)
            {
                questTextObj = GameManager.instance.objpoolMgr.GetObject("QuestText");
                questTextObj.transform.parent = questNoteLists[1].transform;
                questTextObj.GetComponent<TextMeshProUGUI>().text = quest.questName;
                if (quest.isClear)
                {
                    questTextObj.GetComponent<TextMeshProUGUI>().text = "<s>" + quest.questName + "</s>";
                }
                deList.Add(questTextObj);
            }
            if(quest.id.ToString().Length == 4)
            {
                if (!Stage1SubQuest)
                {
                    subQuestTextObj = GameManager.instance.objpoolMgr.GetObject("QuestCollectTextQ1");
                    subQuestTextObj.transform.parent = questNoteLists[1].transform;
                    subQuestTextObj.GetComponent<TextMeshProUGUI>().text = "(피크닉 담요에 가져올 물건 : ";
                    subQuestTextObj.GetComponent<TextMeshProUGUI>().text += quest.questName;
                    if (quest.isClear)
                    {
                        subQuestTextObj.GetComponent<TextMeshProUGUI>().text += "<s>" + quest.questName + "</s>";
                    }
                    Stage1SubQuest = true;
                    deList.Add(subQuestTextObj);
                }
                else
                {
                    subQuestTextObj.GetComponent<TextMeshProUGUI>().text += (", " + quest.questName);
                    if (quest.isClear)
                    {
                        subQuestTextObj.GetComponent<TextMeshProUGUI>().text += (", " +"<s>" + quest.questName + "</s>");
                    }
                }
            }
        }
        subQuestTextObj.GetComponent<TextMeshProUGUI>().text += ")";
        Stage1SubQuest = false;


        //for (int i = 0; i < DataManager.currentQuestDataList.Count; i++)
        //{
        //    if (DataManager.currentQuestDataList[i].stage == 1 && !DataManager.currentQuestDataList[i].isMainQuest)
        //    {
        //        if (DataManager.currentQuestDataList[i].id.ToString().Length != 4)
        //        {
        //            textObj = Instantiate(questText, questNoteLists[1].transform.GetChild(0));
        //            textObj.GetComponent<TextMeshProUGUI>().text = DataManager.currentQuestDataList[i].questName;
        //            if (DataManager.currentQuestDataList[i].isClear)
        //            {
        //                textObj.GetComponent<TextMeshProUGUI>().text = "<s>" + textObj.GetComponent<TextMeshProUGUI>().text + "</s>";
        //            }
        //            //questTextDic.Add(DataManager.currentQuestDataList[i].id, textObj);
        //        }
        //        else // ?? ?κ? ????
        //        {
        //            if (!Stage1SubQuest)
        //            {
        //                subTextObj = Instantiate(questCollectTexts[0], questNoteLists[1].transform.GetChild(0));
        //                subTextObj.GetComponent<TextMeshProUGUI>().text += DataManager.currentQuestDataList[i].questName;
        //                if (DataManager.currentQuestDataList[i].isClear)
        //                {
        //                    subTextObj.GetComponent<TextMeshProUGUI>().text += "<s>" + subTextObj.GetComponent<TextMeshProUGUI>().text + "</s>";
        //                }
        //                Stage1SubQuest = true;
        //            }
        //            else
        //            {
        //                subTextObj.GetComponent<TextMeshProUGUI>().text += (", " + DataManager.currentQuestDataList[i].questName);
        //                if (DataManager.currentQuestDataList[i].isClear)
        //                {
        //                    subTextObj.GetComponent<TextMeshProUGUI>().text += (", " +"<s>" + subTextObj.GetComponent<TextMeshProUGUI>().text + "</s>");
        //                }
        //            }
        //        }
        //    }
        //}
        //subTextObj.GetComponent<TextMeshProUGUI>().text += ")";
    }
}

