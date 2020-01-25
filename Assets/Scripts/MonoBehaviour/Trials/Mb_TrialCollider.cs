using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using DG.Tweening;

public class Mb_TrialCollider : MonoBehaviour
{
    [SerializeField] Mb_Trial trialAssociated;
    GamePadState controlerOfTheUser;
    [SerializeField] Transform PositionToLookAndPut;
    bool isAnItem = false;
    public Mb_PlayerControler currentUser;
    // List<Mb_PlayerControler> listOfUser;

    private void Start()
    {
        if (trialAssociated.GetComponent<Mb_Item>())
            isAnItem = true;
        currentUser = null;
    }


    private void OnTriggerEnter (Collider other)
    {
        Mb_PlayerControler playerOccupying = other.GetComponent<Mb_PlayerControler>();
      
        if (playerOccupying != null)
        {
            if (currentUser == null )
            {
             
                playerOccupying.AddOverlapedTrial(trialAssociated);
                currentUser = playerOccupying;
                trialAssociated.AddUser(currentUser);
                if (currentUser.CurrentTrialsOverlaped[0] == trialAssociated )
                {
                    trialAssociated.UiAppearence();
                    trialAssociated.UiActivate();
                }
                if (currentUser.CurrentTrialsOverlaped.Count > 1)
                {
                    if (currentUser.CurrentTrialsOverlaped[1] == trialAssociated)
                    {
                        trialAssociated.UiAppearence();
                        trialAssociated.UiActivate();
                    }
                }
           
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Mb_PlayerControler playerLeaving = other.GetComponent<Mb_PlayerControler>();
        if (playerLeaving == true)
        {
            trialAssociated.RemoveUser(playerLeaving);
            playerLeaving.RemoveOverlapedTrial(trialAssociated);
        }

        if (currentUser == playerLeaving)
        {
            if (trialAssociated.listOfUser.Count==0)
            {
                trialAssociated.ResetAccomplishment();
                trialAssociated.UiDisaparence();
                trialAssociated.UiDisactivate();
            }

            currentUser = null;
        }
    }

    private void Update()
    {
        if (isAnItem == false && trialAssociated.CanInterract() && currentUser != null)
            SetupLookAndPosition();
    }

    public void SetupLookAndPosition()
    {
            if (currentUser.inputController.AButton == ButtonState.Pressed)
            {
                currentUser.transform.DOLookAt(new Vector3(trialAssociated.transform.position.x,currentUser.transform.position.y, trialAssociated.transform.position.z),.5f);
                currentUser.transform.DOMove(new Vector3(PositionToLookAndPut.transform.position.x, currentUser.transform.position.y, PositionToLookAndPut.transform.position.z), 0.5f);
            }
        
    }
}
