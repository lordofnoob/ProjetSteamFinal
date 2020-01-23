using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_DoorTiming : Mb_Door
{
    [SerializeField] float timeBeforeClose;
    [SerializeField] Animator feedBackTime;
    float timing = 0;
    bool counting = false;
    bool canTrigger=true;

    public override bool CanInterract()
    {
        if (counting == true)
            return false;
        return base.CanInterract();
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void FixedUpdate()
    {
        if (timing < timeBeforeClose && counting == true)
        {
            timing += Time.fixedDeltaTime;
        }

        else if (counting == true)
        {
            DoThings();
            canTrigger = true;
            timing = 0;
        }

        if (timing > timeBeforeClose - 5 && canTrigger == true)
        {
            feedBackTime.SetTrigger("DoThings");
            canTrigger = false;
        }

    }

    public override void UiAppearence()
    {
        if (counting == false)
        {
            base.UiAppearence();
        }
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
