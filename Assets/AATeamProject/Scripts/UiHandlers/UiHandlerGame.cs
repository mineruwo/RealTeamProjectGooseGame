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

    public void OnClickSaveButton()
    {
        GameManager.instance.uiMgr.OnClickSaveButton();
    }

    public void OnClickQuestButton()
    {
        Debug.Log($"[UiHandlerGame] 퀘스트 버튼 누름");

        // GameManager.instance.uiMgr.ClearNote(questNoteLists);


        GameManager.instance.uiMgr.OnClickQuestButton(questMenuUp, questMenuDown);
        GameManager.instance.uiMgr.WriteQuestNote(questNoteLists);

        
    }

    public void OnClickLeftArrow()
    {

    }

    public void NoteScrapeUp()
    {
        GameManager.instance.uiMgr.NoteScrapeUp(questNoteScrap);
        StartCoroutine(ScrapeDown());
    }

    //public void QuestClear(int num)
    //{
    //    Debug.Log($"[UiManager] ????????");


    //    var text = questNoteScrape.transform.GetChild(0);
    //    text.GetComponent<TextMeshProUGUI>().text = "<s>" + DataManager.currentQuestDataList[num].questName + "</s>";
    //    questNoteScrape.SetActive(true);
    //    StartCoroutine(ScrapeDown());
    //}

    public IEnumerator ScrapeDown()
    {
        yield return new WaitForSeconds(2.5f);
        questNoteScrap.SetActive(false);
    }

}
