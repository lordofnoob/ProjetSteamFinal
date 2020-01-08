using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_TrialCollider : MonoBehaviour
{
    [SerializeField] Mb_Trial trialAssociated;
    Mb_PlayerControler currentUser;
    List<Mb_PlayerControler> listOfUser;

    private void OnTriggerStay (Collider other)
    {
        Mb_PlayerControler playerOccupying = other.GetComponent<Mb_PlayerControler>();

        if (currentUser ==null)
        {
            currentUser = playerOccupying;

            playerOccupying.AddOverlapedTrial(trialAssociated);

            trialAssociated.AddUser(currentUser);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentUser.GetComponent<Collider>())
        {
            Mb_PlayerControler playerOccupying = other.GetComponent<Mb_PlayerControler>();

            playerOccupying.RemoveOverlapedTrial(trialAssociated);

            trialAssociated.RemoveUser(currentUser);

            if (trialAssociated.listOfUser.Count == 0)
                trialAssociated.ResetAccomplishment();

            currentUser = null;
        }
    }
}
