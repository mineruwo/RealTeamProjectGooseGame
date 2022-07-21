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
            switch(isDrag)
            {
                case true:
                    isDrag = false;

                    //grabObject의 rigidBody를 원래 상태로 되돌려야함.
                    //grabObject를 자식오브젝트로 배치한거 풀어야함.

                    grabObject.transform.SetParent(null);

                    grabObject = null;  
                    break;
                case false:
                    if (grabObject != null) //
                    {
                        isDrag = true;
                        Rigidbody grabObjRb;

                        if (!grabObject.GetComponent<PhysicObject>().isHeavy)   //가벼운 오브젝트 잡을 때
                        {
                            grabObjRb = grabObject.GetComponent<SmallObject>().Rigidbody;
                            Debug.Log("Success Grab");

                            var rot = gooseMouse.transform.eulerAngles - grabObject.GetComponent<SmallObject>().handlePoint.transform.eulerAngles;

                            grabObjRb.transform.eulerAngles += rot;

                            var pos = gooseMouse.position - grabObject.GetComponent<SmallObject>().handlePoint.transform.position;
                            grabObject.transform.position += pos;

                            grabObjRb.useGravity = false;
                            grabObjRb.isKinematic = false;
                            grabObjRb.mass = 0f;
                            grabObjRb.constraints = RigidbodyConstraints.FreezeAll;
                            grabObject.transform.SetParent(gooseMouse);
                        }
                        else if(grabObject.GetComponent<PhysicObject>().isHeavy)    //무거운 오브젝트 잡을 때
                        {
                            grabObjRb = grabObject.GetComponent<BigObject>().Rigidbody;
                            GameObject handle = grabObject.GetComponent<BigObject>().handlePoint[0];
                            Vector3 vec3 = gooseMouse.transform.position - handle.transform.position;
                            float distance = vec3.magnitude;

                            var handlePoints = grabObject.GetComponent<BigObject>().handlePoint;
                            foreach(var handlePoint in handlePoints)
                            {
                                //여기서 거리 짧은애 구함
                                var trans = gooseMouse.transform.position - handlePoint.transform.position;
                                if(trans.magnitude < distance)
                                {
                                    handle = handlePoint;
                                    distance = trans.magnitude;
                                }
                            }

                            var hingeJoint = goose.AddComponent<HingeJoint>();
                            hingeJoint.anchor = gooseMouse.transform.position;

                            hingeJoint.connectedBody = grabObjRb;

                            hingeJoint.connectedAnchor = handle.transform.position;
                            //그다음 여기서 위에 있는 코드들이랑 똑같이 작성하면 끝?
                            //단 inverse kinematic 애니메이션을 사용하여 부리가 handlePoint에 닿게 해야함.
                            
                        }
                    }
                    break;
            }

           

           
        }
       

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
        var obj = other.GetComponent<PhysicObject>();

        if (obj == null)
        {
            obj = other.GetComponentInParent<PhysicObject>();
        }

        if (obj)
        {
            grabObject = obj.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.GetComponent<PhysicObject>();

        if(obj == null)
        {
            obj = other.GetComponentInParent<PhysicObject>();
        }

        if (obj && !isDrag)
        {
            grabObject = null;
        }
    }

}