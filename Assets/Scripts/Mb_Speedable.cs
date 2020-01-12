using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Speedable : MonoBehaviour
{
    public Vector3 strengthApplied;
    Mb_Item itemAttachedTo;
    Rigidbody body;
    
    private void Start()
    {
       

        if (!GetComponent<Mb_PlayerControler>())
        {
            body = GetComponent<Rigidbody>();
            itemAttachedTo = GetComponent<Mb_Item>();
        }
    }

    private void FixedUpdate()
    {
        if (body != null && itemAttachedTo.thrown == false)
        {
            body.velocity = new Vector3(strengthApplied.x, body.velocity.y, strengthApplied.z) ;
        }
    }

    public void ResetStrenghApplied()
    {
        strengthApplied = Vector3.zero;
    }
}
