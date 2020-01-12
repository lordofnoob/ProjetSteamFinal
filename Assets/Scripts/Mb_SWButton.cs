using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_SWButton : Mb_Trial
{
    [Header("Trial Resolution")]
    [SerializeField] Mb_SpeedWalk[] allSpeedWalkToImpact;
    [Tooltip("0 pour eteindre , -1 pour inverser")] [SerializeField] float forceModifier=-1;

    public override void DoThings()
    {
        for (int i =0; i < allSpeedWalkToImpact.Length; i ++)
        {
            allSpeedWalkToImpact[i].Swap(forceModifier);
        }

        base.DoThings();
    }

}
