using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePhysics : MonoBehaviour
{
    // A flag to indicate whether physics are currently frozen or not
    private bool isPhysicsFrozen = false;

    // A list to store the Rigidbody components of all objects with the specified tag
    private List<Rigidbody> rigidbodies = new List<Rigidbody>();

    void Start()
    {
        // Find all the objects with the specified tag and get their Rigidbody components
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Balls");
        foreach (GameObject obj in objectsWithTag)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rigidbodies.Add(rb);
            }
        }
    }

    void Update()
    {
        // Freeze or resume physics on the press of the X key
        if (Input.GetKeyDown(KeyCode.X))
        {
            isPhysicsFrozen = !isPhysicsFrozen;
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = isPhysicsFrozen;
            }
        }
    }
}
