using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ma_UiManager : MonoBehaviour
{
    [SerializeField] GameObject StartCanvas;
    [SerializeField] GameObject EndCanvas;
    [SerializeField] Image TimeBar;
    public TextMeshProUGUI timeRemainingText;
    public static Ma_UiManager instance;

    private void Awake()

    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void SetActiveEndCanvas()
    {
        EndCanvas.SetActive(true);
    }

    public void SetActiveStartCanvas()
    {
        StartCanvas.SetActive(true);
    }

    public void SetDesactiveStartCanvas()
    {
        StartCanvas.SetActive(false);
    }

    public void UpdateTimeRemainingText(float remainingTime)
    {
        float minuteRemaining, secondsRemaining;
        minuteRemaining = Mathf.Clamp(Mathf.FloorToInt(remainingTime / 60),0,9999999);
        secondsRemaining = Mathf.Clamp(Mathf.RoundToInt(remainingTime - minuteRemaining * 60), 0, 59);
        string timeSpentToDisplay;

        if (secondsRemaining > 10)
            timeSpentToDisplay = minuteRemaining + " : " + secondsRemaining;
        else
            timeSpentToDisplay = minuteRemaining + " : 0" + secondsRemaining;

        timeRemainingText.text = timeSpentToDisplay;
    }

   public  void UpdateTimeBar(float fillAmount)
    {
        TimeBar.fillAmount = fillAmount;
    }

    public void SetupEndPannel(float moneyAmount, bool firstObjective, bool secondObjectve, bool thirdObjective)
    {
        Mb_EndPannel.instance.moneySpot.text = moneyAmount + "$";
        Mb_EndPannel.instance.firstStar.gameObject.SetActive(firstObjective);
        Mb_EndPannel.instance.secondStar.gameObject.SetActive(secondObjectve);
        Mb_EndPannel.instance.thirdStar.gameObject.SetActive(thirdObjective);
    }

}
