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
    [SerializeField] Animator animatorAssociated;
    [SerializeField] Animator uiAnimator;
    [Header("UIPART")]
    [SerializeField] Image uiToFill;
    [SerializeField] TextMeshProUGUI textUser;
    public Image uiToTrigger;
    [SerializeField] float appearenceTime = 0.5f;

    //Timer before decay
    float timeBeforeDecay;

    private void FixedUpdate()
    {
        Decay();
    }


    //trial Result
    public virtual void DoThings()
    {
        if (animatorAssociated!= null)
            animatorAssociated.SetTrigger("DoThings");
        UiDisaparence();
        ResetAccomplishment();
    }

    //Accomplissement
    public virtual bool CanInterract()
    {
        bool interactionAvaible = true;

        if (trialRules.toolsNeeded.Length > 0 && listOfUser.Count >0)
        {
            if (listOfUser[0].itemHold != null && trialRules.toolsNeeded[0] == listOfUser[0].itemHold.itemType)
            {
                /*
                for (int i = 0; i < trialRules.toolsNeeded.Length; i++)
                {
                    for (int y = 0; y < listOfUser.Count; y++)
                        if (listOfUser[y].itemHold.itemType == trialRules.toolsNeeded[i])
                        {*/
                          //  print(trialRules.toolsNeeded[i]);
                            interactionAvaible = true;
                          //  break;
                /*        }
                }*/
            }
            else
            {
                uiAnimator.SetBool("WrongItem", true);
                        return false;
            }
            
        }

        //rajouter 1 parce que j update la lust que après avoir la condition ps: jui con
        if (listOfUser.Count < trialRules.numberOfPlayerNeeded)
            interactionAvaible = false;

        if (interactionAvaible == true)
            uiAnimator.SetBool("WrongItem", false);
        else
            uiAnimator.SetBool("WrongItem", true);

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
            DoThings();
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

    public void UiDisactivate()
    {
        uiToTrigger.gameObject.SetActive(false);
    }

    public void UiActivate()
    {
        uiToTrigger.gameObject.SetActive(true);
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
