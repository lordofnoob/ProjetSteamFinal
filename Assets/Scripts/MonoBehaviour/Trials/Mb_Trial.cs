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
        ResetAccomplishment();
        UiDisaparence();
        DoThings();
    }

    //trial Result
    public virtual void DoThings()
    {

    }

    //Accomplissement
    public bool canInteract()
    {
        if (listOfUser.Count >= trialRules.numberOfPlayerNeeded)
            return true;
        else
            return true;
    }

    public void AddAvancement(float accomplishmentToAdd)
    {
        if (trialRules.trialType == TrialType.Time)
        {
            trialAccomplishment += trialRules.accomplishmentToAdd * Time.fixedDeltaTime;
        }
            
        else if (trialRules.trialType == TrialType.Mashing)
            trialAccomplishment += trialRules.accomplishmentToAdd;

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
        if (listOfUser.Count == 0)
            UiAppearence();

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
        uiToTrigger.transform.DOScaleY(0, appearenceTime);
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
