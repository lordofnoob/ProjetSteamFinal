using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_Door : Mb_Trial
{
    [Header("DoorToOpen")]
    public bool isTimingDoor;
    private float timeBeforeReactivation;
    public Transform[] doorToMove;
    public Transform[] desynchronisedDoor;
    [SerializeField] float yToAdd;
    [SerializeField] float timeToDo;

    [Header("FeedBacksAssociated")]
    [SerializeField] Animator[] animToTrigger;


    List<Vector3> endPose = new List<Vector3>();
    List<Vector3> beginPose = new List<Vector3>();

    List<Vector3> endPoseDesynch = new List<Vector3>();
    List<Vector3> beginPoseDesynch = new List<Vector3>();

    float timing = 0;
    bool open, canTrigger = false;  
    public bool counting, isOpen = false;

    public override void Awake()
    {
        for (int i = 0; i < doorToMove.Length; i++)
        {
            endPose.Add(doorToMove[i].position+ new Vector3(0, yToAdd,0));
            beginPose.Add(doorToMove[i].position);
        }
        if (desynchronisedDoor.Length >0)
            for (int i = 0; i < doorToMove.Length; i++)
            {
                print(desynchronisedDoor[i].position - new Vector3(0, yToAdd, 0));
                endPoseDesynch.Add(desynchronisedDoor[i].position - new Vector3(0, yToAdd, 0));
                beginPoseDesynch.Add(desynchronisedDoor[i].position);
            }
        base.Awake();

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

        if (desynchronisedDoor.Length > 0)
            for (int i = 0; i < doorToMove.Length; i++)
            {
                desynchronisedDoor[i].DOMove(endPoseDesynch[i], timeToDo);
            }
        open = !open;
    }

    public void CloseDoor()
    {
        for (int i = 0; i < doorToMove.Length; i++)
            doorToMove[i].DOMove(beginPose[i], timeToDo);

        if (desynchronisedDoor.Length > 0)
            for (int i = 0; i < doorToMove.Length; i++)
                desynchronisedDoor[i].DOMove(beginPoseDesynch[i], timeToDo);

        open = !open;
    }

    public virtual void ResetParameters()
    {

    }

    public void OpenTimingDoor(float timeBeforeClose)
    {
        DoThings();
        if (open == true)
        {
            open = true;
            timeBeforeReactivation = timeBeforeClose;
            canTrigger = true;
            counting = true;
            timing = 0;
        }
    }

    public override void FixedUpdate()
    {
        if (timing < timeBeforeReactivation && open == true)
        {
            timing += Time.fixedDeltaTime;
        }

        else if (open == true)
        {
            counting = false;
           DoThings();
           open = false;
        }

        if (timing > timeBeforeReactivation - 5 && canTrigger == true)
        {
            for (int i = 0; i < animToTrigger.Length; i++)
            {
                animToTrigger[i].SetTrigger("DoThings");
            }
            
            canTrigger = false;
        }

    }

    public void ResetDoor()
    {
        if (open == true)
        {
            for (int i = 0; i < doorToMove.Length; i++)
            {
                timing = 0;
                counting = false;
                CloseDoor();
                open = false;
            }
        }
    }
}
