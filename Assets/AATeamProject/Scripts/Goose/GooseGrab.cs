using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseGrab : MonoBehaviour
{
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
       // rb = GetComponent<Rigidbody>();
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
                if (!grabObject.GetComponent<PhysicObject>().isHeavy)
                {
                    Debug.Log("Success Grab");
                    
                    var pos = gooseMouse.position - grabObject.GetComponent<SmallObject>().handlePoint.transform.position;
                    grabObject.transform.position += pos;
                    //grabObject.transform.rotation = gooseMouse.transform.rotation;//grabObject.GetComponent<SmallObject>().handlePoint.transform.rotation;
                    var rot = gooseMouse.transform.eulerAngles - grabObject.GetComponent<SmallObject>().handlePoint.transform.eulerAngles;
                    
                    grabObjRb.transform.eulerAngles += rot;

                    grabObjRb.useGravity = false;
                    grabObjRb.isKinematic = false;
                    grabObjRb.mass = 0f;
                    grabObjRb.constraints = RigidbodyConstraints.FreezeAll;
                    grabObject.transform.SetParent(gooseMouse);
                    
                   // var fixedJoint = grabObject.AddComponent<FixedJoint>();
                     //fixedJoint.connectedBody = rb;
                    // fixedJoint.connectedAnchor = gameObject.GetComponent<SmallObject>().handlePoint.transform.position;
                }
                

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
        if (other.GetComponent<PhysicObject>())
        {
            grabObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PhysicObject>() && !isDrag)
        {
            grabObject = null;
        }
    }

}