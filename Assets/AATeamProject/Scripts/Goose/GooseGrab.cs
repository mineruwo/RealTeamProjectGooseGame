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
        GrabObject();
    }

    public float force = 600;
    public float damping = 6;

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
                    Destroy(goose.GetComponent<HingeJoint>());
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
                            GameObject handle = grabObject.GetComponent<BigObject>().handlePoint[0];
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

                            var hingeJoint = goose.AddComponent<HingeJoint>();
                            hingeJoint.anchor = gooseMouse.transform.position;

                            hingeJoint.connectedBody = grabObjRb;

                            hingeJoint.connectedAnchor = handle.transform.position;
                            //�״��� ���⼭ ���� �ִ� �ڵ���̶� �Ȱ��� �ۼ��ϸ� ��?
                            //�� inverse kinematic �ִϸ��̼��� ����Ͽ� �θ��� handlePoint�� ��� �ؾ���.

                        }
                    }
                    break;
            }
        }
    }

    private JointDrive NewJointDrive(float force, float damping)
    {
        JointDrive drive = new JointDrive();
        drive.mode = JointDriveMode.Position;
        drive.positionSpring = force;
        drive.positionDamper = damping;
        drive.maximumForce = Mathf.Infinity;
        return drive;
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