using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Camera : MonoBehaviour
{
    [SerializeField] Mb_Door trialToActivate;
    [SerializeField] Animator anim;
    [SerializeField] int timeBeforeReactivate;
    bool canSee = true;

    private void OnTriggerStay(Collider other)
    {
        if (canSee == true)
        {
            ResetDoor();
        }
    }


    void ResetDoor()
    {
        trialToActivate.ResetDoor();
    }
}
