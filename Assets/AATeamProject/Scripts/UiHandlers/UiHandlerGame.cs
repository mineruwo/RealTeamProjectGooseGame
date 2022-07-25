using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandlerGame : MonoBehaviour
{
    public GameObject questMenuUp;
    public GameObject questMenuDown;

    public GameObject[] questNoteLists;
    public GameObject[] questPages;

    public GameObject[] cursors;
    private Vector2 sizeUp = new Vector2(1.3f, 1.3f);
    private Vector2 sizeDown = new Vector2(1f, 1f);

    public GameObject questNoteScrap;

    public GameObject joyStick;

    // ¹öÆ°
    public PlayerController gooseCon;
    public GooseGrab gooseGrab;
    public Button grab;
    public Button sneck;
    public Button wing;
    public Button run;

    public void Start()
    {
        GameManager.instance.uiMgr.SetQuestMenu(questMenuUp, questMenuDown
            , questNoteLists, cursors, questPages);
        GameManager.instance.uiMgr.SetScrapNote(questNoteScrap);
        GameManager.instance.inputMgr.SetJoyStick(joyStick);
    }

    public void OnClickGrabButton()
    {
        gooseGrab.ChangeGrab();
    }
    public void OnClicksneckButton()
    {
        gooseCon.ChangeSneck();
    }
    public void OnClickwingButton()
    {
        gooseCon.ChangeWing();
    }
    public void OnClickrunButton()
    {
        gooseCon.ChangeRun();
    }

    public void OnClickQuestButton()
    {
        GameManager.instance.uiMgr.ShowQuestWindow();      
    }

    public void OnClickLeftArrow()
    {
        GameManager.instance.uiMgr.ClickLeftArrow();
    }
    public void OnClickRightArrow()
    {
        GameManager.instance.uiMgr.ClickRightArrow();
    }

    public void OnClickSaveButton()
    {
        GameManager.instance.uiMgr.OnClickSaveButton();
    }
}
