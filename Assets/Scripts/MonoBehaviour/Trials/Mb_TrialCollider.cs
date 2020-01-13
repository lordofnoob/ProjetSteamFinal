using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using DG.Tweening;

public class Mb_TrialCollider : MonoBehaviour
{
    [SerializeField] Mb_Trial trialAssociated;
    [HideInInspector] public Mb_PlayerControler currentUser;
    GamePadState controlerOfTheUser;
    [SerializeField] Transform PositionToLookAndPut;
    bool isAnItem = false;
    // List<Mb_PlayerControler> listOfUser;

    private void Start()
    {
        if (trialAssociated.GetComponent<Mb_Item>())
            isAnItem = true;
    }


    private void OnTriggerEnter (Collider other)
    {
        Mb_PlayerControler playerOccupying = other.GetComponent<Mb_PlayerControler>();
      
        if (playerOccupying != null)
        {
            trialAssociated.UiAppearence();

            if (currentUser == null)
            {
                playerOccupying.AddOverlapedTrial(trialAssociated);
                currentUser = playerOccupying;
                trialAssociated.AddUser(currentUser);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Mb_PlayerControler playerLeaving = other.GetComponent<Mb_PlayerControler>();

        if (currentUser == playerLeaving)
        {
            playerLeaving.RemoveOverlapedTrial(trialAssociated);

            trialAssociated.RemoveUser(playerLeaving);

            /*if (trialAssociated.listOfUser.Count == 0)
                trialAssociated.ResetAccomplishment();*/
            if (currentUser == null)
            {
                trialAssociated.ResetAccomplishment();
                trialAssociated.UiDisaparence();
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
        controlerOfTheUser = currentUser.controlerUsedState;
            if (controlerOfTheUser.Buttons.A == ButtonState.Pressed)
            {
                currentUser.transform.DORotate(new Vector3(0, Mathf.Atan2(PositionToLookAndPut.rotation.x, PositionToLookAndPut.rotation.z) * Mathf.Rad2Deg + 90,0), .5f);
                currentUser.transform.DOMove(new Vector3(PositionToLookAndPut.transform.position.x, currentUser.transform.position.y, PositionToLookAndPut.transform.position.z), 0.5f);

            }
        
    }
}
