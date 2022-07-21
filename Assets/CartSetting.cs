using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CartSetting : MonoBehaviour
{
    private CinemachineDollyCart cart;
    private Vector3 privPos;

    private void Awake()
    {
        cart = GetComponentInChildren<CinemachineDollyCart>();

        cart.m_Position = 0;

        this.privPos = gameObject.transform.position;
    }

    public void Update()
    {
        var curPos = gameObject.transform.position;

        if (privPos != curPos)
        {
            if (privPos.x > curPos.x)
            {
                cart.m_Position -= 0.0005f;

                if (cart.m_Position < 0)
                {
                    cart.m_Position = 0;
                }
            }
            else
            {
                cart.m_Position += 0.0005f;

                if (cart.m_Position > 1)
                {
                    cart.m_Position = 1;
                }
            }

            privPos = curPos;
        }

    }

}
