using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public GameObject clearUI; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Goose"))
        {
            clearUI.SetActive(true);
        }
    }
}
