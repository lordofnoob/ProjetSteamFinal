using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_Door : Mb_Trial
{
    [Header("TrialEffect")]
    [SerializeField] Transform[] doorToMove;
    [SerializeField] float yToAdd;
    [SerializeField] float timeToDo;
    [SerializeField] bool reapeatable;
    bool interacted = false;

    public override void DoThings()
    {
        for (int i =0; i < doorToMove.Length; i++)
            doorToMove[i].DOMoveY(doorToMove[i].position.y + yToAdd, timeToDo);

        if (reapeatable == false)
            interacted = true;

        base.DoThings();
    }

    public override bool CanInterract()
    {
        if (interacted == true)
            return false;
        return base.CanInterract();
    }

}
