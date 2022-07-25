using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineClearShot clear;

    public CinemachineVirtualCamera vcam;

    public CinemachineGroupComposer aim;

    public float magnification = 2f;

    public bool isZoom = false;
    

    public void Update()
    {
        foreach (var cam in clear.ChildCameras)
        {
            if (cam.Priority == 11)
            {
                vcam = (CinemachineVirtualCamera)cam;

                aim = vcam.GetCinemachineComponent<CinemachineGroupComposer>();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {


            if (isZoom)
            {
                aim.m_MinimumOrthoSize = Mathf.Lerp(2f, 2.5f, 10f);

            }
            else
            {
                aim.m_MinimumOrthoSize = 2f;
            }


             isZoom = !isZoom;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {


            if (isZoom)
            {
                aim.m_MinimumOrthoSize = 1.5f;
            }
            else
            {
                aim.m_MinimumOrthoSize = 2f;
            }

            isZoom = !isZoom;
        }
    }
}
