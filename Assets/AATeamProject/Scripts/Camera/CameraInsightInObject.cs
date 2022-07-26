using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraInsightInObject : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;

    public int currMember = 0;

    private List<GameObject> viewTarget = new List<GameObject>();

    private void Start()
    {
        CheckToTarget();
    }

    private void Update()
    {
        foreach (GameObject go in viewTarget)
        {
            var insight = go.GetComponentInChildren<OnvisualCamera>().isView;   
            if (insight)
            {
                var check = targetGroup.FindMember(go.transform);

                if (check == -1)
                {
                    targetGroup.AddMember(go.transform, 1, 0);
                    currMember++;
                }
            }
            else
            {
                var check = targetGroup.FindMember(go.transform);

                if (check != -1)
                {
                    targetGroup.RemoveMember(go.transform);
                    currMember--;
                }
            }
        }

    }

    public void CheckToTarget()
    {
        viewTarget.Clear();

        var NPCList = GameObject.FindGameObjectsWithTag("Human");

        viewTarget.AddRange(NPCList);
    }
}
