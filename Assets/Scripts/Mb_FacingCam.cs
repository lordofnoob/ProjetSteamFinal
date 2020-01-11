using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_FacingCam : MonoBehaviour
{
    Vector3 lookAtPose;

    private void Start()
    {

        lookAtPose = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        transform.LookAt(lookAtPose);
    }
}

