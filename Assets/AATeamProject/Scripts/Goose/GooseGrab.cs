using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseGrab : MonoBehaviour
{
    public float grabRange = 3f;
    public Transform gooseMouse;
    public GameObject grabObject;
    public Rigidbody rb;
    private Collider collider;
    public GameObject goose;

    private bool isDrag = false;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isDrag)
            {
                isDrag = false;
                grabObject = null;
            }

            if (grabObject != null)
            {
                isDrag = true;
                Rigidbody grabObjRb = grabObject.GetComponent<Rigidbody>();
                grabObjRb.isKinematic = true;
                grabObjRb.useGravity = false;

                goose.AddComponent<FixedJoint>();
                var fixedJoint = goose.GetComponent<FixedJoint>();
                fixedJoint.connectedBody = grabObjRb;
                fixedJoint.connectedAnchor = gameObject.GetComponent<TestGrabHandle>().grabHandle.transform.position;

                // gooseMouse.transform.position = grabObject.GetComponent<TestGrabHandle>().grabHandle.transform.localPosition;

                //grabObject.transform.SetParent(gooseMouse);
            }
        }

        //collider.transform.position = gooseMouse.transform.position;
    }


    void GrabObject(GameObject grabObj)
    {
        if (grabObj.GetComponent<TestGrabHandle>())
        {
            Rigidbody grabObjRb = grabObj.GetComponent<Rigidbody>();
            grabObjRb.isKinematic = true;
            grabObjRb.useGravity = false;

            gooseMouse.transform.position = grabObj.GetComponent<TestGrabHandle>().transform.localPosition;

            grabObj.GetComponent<TestGrabHandle>().transform.SetParent(gooseMouse);


        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<TestGrabHandle>())
        {
            grabObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TestGrabHandle>() && !isDrag)
        {
            grabObject = null;
        }
    }

}