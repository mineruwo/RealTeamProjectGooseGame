using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GardenerJob : MonoBehaviour
{
    public Job gardenerToDo;

    public bool isFinished = false;

    public Transform objectsPos;

    public List<GameObject> destinations = new List<GameObject>();

    //공통적으로 들어가야 할 것 -> if 거위가 item 뺏어갔을 시 chase
    public void DoNothing()
    {
        //그 장소 그대로 Idle animation
    }

    public void Watering()
    {
        Vector3 wateringCanPos = objectsPos.position;
        //얘를 어떻게 들고오지

        //물뿌리개 있는 장소로 감
        //물뿌리개 들고 꽃 있는 곳으로 감
        //물 줌
        //물뿌리개 원래 장소에

        DoNothing();
    }

    public void CaringPlants()
    {
        Vector3 shovelPos = objectsPos.position;
        //의자 위에 있는 삽 가지러 감
        //if 그자리에 없으면 ?? 애니메이션
        //다른 작업
        //있으면 삽 들고 새싹있는 화단에 감
        //삽질
        //삽 다시 원래 자리에

        DoNothing();
    }

    public void CarryingVase()
    {
        Vector3 vasePos = objectsPos.position;
        //근데 얘는 한번 옮기고 그자리를 곶어해줘야하니까 . . .. 업데이트에서...??
    }
}
