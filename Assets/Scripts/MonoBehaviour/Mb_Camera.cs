using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Camera : MonoBehaviour
{
    [SerializeField] Mb_Door[] trialToActivate;
    [SerializeField] Animator anim;
    [SerializeField] int timeBeforeReactivate;
    bool canSee = true;

    private void OnTriggerStay(Collider other)
    {
        if (canSee == true)
        {

           canSee = false;
           ActivateDoor();
            // anim.SetTrigger("DoThings");
            StartCoroutine("WaitBeforeReactivation");

        }
    }

    void ActivateDoor()
    {
        for (int i = 0; i < trialToActivate.Length; i++)
            trialToActivate[i].DoThings();
      
    }

    IEnumerator WaitBeforeReactivation()
    {
        yield return new WaitForSeconds(timeBeforeReactivate);
        ActivateDoor();
        canSee = true;
    }
}
