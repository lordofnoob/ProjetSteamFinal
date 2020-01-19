using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mb_DoorForce : MonoBehaviour
{

    public Rigidbody rb;
    public Vector3 force;
    void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(force);
    }
}
