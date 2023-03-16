using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Management;
using UnityEngine.InputSystem.XR;
using UnityEngine;

public class ColorChangeObject : MonoBehaviour
{

    bool canChangecolor = false;

    private void Update()
    {
        if (XRController.leftHand != null)
        {

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Renderer>().material.color = Color.red;
    }
}
