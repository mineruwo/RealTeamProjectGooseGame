using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHandlerGame : MonoBehaviour
{
    public GameObject questMenuUp;
    public GameObject questMenuDown;

    public GameObject[] questNoteLists;

    public GameObject[] cursors;
    private Vector2 sizeUp = new Vector2(1.3f, 1.3f);
    private Vector2 sizeDown = new Vector2(1f, 1f);

    public GameObject questNoteScrap;

    public void Start()
    {
        GameManager.instance.uiMgr.SetQuestMenu(questMenuUp, questMenuDown
            , questNoteLists, cursors);
        GameManager.instance.uiMgr.SetScrapNote(questNoteScrap);
    }

    public void OnClickQuestButton()
    {
        Debug.Log($"[UiHandlerGame] 퀘스트 버튼 누름");
        GameManager.instance.uiMgr.ShowQuestWindow();
        // GameManager.instance.uiMgr.ClearNote(questNoteLists);
        //GameManager.instance.uiMgr.OnClickQuestButton(questMenuUp, questMenuDown);
        //GameManager.instance.uiMgr.WriteQuestNote();        
    }

    public void OnClickLeftArrow()
    {
        Debug.Log($"[UiHandlerGame] 왼쪽 화살표 누름");

        GameManager.instance.uiMgr.ClickLeftArrow();
    }
    public void OnClickRightArrow()
    {
        Debug.Log($"[UiHandlerGame] 오른쪽 화살표 누름");

        GameManager.instance.uiMgr.ClickRightArrow();
    }





    public void OnClickSaveButton()
    {
        GameManager.instance.uiMgr.OnClickSaveButton();
    }


}
