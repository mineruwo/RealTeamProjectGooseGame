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
        }

    }
    private void OnBecameInvisible()
    {

        if (Camera.current == Camera.main)
        {
            isView = false;
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
