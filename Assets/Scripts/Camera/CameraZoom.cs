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
                var len = vcam.m_Lens;
                len.OrthographicSize = Mathf.Lerp(len.OrthographicSize, 2.5f, 3f);
            }
            else
            {
                var len = vcam.m_Lens;
                len.OrthographicSize = Mathf.Lerp(len.OrthographicSize, 2f, 3f);
            }


             isZoom = !isZoom;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {


            if (isZoom)
            {
                var len = vcam.m_Lens;
                len.OrthographicSize = Mathf.Lerp(len.OrthographicSize, 1.5f, 3f);
            }
            else
            {
                var len = vcam.m_Lens;
                len.OrthographicSize = Mathf.Lerp(len.OrthographicSize, 2f, 3f);
            }

            isZoom = !isZoom;
        }
    }
}
