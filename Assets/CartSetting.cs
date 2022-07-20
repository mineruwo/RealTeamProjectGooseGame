using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CartSetting : MonoBehaviour
{
    private CinemachineDollyCart cart;

    private void Awake()
    {
        cart = GetComponentInChildren<CinemachineDollyCart>();

        cart.m_Position = 0;
    }

    public void Update()
    {
    }

}
