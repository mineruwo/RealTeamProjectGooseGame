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
    public PlayerController playerController;

    public GameObject handle;

    public Transform gooseHead;
    public Collider gooseHeadCollider;

    public bool isDrag = false;
    public static bool isGrap = false;

    public GameObject[] necks;
    public List<float> necksDistance;
    public float totalDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerController = goose.GetComponent<PlayerController>();
        for (int i = 0; i < necks.Length - 1; i++)
        {
            necksDistance.Add((necks[i].transform.position - necks[i + 1].transform.position).magnitude);
        }
       
        foreach(var distance in necksDistance)
        {
            totalDistance += distance;
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
                if (grabObject != null && !grabObject.GetComponent<PhysicObject>().isGrab) //
                {
                    grabObject.GetComponent<PhysicObject>().OnGrab(true);
                    grabObject.GetComponent<PhysicObject>().isGrab = true;
                    isDrag = true;
                    isGrap = true;
                    Rigidbody grabObjRb;

                    if (!grabObject.GetComponent<PhysicObject>().isHeavy)   //������ ������Ʈ ���� ��
                    {
                        grabObjRb = grabObject.GetComponent<SmallObject>().Rigidbody;

                        grabObjRb.transform.rotation = Quaternion.identity;
                        var rot = gooseMouse.transform.eulerAngles - grabObject.GetComponent<SmallObject>().handlePoint.transform.eulerAngles;

                        grabObjRb.transform.eulerAngles += rot;
                      
                        var pos = gooseMouse.position - grabObject.GetComponent<SmallObject>().handlePoint.transform.position;
                        grabObject.transform.position += pos;

                        if(grabObject.name == "keys")
                        {
                            GameManager.instance.questMgr.GetQuestId(3);
                        }

                        grabObjRb.useGravity = false;
                        grabObjRb.isKinematic = false;
                        grabObjRb.mass = 0;
                        grabObjRb.constraints = RigidbodyConstraints.FreezeAll;
                        grabObject.transform.SetParent(gooseMouse);
                    }
                    else if (grabObject.GetComponent<PhysicObject>().isHeavy)    //���ſ� ������Ʈ ���� ��
                    {
                        //gooseHeadCollider.isTrigger = true;
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

                        goose.transform.position += handle.transform.position - gooseMouse.transform.position;

                        var joint = goose.AddComponent<ConfigurableJoint>();
                        joint.connectedBody = grabObjRb;
                        joint.enableCollision = true;
                        joint.autoConfigureConnectedAnchor = false;

                        SoftJointLimit limit = joint.linearLimit;
                        limit.limit = 0.4f;
                        joint.linearLimit = limit;

                        joint.xMotion = ConfigurableJointMotion.Limited;
                        joint.yMotion = ConfigurableJointMotion.Limited;
                        joint.zMotion = ConfigurableJointMotion.Limited;

                        joint.connectedAnchor = grabObjRb.gameObject.transform.InverseTransformPoint(handle.transform.position);
                        joint.anchor = new Vector3(0, 5, 1);
                    }
                }
                break;
        }
    }

    public void Drop()
    {
        gooseHeadCollider.isTrigger = false;

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


    private void LateUpdate()
    {
        if (handle != null)
        {
            for(int i=0; i < necks.Length; i++)
            {
                Quaternion LookRotation = Quaternion.LookRotation(necks[i].transform.position - handle.transform.position, necks[i].transform.up) * Quaternion.Euler(new Vector3(0, -90, 0));
                necks[i].transform.rotation = LookRotation;
            }
            playerController.isSneck = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var obj = other.GetComponent<PhysicObject>();

        if ((obj == null) && !isGrap)
        {
            obj = other.GetComponentInParent<PhysicObject>();
        }

        if (obj && !isGrap)
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