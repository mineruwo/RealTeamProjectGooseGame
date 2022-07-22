using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LayerMask layer;

    public Transform groundCastPoint;
    public float radius = 0.1f;
    public float checkDistance = 3f;

    private void FixedUpdate()
    {
      
    }

    void Update()
    {
        bool cast = Physics.SphereCast(groundCastPoint.position, radius, Vector3.down, out var hit, checkDistance, layer, QueryTriggerInteraction.Ignore);
        if (cast)
        {
            Debug.Log("Ground");
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(groundCastPoint.position, radius);
    //}
}
