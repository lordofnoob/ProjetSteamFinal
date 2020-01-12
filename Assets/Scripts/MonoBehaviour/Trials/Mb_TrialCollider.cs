using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_TrialCollider : MonoBehaviour
{
    [SerializeField] Mb_Trial trialAssociated;
    public Mb_PlayerControler currentUser;
   // List<Mb_PlayerControler> listOfUser;

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
}
