using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Mb_Trial : MonoBehaviour
{
    //trials parameters
    [Header("Trial Parameters")]
    public Sc_TrialParameters trialRules;
    public float trialAccomplishment;
    public List<Mb_PlayerControler> listOfUser;
    public animationInteractionType animationType;

    [Header("UIPART")]
    [SerializeField] Image uiToFill;
    [SerializeField] TextMeshProUGUI textUser;
    [SerializeField] Image uiToTrigger;
    [SerializeField] float appearenceTime = 0.5f;

    //Timer before decay
    float timeBeforeDecay;

    private void FixedUpdate()
    {
        Decay();
    }

    //trial Accomplishment
    public void EndTrial()
    {
     
        DoThings();
    }

    //trial Result
    public virtual void DoThings()
    {
        UiDisaparence();
        ResetAccomplishment();
    }

    //Accomplissement
    public virtual bool CanInterract()
    {
        bool interactionAvaible = true;

        if (trialRules.toolsNeeded.Length > 0 && listOfUser.Count >0)
        {
            if (listOfUser[0].itemHold != null)
            {
                for (int i = 0; i < trialRules.toolsNeeded.Length; i++)
                {
                    for (int y = 0; y < listOfUser.Count; y++)
                        if (listOfUser[y].itemHold.itemType == trialRules.toolsNeeded[i])
                        {
                            interactionAvaible = true;
                            break;
                        }
                }
            }
            else
                return false;
        }
  
       //rajouter 1 parce que j update la lust que après avoir la condition ps: jui con
        if (listOfUser.Count +1 < trialRules.numberOfPlayerNeeded)
            interactionAvaible = false;
 

        return interactionAvaible;
    }

    public void AddAvancement(float accomplishmentToAdd)
    {
        if (trialRules.trialType == TrialType.Time)
        {
            trialAccomplishment += trialRules.accomplishmentToAdd * Time.fixedDeltaTime;

        }

        else if (trialRules.trialType == TrialType.Mashing)
        {
            trialAccomplishment += trialRules.accomplishmentToAdd;
        }

        if (trialAccomplishment >= trialRules.accomplishmentNeeded)
        {
            EndTrial();
        }

        UpdateFillAmount();

        timeBeforeDecay = 0;
    }

    public void ResetAccomplishment()
    {
        trialAccomplishment = 0;
        UpdateFillAmount(); 
    }

    public void Decay()
    {
        //si l accomplissement est superieur a zero on tente de le decay si le decay est ready
        timeBeforeDecay += Time.fixedDeltaTime;
        if (trialAccomplishment >= 0)
        {
            if (timeBeforeDecay >= trialRules.timeToWaitBeforeDecay)
            {
                trialAccomplishment -= trialRules.decaying * Time.fixedDeltaTime;
                UpdateFillAmount();
            }

            trialAccomplishment -= trialRules.forceDecay * Time.fixedDeltaTime;
        }
    }

    //user Gestion
    public void AddUser(Mb_PlayerControler playerToAdd)
    {
        //   if (listOfUser.Count == 0)


        listOfUser.Add(playerToAdd);
        UpdateUserCount();
    }

    public void RemoveUser(Mb_PlayerControler playerToRemove)
    {
        listOfUser.Remove(playerToRemove);
        if (listOfUser.Count == 0)
        {
            UiDisaparence();
        }
    }

    //UIFUNCTIONS
    public void UiAppearence()
    {
         uiToTrigger.transform.DOScaleY(1, appearenceTime);
    }

    public void UiDisaparence()
    {
        uiToTrigger.transform.DOScaleY(0, 0);
    }

    public void UpdateFillAmount()
    {
        float fillAmount = trialAccomplishment / trialRules.accomplishmentNeeded;
        uiToFill.DOFillAmount(fillAmount,0.1f);
    }

    void UpdateUserCount()
    {
        textUser.text = listOfUser.Count + " / " + trialRules.numberOfPlayerNeeded;
    }

}

[System.Serializable]
public enum animationInteractionType
{
    Button, InteractionClassic, Hacking, PickUp
}
