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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gardener = GetComponent<Gardener>();
    }

    //���������� ���� �� �� -> if ������ item ����� �� chase

    public void Watering()
    {
        //���Ѹ��� �ִ� ��ҷ� ��
        var pos = GameObject.Find("WateringPos1");
        var pos2 = GameObject.Find("WateringPos2");

        gardener.Move();
        agent.SetDestination(pos.transform.position);

        var distance = Vector3.Distance(agent.transform.position, pos.transform.position);
        Debug.Log(distance);
        if (distance <= 0.1f)
        {
            gardener.Idle();
            Gardener.equipped = true;
        }

        //���Ѹ��� ��� �� �ִ� ������ ��

        if (Gardener.equipped)
        {
            gardener.Move();
            agent.SetDestination(pos2.transform.position);
            distance = Vector3.Distance(agent.transform.position, pos2.transform.position);

            if (distance <= 0.1f)
            {
                gardener.Idle();
                isFinished = true;
            }
        }
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
    }

    public void CarryingVase()
    {
        var pos = GameObject.Find("VasePos1");
        var pos2 = GameObject.Find("VasePos2");

        gardener.Move();
        agent.SetDestination(pos.transform.position);
        //�ٵ� ��� �ѹ� �ű�� ���ڸ��� ����������ϴϱ� . . .. ������Ʈ����...??
    }
}
