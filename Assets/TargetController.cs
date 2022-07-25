using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Animations.Rigging;

public class TargetController : MonoBehaviour
{
    public GooseGrab gooseGrab;

    public Transform target;
    public Rig headRig;

    public Transform goose;

    public List<SmallObject> smallTargets;
    public List<BigObject> bigTargets;

    public List<GameObject> targetsTransform;


    public float maxAngle = 100f;
    void Start()
    {
        smallTargets = FindObjectsOfType<SmallObject>().ToList();
        bigTargets = FindObjectsOfType<BigObject>().ToList();

        foreach(var smallTarget in smallTargets)
        {
            if (smallTarget.handlePoint != null)
            {
                targetsTransform.Add(smallTarget.handlePoint);
            }
        }

        foreach(var bigTarget in bigTargets)
        {
            foreach(var bTarget in bigTarget.handlePoint)
            {
                if (bTarget != null)
                {
                    targetsTransform.Add(bTarget);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = 1.5f;
        Transform track = null;
        if (!gooseGrab.isDrag)
        {
            foreach (var target in targetsTransform)
            {
                Vector3 vec3 = target.transform.position - goose.position;
                if (vec3.magnitude < distance)
                {
                    float angle = Vector3.Angle(transform.forward, vec3);
                    if (angle < maxAngle)
                    {
                        track = target.transform;
                        break;
                    }
                }
            }
        }
        float weight = 0;

        if (track != null && !gooseGrab.isDrag)
        {
            target.position = track.position;
            weight = 1;
        }

        headRig.weight = weight;
    }
}
