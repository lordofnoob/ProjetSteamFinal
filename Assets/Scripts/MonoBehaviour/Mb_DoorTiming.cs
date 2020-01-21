using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_DoorTiming : Mb_Door
{
    [SerializeField] float timeBeforeClose;
    [SerializeField] Animator animToTrigger;
    float timing = 0;
    bool counting = false;

    public override void FixedUpdate()
    {
        if (timing < timeBeforeClose && counting == true)
            timing += Time.fixedDeltaTime;
        else if (counting == true)
        {
            DoThings();
            timing = 0;
        }

        if (timing > timeBeforeClose - 5)
            animToTrigger.SetTrigger("DoThings");

    }

    public override void DoThings()
    {
        counting = !counting;
        base.DoThings();
    }

    public override void ResetParameters()
    {
        counting = false;
        timing = 0;
        base.ResetParameters();
    }
}
