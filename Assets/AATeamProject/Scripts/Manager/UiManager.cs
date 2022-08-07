using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UiManager : MonoBehaviour
{
    // 타이틀씬
    public Camera camera;

    public GameObject[] cameraPoints;
    public GameObject eraser;

    public GameObject[] timeTexts;
    public GameObject volState;
    public GameObject cameraState;

    private Vector3 eraserDefaultPosition;
    private int pointNum = 1;
    private int distance = 280;
    private bool isGetEraser = false;
    private bool isDelete = false;
    private int slotNum;

    private int volNum = 5;
    private string cameraStateName;
    private bool isLookGoose = true;

    public event Action deleteEvent;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGetEraser)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "Start")
                {
                    pointNum = 0;
                }
                if (hit.transform.gameObject.name == "Option")
                {
                    pointNum = 2;
                }

                if (hit.transform.gameObject.name == "arrow_book")
                {
                    pointNum = 1;
                }

                if (hit.transform.gameObject.name == "arrow_option")
                {
                    pointNum = 1;
                }

                if (hit.transform.gameObject.name == "savebook_yellow")
                {
                    GameManager.instance.uiMgr.OnClickGameSlot(1);
                }

                if (hit.transform.gameObject.name == "savebook_green")
                {
                    GameManager.instance.uiMgr.OnClickGameSlot(2);
                }

                if (hit.transform.gameObject.name == "savebook_red")
                {
                    GameManager.instance.uiMgr.OnClickGameSlot(3);
                }

                if (hit.transform.gameObject.name == "vol_arrowL")
                {
                    if(volNum > 0)
                    {
                        volNum--;
                        volState.GetComponent<TextMeshPro>().text = volNum.ToString();
                    }
                }
                if (hit.transform.gameObject.name == "vol_arrowR")
                {
                    if(volNum < 10)
                    {
                        volNum++;
                        volState.GetComponent<TextMeshPro>().text = volNum.ToString();
                    }
                }

                if (hit.transform.gameObject.name == "ca_arrowL")
                {
                    isLookGoose = !isLookGoose;
                    if (isLookGoose)
                    {
                        cameraState.GetComponent<TextMeshPro>().text = "거위만";
                    }
                    else
                    {
                        cameraState.GetComponent<TextMeshPro>().text = "거위 + 사람";
                    }
                }
                if (hit.transform.gameObject.name == "ca_arrowR")
                {
                    isLookGoose = !isLookGoose;
                    if (isLookGoose)
                    {
                        cameraState.GetComponent<TextMeshPro>().text = "거위만";
                    }
                    else
                    {
                        cameraState.GetComponent<TextMeshPro>().text = "거위 + 사람";
                    }
                }
            }
        }

        if(camera != null)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, cameraPoints[pointNum].transform.position, Time.deltaTime * 2f);
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "eraser")
                {
                    isGetEraser = true;
                    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                    hit.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            slotNum = eraser.GetComponent<Eraser>().slotNum;
            Debug.Log($"슬롯번호 : {slotNum}");
            if (slotNum == 1)
            {
                GameManager.instance.dataMgr.saveFileDate.Stage1Time = "비어있음";
                deleteEvent();
            }
            if (slotNum == 2)
            {
                GameManager.instance.dataMgr.saveFileDate.Stage2Time = "비어있음";
                deleteEvent();
            }
            if (slotNum == 3)
            {
                GameManager.instance.dataMgr.saveFileDate.Stage3Time = "비어있음";
                deleteEvent();
            }
            GameManager.instance.uiMgr.OnClickDeleteButton(slotNum);
            //UpdateSaveData();
            isGetEraser = false;
            isDelete = true;
            eraser.transform.position = eraserDefaultPosition;
        }

    }

    public void SetTitleMenu(Camera ca, GameObject[] cp, GameObject er,
        GameObject[] tt, GameObject vs, GameObject cs)
    {
        camera = ca;
        cameraPoints = cp;
        eraser = er;
        timeTexts = tt;
        volState = vs;
        cameraState = cs;
        eraserDefaultPosition = eraser.transform.position;
    }

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
    public void SetOptionMenu(GameObject vs, GameObject cs)
    {
        volState = vs;
        cameraState = cs;
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

    public void OnClickVolLeftArrow()
    {
        if(volNum > 0)
        {
            volNum--;
        }
    }
    public void OnClickVolRightArrow()
    {
        if(volNum < 10)
        {
            volNum++;                  
        }
    }

    public void OnClickCameraLeftArrow()
    {
        isLookGoose = !isLookGoose;
        if (isLookGoose)
        {
            cameraStateName = "거위만";
        }
        else
        {
            cameraStateName = "거위 + 사람";
        }
    }
    public void OnClickCameraRightArrow()
    {
        isLookGoose = !isLookGoose;
        if (isLookGoose)
        {
            cameraStateName = "거위만";
        }
        else
        {
            cameraStateName = "거위 + 사람";
        }
    }

    public int GetVolNum()
    {
        return volNum;
    }

    public string GetCameraState()
    {
        return cameraStateName;
    }

    public void OnClickSaveButton()
    {
        GameManager.instance.dataMgr.SaveQuestData();
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}

