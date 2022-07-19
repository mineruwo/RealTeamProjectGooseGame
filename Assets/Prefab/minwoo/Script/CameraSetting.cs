using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetting : MonoBehaviour
{
    public CinemachineVirtualCamera vcam1;
    public CinemachineVirtualCamera vcam2;
    public CinemachineClearShot clearShot;

    private void Start()
    {
        setTarget();
    }

    public void Update()
    {
        if (vcam1.Follow == null || vcam2.Follow == null || clearShot.Follow == null)
        {
            setTarget();
        }
    }

    public void setTarget()
    {
        var player = GameObject.FindGameObjectWithTag("Goose");

        vcam1.Follow = player.transform;
        vcam2.Follow = player.transform;
        clearShot.Follow = player.transform;

        vcam1.LookAt = player.transform;
        vcam2.LookAt = player.transform;
        clearShot.LookAt = player.transform;

    }
}
