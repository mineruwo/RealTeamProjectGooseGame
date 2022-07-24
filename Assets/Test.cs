using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Test : MonoBehaviour
{
    public Animator animator;

    public Transform target;

    public Transform mouse;

    private Vector3 lateMousePosition;
    private Vector3 lateTargetPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        mouse.position = target.position;
    }

    public void CalculateIK()
    {
        if(target == null)
        {
            return;
        }

       

    }

    

    private void OnAnimatorIK(int layerIndex)
    {

        animator.SetLookAtPosition(target.position);
        animator.SetLookAtWeight(1);    //가중치

        animator.SetIKPosition(AvatarIKGoal.RightHand, mouse.position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);    //가중치

        animator.SetIKRotation(AvatarIKGoal.RightHand, mouse.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
    }

}
