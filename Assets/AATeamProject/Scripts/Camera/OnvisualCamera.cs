using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnvisualCamera : MonoBehaviour
{
    public bool isView = false;

    private void OnBecameVisible()
    {

        if (Camera.current == Camera.main)
        {
            isView = true;
            Debug.Log("Target in side : " + gameObject.name);
        }

    }
    private void OnBecameInvisible()
    {

        if (Camera.current == Camera.main)
        {
            isView = false;
            Debug.Log("Target out side" + gameObject.name);
        }

    }
    private void OnWillRenderObject()
    {
        if (Camera.current == Camera.main)
        {
            isView = true;
        }
        else
        {
            isView = false;
        }
    }

}
