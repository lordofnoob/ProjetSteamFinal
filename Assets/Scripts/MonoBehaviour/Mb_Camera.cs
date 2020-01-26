using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Camera : MonoBehaviour
{
    [SerializeField] Mb_Door trialToActivate;
  //  bool canSee = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Mb_PlayerControler>())
        ResetDoor();
    }


    void ResetDoor()
    {
        trialToActivate.ResetDoor();
    }
}
