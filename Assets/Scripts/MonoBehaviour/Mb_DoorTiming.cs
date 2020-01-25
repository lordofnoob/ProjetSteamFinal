using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_DoorTiming : Mb_Trial
{
    [SerializeField] Mb_Door DoorTouched;
    [SerializeField] float timeBeforeReactivation;

    public override void Awake()
    {
        base.Awake();
    }

    public override bool CanInterract()
    {
        if (DoorTouched.counting == false && base.CanInterract())
            return true;
        else
            return false;
    }

    public override void DoThings()
    {
        if (DoorTouched.counting == false)
            DoorTouched.OpenTimingDoor(timeBeforeReactivation);
        base.DoThings();
    }
}
