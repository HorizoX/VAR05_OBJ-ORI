using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.InputSystem.XR;

public class VrRig : MonoBehaviour
{
    public Transform head, left, right;

    private void Awake()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();

        Debug.Log(XRGeneralSettings.Instance.Manager.activeLoader);
    }

    private void Update()
    {
        if (XRController.leftHand != null)
        {
            Vector3 leftPosition = XRController.leftHand.devicePosition.ReadValue();
            Quaternion leftRotation = XRController.leftHand.deviceRotation.ReadValue();
            left.SetPositionAndRotation(leftPosition, leftRotation);
        }

        if (XRController.rightHand != null)
        {
            Vector3 rightPosition = XRController.rightHand.devicePosition.ReadValue();
            Quaternion rightRotation = XRController.rightHand.deviceRotation.ReadValue();
            left.SetPositionAndRotation(rightPosition, rightRotation);
        }

        //if (XRHMD != null)
        //{

        //}
    }





    // Start is called before the first frame update
    void Start()
    {

    }

}
