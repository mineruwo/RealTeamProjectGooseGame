using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotaion : MonoBehaviour
{

    public CinemachineClearShot clear;

    public bool ischeck = false;

    public void OnTriggerEnter(Collider other)
    {
        switch (ischeck && other.CompareTag("Goose"))
        {
            case true:
                clear.ChildCameras[0].Priority = 11;
                clear.ChildCameras[1].Priority = 9;
                break;
            case false:
                clear.ChildCameras[0].Priority = 9;
                clear.ChildCameras[1].Priority = 11;
                break;
        }

        ischeck = !ischeck;
    }
}
