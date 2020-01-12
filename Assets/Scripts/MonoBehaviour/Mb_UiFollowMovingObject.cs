using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_UiFollowMovingObject : MonoBehaviour
{
    [SerializeField] Transform transformToFollow;
    Vector3 offset;

    private void Awake()
    {
        offset = transform.position - transformToFollow.position;
    }

    private void Update()
    {
        transform.position = new Vector3(transformToFollow.position.x, transformToFollow.position.y, transformToFollow.position.z) + offset;
    }
}
 