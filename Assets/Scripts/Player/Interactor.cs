using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private List<Transform> nearbyGuns = new List<Transform>();
    private Transform nearestGun;

    private void FixedUpdate()
    {
        FindNearestGun();
        Debug.Log("Nearby gun count: " + nearbyGuns.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Add the gun to the nearby guns
        nearbyGuns.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide the gun's info panel if it is shown
        other.transform.GetChild(0).gameObject.SetActive(false);
        // Remove the gun from the nearby guns
        nearbyGuns.Remove(other.transform);
    }

    private void FindNearestGun()
    {
        // Do nothing if there are no nearby guns
        if (nearbyGuns.Count <= 0) { return; }

        float nearestDistance = float.MaxValue;
        
        // Loop through all of the nearby guns and find the nearest one
        for (int i = 0; i < nearbyGuns.Count; i++)
        {
            float distance = (nearbyGuns[i].position - transform.position).magnitude;

            if (distance < nearestDistance)
            {
                nearestGun = nearbyGuns[i];
                nearestDistance = distance;
            }
        }

        foreach (Transform gun in nearbyGuns)
        {
            if (gun == nearestGun)
            {
                gun.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gun.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
