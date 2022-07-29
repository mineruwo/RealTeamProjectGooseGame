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
        GameManager.instance.dataMgr.LoadQuestData(slotNum);
        GameManager.instance.sceneMgr.LoadGameScene();
        GameManager.instance.questMgr.ClearEvent += ShowNoteScrap;
    }

    public void OnClickDeleteButton(int slotNum)
    {
        GameManager.instance.dataMgr.DeleteQuestData(slotNum);
    }

    // 게임씬
    private GameObject questMenuUp;
    private GameObject questMenuDown;

    private GameObject[] questNoteLists;
    public GameObject[] questPages;

    private GameObject[] cursors;

    private GameObject scrapNote;

    private int currentCursor = 1;
    private Vector2 sizeUp = new Vector2(1.3f, 1.3f);
    private Vector2 sizeDown = new Vector2(1f, 1f);

    GameObject questTextObj;
    GameObject subQuestTextObj;

    private bool Stage1SubQuest = false;
    private bool Stage2SubQuest = false;

    public List<GameObject> deleteList;
    private string text;

    private bool isMainQuestOpen = false;

    public void SetQuestMenu(GameObject up, GameObject down,
        GameObject[] lists, GameObject[] cursors, GameObject[] pages)
    {
        questMenuUp = up;
        questMenuDown = down;
        questNoteLists = lists;
        this.cursors = cursors;
        questPages = pages;
    }
    public void SetScrapNote(GameObject note)
    {
        scrapNote = note;
    }

    public void ShowQuestWindow()
    {
        if (questMenuUp.activeSelf)
        {
            DeleteQuestNote();
            questMenuUp.SetActive(false);
            questMenuDown.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            questMenuUp.SetActive(true);
            questMenuDown.SetActive(true);
            WriteQuestNote();
            Time.timeScale = 0;
        }
    }

    public void ClickLeftArrow()
    {
        if (currentCursor > 0)
        {
            CursorSizeDown(cursors);
            questPages[currentCursor].SetActive(false);

            currentCursor--;

            CursorSizeUp(cursors);
            questPages[currentCursor].SetActive(true);
        }
    }

    public void ClickRightArrow()
    {
        if (currentCursor < cursors.Length - 1)
        {
            CursorSizeDown(cursors);
            questPages[currentCursor].SetActive(false);

            currentCursor++;

            CursorSizeUp(cursors);
            questPages[currentCursor].SetActive(true);
        }
    }

    public void CursorSizeUp(GameObject[] cursors)
    {
        cursors[currentCursor].gameObject.transform.localScale = sizeUp;
    }

    public void CursorSizeDown(GameObject[] cursors)
    {
        cursors[currentCursor].gameObject.transform.localScale = sizeDown;
    }

    public void DeleteQuestNote()
    {
        foreach (var del in deleteList)
        {
            var obj = del;
            obj.SetActive(false);
        }
        deleteList.Clear();
        subQuestTextObj = null;
    }

    public void WriteQuestNote()
    {
        foreach (var quest in GameManager.instance.dataMgr.currentQuestDataList.FindAll(x => x.stage == 1))
        {
            if (!quest.isMainQuest)
            {
                questTextObj = GameManager.instance.objpoolMgr.GetObject("QuestText");
                questTextObj.transform.parent = questNoteLists[1].transform;

                questTextObj.transform.localScale = Vector3.one;

                questTextObj.GetComponent<TextMeshProUGUI>().text = quest.questName;
                if (quest.isClear)
                {
                    questTextObj.GetComponent<TextMeshProUGUI>().text = "<s>" + quest.questName + "</s>";
                }
                deleteList.Add(questTextObj);
            }
        }

        foreach (var quest in GameManager.instance.dataMgr.currenSubQuestDataList.FindAll(x => x.stage == 1))
        {
            if (subQuestTextObj == null)
            {
                subQuestTextObj = GameManager.instance.objpoolMgr.GetObject("QuestCollectTextQ1");
                subQuestTextObj.transform.parent = questNoteLists[1].transform;
                subQuestTextObj.transform.localScale = Vector3.one;
                subQuestTextObj.GetComponent<TextMeshProUGUI>().text = "(피크닉 담요에 가져올 물건 : ";
                deleteList.Add(subQuestTextObj);
            }
            if (quest.isClear)
            {
                subQuestTextObj.GetComponent<TextMeshProUGUI>().text += "<s>" + quest.questName + "</s>" + ", ";
            }
            else
            {
                subQuestTextObj.GetComponent<TextMeshProUGUI>().text += quest.questName + ", ";
            }
        }
        subQuestTextObj.GetComponent<TextMeshProUGUI>().text = subQuestTextObj.GetComponent<TextMeshProUGUI>().text.TrimEnd(',', ' ');
        subQuestTextObj.GetComponent<TextMeshProUGUI>().text += ")";

        if (isMainQuestOpen)
        {
            foreach (var quest in GameManager.instance.dataMgr.currentQuestDataList.FindAll(x => x.id == 10001))
            {
                questTextObj = GameManager.instance.objpoolMgr.GetObject("QuestText");
                questTextObj.transform.parent = questNoteLists[1].transform;
                questTextObj.transform.localScale = Vector3.one;
                questTextObj.GetComponent<TextMeshProUGUI>().text = quest.questName;
                if (quest.isClear)
                {
                    questTextObj.GetComponent<TextMeshProUGUI>().text = "<s>" + quest.questName + "</s>";
                }
                deleteList.Add(questTextObj);
            }
        }
    }

    public void WriteMainQuestNote()
    {
        isMainQuestOpen = true;
    }

    public void GetClearQuest(string text)
    {
        this.text = text;
    }

    public void ShowNoteScrap()
    {
        if (scrapNote != null)
        {
            var textnote = scrapNote.transform.GetChild(0);
            textnote.GetComponent<TextMeshProUGUI>().text = "<s>" + text + "</s>";
            scrapNote.SetActive(true);
            StartCoroutine(ScrapeDown());
        }
    }

    public IEnumerator ScrapeDown()
    {
        yield return new WaitForSeconds(2.5f);
        scrapNote.SetActive(false);
    }

    public void OnClickSaveButton()
    {
        GameManager.instance.dataMgr.SaveQuestData();
    }
}

