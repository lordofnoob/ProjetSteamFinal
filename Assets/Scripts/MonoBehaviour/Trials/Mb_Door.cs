using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_Door : Mb_Trial
{
    [Header("TrialEffect")]
    [SerializeField] Transform[] doorToMove;
    [SerializeField] Transform[] desynchronisedDoor;
    [SerializeField] float yToAdd;
    [SerializeField] float timeToDo;
    List<Vector3> endPose = new List<Vector3>();
    List<Vector3> beginPose = new List<Vector3>();

    List<Vector3> endPoseDesynch = new List<Vector3>();
    List<Vector3> beginPoseDesynch = new List<Vector3>();

    bool open = false;

    private void Awake()
    {
        for (int i = 0; i < doorToMove.Length; i++)
        {
            endPose.Add(doorToMove[i].position+ new Vector3(0, yToAdd,0));
            beginPose.Add(doorToMove[i].position);
        }

        for (int i = 0; i < doorToMove.Length; i++)
        {
            endPoseDesynch.Add(desynchronisedDoor[i].position - new Vector3(0, yToAdd, 0));
            beginPoseDesynch.Add(desynchronisedDoor[i].position);
        }

    }
    public override void DoThings()
    {
    
        if (open == false)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
     

        base.DoThings();
    }

    public override bool CanInterract()
    {
        if (open == true)
            return false;
        return base.CanInterract();
    }

    public void OpenDoor()
    {
        for (int i = 0; i < doorToMove.Length; i++)
            doorToMove[i].DOMove(endPose[i], timeToDo);
        for (int i = 0; i < doorToMove.Length; i++)
            desynchronisedDoor[i].DOMove(beginPoseDesynch[i], timeToDo);
        open = !open;
    }
    public void CloseDoor()
    {
        for (int i = 0; i < doorToMove.Length; i++)
            doorToMove[i].DOMove(beginPoseDesynch[i], timeToDo);
        for (int i = 0; i < doorToMove.Length; i++)
            desynchronisedDoor[i].DOMove(endPoseDesynch[i], timeToDo);
        open = !open;
    }
}
