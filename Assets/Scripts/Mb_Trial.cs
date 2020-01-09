using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Mb_Trial : MonoBehaviour
{
    public Sc_TrialParameters trialRules;
    [HideInInspector] public float trialAccomplishment;
    public List<Mb_PlayerControler> listOfUser;
   

    [Header("UIPART")]
    [SerializeField] Image uiToFill;
    [SerializeField] TextMeshProUGUI textUser;
    [SerializeField] Image uiToTrigger;
    [SerializeField] float appearenceTime = 0.5f;
    bool beingUsed;
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

    
    public bool canInteract()
    {
        if (listOfUser.Count >= trialRules.numberOfPlayerNeeded)
            return true;
        else
            return false;
    }

    public void AddAvancement(float accomplishmentToAdd)
    {
        if (trialRules.trialType == TrialType.Time)
            trialAccomplishment += trialRules.accomplishmentToAdd * Time.fixedDeltaTime;
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

   void UpdateUserCount()
    {
        textUser.text = listOfUser.Count + " / " + trialRules.numberOfPlayerNeeded;
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

   public void Decay()
    {
        timeBeforeDecay += Time.fixedDeltaTime;

        if (timeBeforeDecay >= trialRules.timeToWaitBeforeDecay)
        {
            trialAccomplishment -= trialRules.decaying * Time.fixedDeltaTime;
            UpdateFillAmount();
        }
    }

}
