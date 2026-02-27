using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; // The static point of interest

    [Header("Dynamic")]
    public float camZ; //The desired Z pos of the camera

    void Awake()
    {
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        //A single line if statement doesn't require braces
        if (POI == null) return; // of there is no POI, then return

        //Get position of the poi
        Vector3 destination = POI.transform.position;
        //Force Destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        //Set the camera to the destination
        transform.position = destination;
    }
}
