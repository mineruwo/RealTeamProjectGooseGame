using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHandlerTitle : MonoBehaviour
{
    public void OnClickGameSlot(int slotNum)
    {
        Debug.Log($"[UiManager] {slotNum}번 슬롯 누름");

        GameManager.instance.uiMgr.OnClickGameSlot(slotNum);
    }

    public void OnClickDeleteButton(int slotNum)
    {
        Debug.Log($"[UiManager] {slotNum} 지우기");
        GameManager.instance.uiMgr.OnClickDeleteButton(slotNum);
    }
}
