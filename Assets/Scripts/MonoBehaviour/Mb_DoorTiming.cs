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

    public override void DoThings()
    {
        DoorTouched.OpenTimingDoor(timeBeforeReactivation);
        base.DoThings();
    }


}
