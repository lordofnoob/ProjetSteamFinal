using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform itemToFollow;
    private Vector3 offset;

    private void Awake()
    {
       offset = transform.position - itemToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {

        Follow();

    }

    public void Follow()
    {
        transform.position = itemToFollow.position + offset;
    }
}
