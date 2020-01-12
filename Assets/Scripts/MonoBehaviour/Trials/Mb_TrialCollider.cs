using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using DG.Tweening;

public class Mb_TrialCollider : MonoBehaviour
{
    [SerializeField] Mb_Trial trialAssociated;
    public Mb_PlayerControler currentUser;
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
        Mb_PlayerControler playerOccupying = other.GetComponent<Mb_PlayerControler>();

        if (other == currentUser.GetComponent<Collider>() )
        {
            playerOccupying.RemoveOverlapedTrial(trialAssociated);

            trialAssociated.RemoveUser(currentUser);

            /*if (trialAssociated.listOfUser.Count == 0)
                trialAssociated.ResetAccomplishment();*/
            if (currentUser == null)
                trialAssociated.ResetAccomplishment();

            currentUser = null;
        }
    }

    private void Update()
    {
        if (isAnItem == false)
            SetupLookAndPosition();
    }

    public void SetupLookAndPosition()
    {
        if (currentUser != null)
        {
            controlerOfTheUser = currentUser.controlerUsedState;
            if (controlerOfTheUser.Buttons.A == ButtonState.Pressed)
            {
                currentUser.transform.DORotate(new Vector3(0, Mathf.Atan2(PositionToLookAndPut.rotation.x, PositionToLookAndPut.rotation.z) * Mathf.Rad2Deg + 90,0), .5f);
                currentUser.transform.DOMove(PositionToLookAndPut.transform.position, 0.5f);

            }
        }
    }
}
