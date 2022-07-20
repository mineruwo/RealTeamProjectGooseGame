using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GardenerJob : MonoBehaviour
{
    //Stores a reference to the waypoint system this object will be
    [SerializeField] private Waypoints waypoints;

    private Gardener gardener;
    private Transform currentWaypoint;
    private NavMeshAgent agent;

    public bool isFinished = false;
    public bool equipped = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gardener = GetComponent<Gardener>();
    }

    //���������� ���� �� �� -> if ������ item ����� �� chase
    public void DoNothing()
    {
        //�� ��� �״�� Idle animation
    }

    public void Watering()
    {
        var pos = GameObject.Find("WateringPos1");
        agent.SetDestination(pos.transform.position);
        //�긦 ��� ������

        //���Ѹ��� �ִ� ��ҷ� ��
        //���Ѹ��� ��� �� �ִ� ������ ��
        //�� ��
        //���Ѹ��� ���� ��ҿ�

    }

    public void CaringPlants()
    {
        //���� ���� �ִ� �� ������ ��
        //if ���ڸ��� ������ ?? �ִϸ��̼�
        //�ٸ� �۾�
        //������ �� ��� �����ִ� ȭ�ܿ� ��
        //����
        //�� �ٽ� ���� �ڸ���

        DoNothing();
    }

    public void CarryingVase()
    {
        //�ٵ� ��� �ѹ� �ű�� ���ڸ��� ����������ϴϱ� . . .. ������Ʈ����...??
    }
}
