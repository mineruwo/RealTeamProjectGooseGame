using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseGrab : MonoBehaviour
{
    public Animator animator;

    public Transform gooseMouse;
    public GameObject grabObject;
    public Rigidbody rb;
    public GameObject goose;

    private GameObject handle;

    public bool isDrag = false;
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GrabObject();
        //animator.SetBool("isDrag", isDrag);
    }

    public void ChangeGrab()
    {
        switch (isDrag)
            {
                case true:
                    isDrag = false;
                    grabObject.GetComponent<PhysicObject>().isGrab = false;
                    grabObject.transform.SetParent(null);
                    grabObject = null;

                    //var joint = goose.GetComponent<HingeJoint>();
                    Destroy(goose.GetComponent<ConfigurableJoint>());
                    handle = null;
                    break;
                case false:
                    if (grabObject != null) //
                    {
                        grabObject.GetComponent<PhysicObject>().isGrab = true;
                        isDrag = true;
                        Rigidbody grabObjRb;


                        if (!grabObject.GetComponent<PhysicObject>().isHeavy)   //������ ������Ʈ ���� ��
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
                        else if (grabObject.GetComponent<PhysicObject>().isHeavy)    //���ſ� ������Ʈ ���� ��
                        {
                            grabObjRb = grabObject.GetComponent<BigObject>().Rigidbody;
                            handle = grabObject.GetComponent<BigObject>().handlePoint[0];
                            Vector3 vec3 = gooseMouse.transform.position - handle.transform.position;
                            float distance = vec3.magnitude;

                            var handlePoints = grabObject.GetComponent<BigObject>().handlePoint;
                            foreach (var handlePoint in handlePoints)
                            {
                                //���⼭ �Ÿ� ª���� ����
                                var trans = gooseMouse.transform.position - handlePoint.transform.position;
                                if (trans.magnitude < distance)
                                {
                                    handle = handlePoint;
                                    distance = trans.magnitude;
                                }
                            }
                            var joint = goose.AddComponent<ConfigurableJoint>();
                            joint.connectedBody = grabObjRb;
                            joint.enableCollision = true;
                            joint.autoConfigureConnectedAnchor = false;

                            SoftJointLimit limit = joint.linearLimit;
                            limit.limit = 0.3f;
                            joint.linearLimit = limit;

                            joint.xMotion = ConfigurableJointMotion.Limited;
                            joint.yMotion = ConfigurableJointMotion.Limited;
                            joint.zMotion = ConfigurableJointMotion.Limited;

                            // joint.connectedAnchor = handle.transform.position;
                            joint.connectedAnchor = grabObjRb.gameObject.transform.InverseTransformPoint(handle.transform.position);
                            joint.anchor = goose.transform.InverseTransformPoint(gooseMouse.transform.position);
                        }
                    }
                    break;
            }
    }

    void GrabObject()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (isDrag)
            {
                case true:
                    isDrag = false;
                    grabObject.GetComponent<PhysicObject>().isGrab = false;
                    grabObject.transform.SetParent(null);
                    grabObject = null;

                    //var joint = goose.GetComponent<HingeJoint>();
                    Destroy(goose.GetComponent<ConfigurableJoint>());
                    handle = null;
                    break;
                case false:
                    if (grabObject != null) //
                    {
                        grabObject.GetComponent<PhysicObject>().isGrab = true;
                        isDrag = true;
                        Rigidbody grabObjRb;


                        if (!grabObject.GetComponent<PhysicObject>().isHeavy)   //������ ������Ʈ ���� ��
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
                        else if (grabObject.GetComponent<PhysicObject>().isHeavy)    //���ſ� ������Ʈ ���� ��
                        {
                            grabObjRb = grabObject.GetComponent<BigObject>().Rigidbody;
                            handle = grabObject.GetComponent<BigObject>().handlePoint[0];
                            Vector3 vec3 = gooseMouse.transform.position - handle.transform.position;
                            float distance = vec3.magnitude;

                            var handlePoints = grabObject.GetComponent<BigObject>().handlePoint;
                            foreach (var handlePoint in handlePoints)
                            {
                                //���⼭ �Ÿ� ª���� ����
                                var trans = gooseMouse.transform.position - handlePoint.transform.position;
                                if (trans.magnitude < distance)
                                {
                                    handle = handlePoint;
                                    distance = trans.magnitude;
                                }
                            }

                            var joint = goose.AddComponent<ConfigurableJoint>();
                            joint.connectedBody = grabObjRb;
                            joint.enableCollision = true;
                            joint.autoConfigureConnectedAnchor = false;

                            SoftJointLimit limit = joint.linearLimit;
                            limit.limit = 0.3f;
                            joint.linearLimit = limit;

                            joint.xMotion = ConfigurableJointMotion.Limited;
                            joint.yMotion = ConfigurableJointMotion.Limited;
                            joint.zMotion = ConfigurableJointMotion.Limited;

                            // joint.connectedAnchor = handle.transform.position;
                            joint.connectedAnchor = grabObjRb.gameObject.transform.InverseTransformPoint(handle.transform.position);
                            joint.anchor = goose.transform.InverseTransformPoint(gooseMouse.transform.position);
                        }
                    }
                    break;
            }
        }
    }




    private void FixedUpdate()
    {
        var joint = goose.GetComponentInParent<ConfigurableJoint>();
        if (handle != null)
        {
            goose.transform.LookAt(handle.transform);
        }
        if (joint)
        {
            var a = joint.connectedBody.gameObject.transform.position - transform.position;
            Debug.Log(a.magnitude);
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

        if (obj == null)
        {
            obj = other.GetComponentInParent<PhysicObject>();
        }

        if (obj && !isDrag)
        {
            grabObject = null;
        }
    }

    
}