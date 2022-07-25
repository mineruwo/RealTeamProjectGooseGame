using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiHandlerTitle : MonoBehaviour
{
    public Camera camera;

    public GameObject[] cameraPoints;
    public int pointNum = 1;
    public int distance = 20;

    public GameObject eraser;
    private Vector3 eraserDPosition;
    private bool isGetEraser = false;
    private bool isDelete = false;
    private int slotNum;

    public GameObject[] timeTexts;

    public void Start()
    {
        eraserDPosition = eraser.transform.position;
        UpdateSaveData();
    }


    public void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isGetEraser)
        {
            Debug.Log($"[UiHandlerTitle] ���콺 ����");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.name == "Start")
                {
                    Debug.Log($"[UiHandlerTitle] ��ŸƮ ��ư ����");
                    Debug.Log($"[UiHandlerTitle] ���� ������Ʈ{hit.transform.gameObject}");
                    pointNum = 0;
                }

                if (hit.transform.gameObject.name == "arrow")
                {
                    Debug.Log($"[UiHandlerTitle] ���ư��� ����");
                    Debug.Log($"[UiHandlerTitle] ���� ������Ʈ{hit.transform.gameObject}");
                    pointNum = 1;
                }

                if (hit.transform.gameObject.name == "savebook_yellow")
                {
                    Debug.Log($"[UiManager] 1�� ���� ����");
                    GameManager.instance.uiMgr.OnClickGameSlot(1);
                }

                if (hit.transform.gameObject.name == "savebook_green")
                {
                    Debug.Log($"[UiManager] 2�� ���� ����");
                    GameManager.instance.uiMgr.OnClickGameSlot(2);
                }

                if (hit.transform.gameObject.name == "savebook_red")
                {
                    Debug.Log($"[UiManager] 3�� ���� ����");
                    GameManager.instance.uiMgr.OnClickGameSlot(3);
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
                    //hit.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, hit.transform.position.z);
                }
            }

        }

        if(Input.GetMouseButtonUp(0))
        {
            slotNum = eraser.GetComponent<Eraser>().slotNum;
            if(slotNum == 1)
            {
                GameManager.instance.dataMgr.saveFileDate.Stage1Time = "�������";
                Debug.Log($"[UiHandlerTitle] �ؽ�Ʈ ��������Ȯ��{GameManager.instance.dataMgr.saveFileDate.Stage1Time}");

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
        Debug.Log($"[UiHandlerTitle] ���ӿ�����Ʈã��?{timeTexts[0]}");
        Debug.Log($"[UiHandlerTitle] �ؽ�Ʈ ã��?{timeTexts[0].GetComponent<TextMeshPro>().text}");
        timeTexts[0].GetComponent<TextMeshPro>().text = GameManager.instance.dataMgr.saveFileDate.Stage1Time;
        timeTexts[1].GetComponent<TextMeshPro>().text = GameManager.instance.dataMgr.saveFileDate.Stage2Time;
        timeTexts[2].GetComponent<TextMeshPro>().text = GameManager.instance.dataMgr.saveFileDate.Stage3Time;
    }

    //public void OnTriggerStay(Collider other)
    //{
    //    Debug.Log($"[UiManager] ���찳�� �浹����");
    //    if (other.gameObject.name == "savebook_yellow" && isDelete)
    //    {
    //        Debug.Log($"[UiManager] 1�� ���� ����");
    //        GameManager.instance.dataMgr.DeleteQuestData(1);
    //        isDelete = false;
    //    }
    //}

    //public float radius;

    //public GameObject effectPrefab;
    //public float effectDuration;

    //public void Fire(GameObject caster, Vector3 position, int layerMask)
    //{
    //    Debug.Log("[CircleRangeSkill] ???? ??? ???");
    //    if (caster == null)
    //        return;

    //    var go = Instantiate(effectPrefab, position, effectPrefab.transform.rotation);
    //    Destroy(go, effectDuration);

    //    var cols = Physics.OverlapSphere(position, radius);

    //    foreach (var col in cols)
    //    {
    //        if (col.gameObject == caster)
    //            continue;

    //        //if (col.gameObject.layer == layerMask)
    //        //    Debug.Log($"[CircleRangeSkill] ?????? ??? {col.name}");
    //        //    continue;

    //        var attackables = col.GetComponentsInChildren<IAttackable>();

    //        if (attackables.Length == 0)
    //            continue;

    //        var aStates = caster.GetComponent<LivingEntity>();
    //        //var dStates = caster.GetComponent<LivingEntity>();

    //        var attack = CreateAttack(aStates);
    //        foreach (var attackable in attackables)
    //        {
    //            attackable.OnAttack(caster, attack);
    //        }
    //    }
    //}





    public void OnClickGameSlot(int slotNum)
    {
        Debug.Log($"[UiManager] {slotNum}�� ���� ����");

        GameManager.instance.uiMgr.OnClickGameSlot(slotNum);
    }

    public void OnClickDeleteButton(int slotNum)
    {
        Debug.Log($"[UiManager] {slotNum} �����");
        GameManager.instance.uiMgr.OnClickDeleteButton(slotNum);
    }

    public void GameStart()
    {

    }


}
