using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGardenerWear : MonoBehaviour
{
    public GameObject pinPoint;

    void Start()
    {
        transform.parent = pinPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
