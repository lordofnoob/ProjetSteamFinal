using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_SpeedWalk : MonoBehaviour
{
    [SerializeField] float rollingForce;
    List<Mb_Speedable> toMove = new List<Mb_Speedable>();


    private void OnTriggerEnter(Collider other)
    {

        Mb_Speedable itemToMove = other.GetComponent<Mb_Speedable>();
        toMove.Add(itemToMove);
        if (itemToMove != null)
            itemToMove.strengthApplied += transform.forward * rollingForce;
    }

    private void OnTriggerExit(Collider other)
    {
        Mb_Speedable itemToStopMoving = other.GetComponent<Mb_Speedable>();
        toMove.Remove(itemToStopMoving);
        if (itemToStopMoving != null)
            itemToStopMoving.strengthApplied -= transform.forward * rollingForce;
    }

   public void Swap(float forceModifier)
    {
        for (int i = 0; i < toMove.Count; i++)
        {
            toMove[i].strengthApplied -=  transform.forward * rollingForce;
        }

        rollingForce *= forceModifier;

        for (int i =0; i < toMove.Count; i++)
        {
            print(transform.forward * 2 * rollingForce);
            toMove[i].strengthApplied += transform.forward *rollingForce;
        }
    }
}
