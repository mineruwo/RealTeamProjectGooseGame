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
        GameManager.instance.uiMgr.SetTitleMenu(camera, cameraPoints, eraser,
            timeTexts, volState, cameraState);
        GameManager.instance.uiMgr.deleteEvent += UpdateSaveData;
        UpdateSaveData();

    }

    public void UpdateSaveData()
    {
        timeTexts[0].GetComponent<TextMeshPro>().text = GameManager.instance.dataMgr.saveFileDate.Stage1Time;
        timeTexts[1].GetComponent<TextMeshPro>().text = GameManager.instance.dataMgr.saveFileDate.Stage2Time;
        timeTexts[2].GetComponent<TextMeshPro>().text = GameManager.instance.dataMgr.saveFileDate.Stage3Time;
    }





}
