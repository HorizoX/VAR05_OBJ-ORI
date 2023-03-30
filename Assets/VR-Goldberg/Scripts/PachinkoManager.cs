using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PachinkoManager : MonoBehaviour
{
    // The material to be applied when the trigger is pressed
    public Material newMaterial;
    private List<Material> originalMaterials = new List<Material>();
    private InputDevice rightHandController;
    private bool spacebarDown = false;
    private bool triggerDown = false;


    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        foreach (InputDevice device in devices)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
            {
                rightHandController = device;
                break;
            }
        }
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Translucent");
        foreach (GameObject obj in objectsWithTag)
        {
            originalMaterials.Add(obj.GetComponent<Renderer>().material);
        }
    }

    void Update()
    {
        // Check if space bar is currently held down
        if (Input.GetKey(KeyCode.Space))
        {
            spacebarDown = true;
        }
        else
        {
            spacebarDown = false;
        }

        // Check if trigger button is currently pressed or released
        bool triggerPressed = false;
        if (rightHandController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0)
        {
            triggerPressed = true;
            if (!triggerDown)
            {
                triggerDown = true;
            }
        }
        else
        {
            if (triggerDown)
            {
                triggerDown = false;
            }
        }

        // Change materials based on whether space bar or trigger button is held down
        if (spacebarDown || triggerDown)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Translucent");
            foreach (GameObject obj in objectsWithTag)
            {
                obj.GetComponent<Renderer>().material = newMaterial;
            }
        }
        else
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Translucent");
            for (int i = 0; i < objectsWithTag.Length; i++)
            {
                objectsWithTag[i].GetComponent<Renderer>().material = originalMaterials[i];
            }
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}