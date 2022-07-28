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

    public GameObject handle;

    public Transform gooseHead;
    public Transform OriParent;

    public bool isDrag = false;
    public static bool isGrap = false;

    public GameObject[] necks;
    public List<Vector3> necksDistance;

    // Start is called before the first frame update
    void Start()
    {
        OriParent = gooseHead.parent;

        for (int i = 0; i < necks.Length - 1; i++)
        {
            necksDistance.Add(necks[i].transform.position - necks[i + 1].transform.position);
        }

        foreach(var j in necksDistance)
        {
            Debug.Log(j.magnitude);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GrabAndDropObject();
        }
        animator.SetBool("isDrag", isDrag);
        if (handle != null)
        {
            goose.transform.LookAt(handle.transform);
        }
    }

    public void ChangeGrab()
    {
        GrabAndDropObject();
    }

    public void GrabAndDropObject()
    {
        switch (isDrag)
        {
            case true:
                Drop();
                break;
            case false:
                if (grabObject != null) //
                {
                    grabObject.GetComponent<PhysicObject>().OnGrab(true);
                    grabObject.GetComponent<PhysicObject>().isGrab = true;
                    isDrag = true;
                    isGrap = true;
                    Rigidbody grabObjRb;

                    if (!grabObject.GetComponent<PhysicObject>().isHeavy)   //������ ������Ʈ ���� ��
                    {
                        grabObjRb = grabObject.GetComponent<SmallObject>().Rigidbody;

                        Debug.Log("Success Grab");

                        var rot = gooseMouse.transform.eulerAngles - grabObject.GetComponent<SmallObject>().handlePoint.transform.eulerAngles;

                        grabObjRb.transform.eulerAngles += rot;

                        var pos = gooseMouse.position - grabObject.GetComponent<SmallObject>().handlePoint.transform.position;
                        grabObject.transform.position += pos;

                        Debug.Log($"이름이름{grabObject.name}");

                        if(grabObject.name == "keys")
                        {
                            GameManager.instance.questMgr.GetQuestId(3);
                        }


                        grabObjRb.useGravity = false;
                        grabObjRb.isKinematic = false;
                        grabObjRb.mass = 0f;
                        grabObjRb.constraints = RigidbodyConstraints.FreezeAll;
                        grabObject.transform.SetParent(gooseMouse);
                    }
                    else if (grabObject.GetComponent<PhysicObject>().isHeavy)    //���ſ� ������Ʈ ���� ��
                    {
                        //gooseHead.transform.SetParent(goose.transform);

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

                        //Vector3 localPos = handle.transform.position - gooseMouse.transform.position;
                        //goose.transform.position += localPos;

                        //goose.transform.LookAt(handle.transform.position);

                        var joint = goose.AddComponent<ConfigurableJoint>();
                        joint.connectedBody = grabObjRb;
                        joint.enableCollision = false;
                        joint.autoConfigureConnectedAnchor = false;

                        SoftJointLimit limit = joint.linearLimit;
                        limit.limit = 0.5f;
                        joint.linearLimit = limit;

                        joint.xMotion = ConfigurableJointMotion.Limited;
                        joint.yMotion = ConfigurableJointMotion.Limited;
                        joint.zMotion = ConfigurableJointMotion.Limited;

                        // joint.connectedAnchor = handle.transform.position;
                        joint.connectedAnchor = grabObjRb.gameObject.transform.InverseTransformPoint(handle.transform.position);
                        //joint.anchor = goose.transform.InverseTransformPoint(gooseMouse.transform.position);
                        joint.anchor = new Vector3(0, 5, -2);
                    }
                }
                break;
        }
    }
    public void Drop()
    {
        isGrap = false;
        isDrag = false;
        if (grabObject)
        {
            grabObject.GetComponent<PhysicObject>().OnGrab(false);
            grabObject.GetComponent<PhysicObject>().isGrab = false;
            grabObject.transform.SetParent(null);
            grabObject = null;
        }
        //var joint = goose.GetComponent<HingeJoint>();
        Destroy(goose.GetComponent<ConfigurableJoint>());
        handle = null;
    }

    private void FixedUpdate()
    {
        var joint = goose.GetComponentInParent<ConfigurableJoint>();
        if (handle != null)
        {
            //goose.transform.LookAt(handle.transform);
        }
        if (joint)
        {
            var a = joint.connectedBody.gameObject.transform.position - transform.position;
        }
    }

    private void LateUpdate()
    {
        if (handle != null)
        {
            Vector3 targetVec3 = handle.transform.position;
            gooseHead.transform.position += handle.transform.position-gooseMouse.transform.position;

            
                //necks[0].transform.position += (gooseHead.transform.position - necks[0].transform.position) - necksDistance[0];
            //necks[0].transform.position = targetVec3;
            //necks[1].transform.position = (targetVec3 - necks[1].transform.position).normalized * necksDistance[0].magnitude;
            //for (int i = 1; i < necks.Length; i++)
            //{
            //    targetVec3 = necks[i - 1].transform.position;
            //    Vector3 dir = (targetVec3 - necks[i].transform.position).normalized;
            //    necks[i].transform.position += (dir * necksDistance[i].magnitude);


                //}

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