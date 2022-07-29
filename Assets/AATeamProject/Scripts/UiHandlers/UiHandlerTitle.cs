using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiHandlerTitle : MonoBehaviour
{
    public Camera camera;

    public GameObject[] cameraPoints;
    private int pointNum = 1;
    private int distance = 280;

    public GameObject eraser;
    private Vector3 eraserDPosition;
    private bool isGetEraser = false;
    private bool isDelete = false;
    private int slotNum;

    public GameObject[] timeTexts;
    public GameObject volState;
    public GameObject cameraState;

    private int volNum = 5;
    private bool isLookGoose = true;

    public void Start()
    {
        eraserDPosition = eraser.transform.position;
        UpdateSaveData();
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isGetEraser)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.name == "Start")
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

                if(hit.transform.gameObject.name == "vol_arrowL")
                {
                    volNum--;
                    volState.GetComponent<TextMeshPro>().text = volNum.ToString();
                }
                if (hit.transform.gameObject.name == "vol_arrowR")
                {
                    volNum++;
                    volState.GetComponent<TextMeshPro>().text = volNum.ToString();
                }

                if (hit.transform.gameObject.name == "ca_arrowL")
                {
                    isLookGoose = !isLookGoose;
                    if(isLookGoose)
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
        camera.transform.position = Vector3.Lerp(camera.transform.position, cameraPoints[pointNum].transform.position, Time.deltaTime * 2f);

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

        if(Input.GetMouseButtonUp(0))
        {
            slotNum = eraser.GetComponent<Eraser>().slotNum;
            if(slotNum == 1)
            {
                GameManager.instance.dataMgr.saveFileDate.Stage1Time = "비어있음";
            }
            if (slotNum == 2)
            {
                GameManager.instance.dataMgr.saveFileDate.Stage2Time = "비어있음";
            }
            if (slotNum == 3)
            {
                GameManager.instance.dataMgr.saveFileDate.Stage3Time = "비어있음";
            }
            GameManager.instance.uiMgr.OnClickDeleteButton(slotNum);
            UpdateSaveData();
            isGetEraser = false;
            isDelete = true;
            eraser.transform.position = eraserDPosition;
        }

    }

    public void UpdateSaveData()
    {
        timeTexts[0].GetComponent<TextMeshPro>().text = GameManager.instance.dataMgr.saveFileDate.Stage1Time;
        timeTexts[1].GetComponent<TextMeshPro>().text = GameManager.instance.dataMgr.saveFileDate.Stage2Time;
        timeTexts[2].GetComponent<TextMeshPro>().text = GameManager.instance.dataMgr.saveFileDate.Stage3Time;
    }

    public void OnClickGameSlot(int slotNum) // 안쓸듯
    {
        GameManager.instance.uiMgr.OnClickGameSlot(slotNum);
    }

    public void OnClickDeleteButton(int slotNum) // 안쓸듯
    {
        GameManager.instance.uiMgr.OnClickDeleteButton(slotNum);
    }


    // 옵션



}
