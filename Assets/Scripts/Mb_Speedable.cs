using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Speedable : MonoBehaviour
{
    public Vector3 strengthApplied;
    Rigidbody body;
    
    private void Start()
    {
        if (!GetComponent<Mb_PlayerControler>())
        {
            body = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        if (body != null)
            body.velocity = strengthApplied;
    }
}
