﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_UiFollowMovingObject : MonoBehaviour
{
    [SerializeField] Transform transformToFollow;
    Vector3 offset;

    private void Awake()
    {
        offset = transform.position - transformToFollow.transform.position;
    }

    private void Update()
    {
        transform.position = transform.position + offset;
    }
}
 