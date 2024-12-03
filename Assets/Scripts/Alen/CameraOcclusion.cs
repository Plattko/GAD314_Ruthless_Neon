using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOcclusion : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask objectsToIgnore;
    [SerializeField] private Camera mainCam;

    //This has been optimised and set at 3 any lower and and the raycast doesnt ignore soon enough 
    //any higher and the ray shoots too far and then cuts of building infront
    [Header("Variables")]
    [SerializeField][Range(0f, 200f)] private float raycastDistance = 100f;

    private void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (mainCam == null || player == null) 
        {
            return;
        }

        Vector3 directionToPlayer = player.position - mainCam.transform.position;
        Ray ray = new Ray(mainCam.transform.position, directionToPlayer);

        //The code for the raycast setting it to ignore anything that the ray hits in the layermask
        if(Physics.Raycast(ray, out RaycastHit hit, raycastDistance, objectsToIgnore))
        {
            //This hides the render of the objects hit that are in the Occlusion layer
            Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        else
        {
            RestoreObject();
        }
    }

    private void RestoreObject()
    {
        //finds objects that are being ignored and if they are being ignored restores them
        foreach(GameObject obj in FindObjectsOfType<GameObject>())
        {
            if(((1 << obj.layer) & objectsToIgnore) != 0)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if(renderer != null)
                {
                    renderer.enabled = true;
                }
            }
        }
    }

    //Debugging gizmo for checking the ray and where it is hitting. 
    private void OnDrawGizmos()
    {
        if (mainCam == null || player == null)
        {
            return;
        }

        Gizmos.color = Color.blue;

        Vector3 directionToPlayer = player.position - mainCam.transform .position;

        Gizmos.DrawLine(mainCam.transform.position, mainCam.transform.position + directionToPlayer);
    }
}
