using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Mb_Trial : MonoBehaviour
{
    public Sc_TrialParameters trialRules;
    [HideInInspector] public float trialAccomplishment;
    public List<Mb_PlayerControler> listOfUser;
    public bool canInteract;

    [Header("UIPART")]
    [SerializeField] Image uiToFill;
    [SerializeField] Image uiToTrigger;
    [SerializeField] float appearenceTime = 0.5f;

    //trial Accomplishment
    public void EndTrial()
    {
        ResetAccomplishment();
        UiDisaparence();
        DoThings();
    }

    public virtual void DoThings()
    {

    }

    public void CheckBool()
    {
       // if 
    }

    public void AddAvancement(float accomplishmentToAdd)
    {
        trialAccomplishment += trialRules.accomplishmentToAdd;

        if (trialAccomplishment >= trialRules.accomplishmentNeeded)
        {
            EndTrial();

        }
        UpdateFillAmount(trialAccomplishment / trialRules.accomplishmentNeeded);
    }

    public void ResetAccomplishment()
    {
        trialAccomplishment = 0;
        UpdateFillAmount(trialAccomplishment);
        canInteract = false;
    }
    //user Gestion
    public void AddUser(Mb_PlayerControler playerToAdd)
    {
        if (listOfUser.Count == 0)
            UiAppearence();

        listOfUser.Add(playerToAdd);
        
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

   public void UpdateFillAmount(float fillAmount)
    {
        uiToFill.DOFillAmount(fillAmount,0.1f);
    }

   

}
