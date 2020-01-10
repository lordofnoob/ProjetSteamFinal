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

    public override void DoThings()
    {
        for (int i =0; i < doorToMove.Length; i++)
            doorToMove[i].DOMoveY(doorToMove[i].position.y + yToAdd, timeToDo);

        base.DoThings();
    }
}
