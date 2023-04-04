using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> Devices = new List<InputDevice>();
        InputDevices.GetDevices(Devices);

        foreach (var item in Devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
