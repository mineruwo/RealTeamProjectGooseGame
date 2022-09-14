using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    public bool isGruop = false;
    public CinemachineClearShot clearShot;
    public GameObject Group;
    private GameObject goose;

    private void Awake()
    {
        goose = GameObject.FindGameObjectWithTag("Goose");
        clearShot = transform.GetComponentInChildren<CinemachineClearShot>();
        Group = transform.GetComponentInChildren<CinemachineTargetGroup>().gameObject;
    }

    private void Update()
    {
        switch (isGruop)
        {
            case true:
                Group.SetActive(true);
                clearShot.Follow = Group.transform;
                clearShot.LookAt = Group.transform;

                foreach (var cam in clearShot.ChildCameras)
                {
                    cam.Follow = Group.transform;
                    cam.LookAt = Group.transform;
                }
                break;

            case false:

                Group.SetActive(false);
                clearShot.Follow = goose.transform;
                clearShot.LookAt = goose.transform;

                foreach (var cam in clearShot.ChildCameras)
                {
                    cam.Follow = goose.transform;
                    cam.LookAt = goose.transform;
                }
                break;

        }
    }

}
