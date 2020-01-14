using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_CameraFollowPlayer : MonoBehaviour
{

    public Vector3 rotation;
    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {

        Follow();

    }

    public void Follow()
    {
        myTransform.rotation = Quaternion.Euler(rotation);
    }
}
