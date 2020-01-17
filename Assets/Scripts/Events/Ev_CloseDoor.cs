using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ev_CloseDoor : EventLd
{
   public override void DoThings()
    {
        for (int i = 0; i < Gamemanager.instance.doorToCloseOnEvent.Length; i++)
        {
            Gamemanager.instance.doorToCloseOnEvent[i].CloseDoor();
        }
        base.DoThings();
    }
}
