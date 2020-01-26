using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_Camera : MonoBehaviour
{
    [SerializeField] Mb_Door trialToActivate;
    [SerializeField] ParticleSystem particleToPop;
  //  bool canSee = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Mb_PlayerControler>())
        {
            particleToPop.transform.position = other.transform.position;
            particleToPop.Play();
            ResetDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Mb_PlayerControler>())
        {
            particleToPop.transform.position = other.transform.position;
            particleToPop.Play();
            ResetDoor();
        }
    }


    void ResetDoor()
    {
        trialToActivate.ResetDoor();
    }
}
