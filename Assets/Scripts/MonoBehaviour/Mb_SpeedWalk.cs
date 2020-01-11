using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_SpeedWalk : MonoBehaviour
{
    List<Rigidbody> allItemInterracted = new List<Rigidbody>();
    [SerializeField] float rollingForce;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody bodyToAdd = other.GetComponent<Rigidbody>();
        allItemInterracted.Add(bodyToAdd);
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody bodyToRemove = other.GetComponent<Rigidbody>();
        allItemInterracted.Remove(bodyToRemove);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < allItemInterracted.Count; i++)
        {
            allItemInterracted[i].transform.position += transform.forward * rollingForce * Time.fixedDeltaTime;
        }
    }
}
