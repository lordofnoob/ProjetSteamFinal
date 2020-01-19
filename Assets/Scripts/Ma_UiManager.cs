using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ma_UiManager : MonoBehaviour
{
    public Canvas inGameCanvas;
    [SerializeField] GameObject StartCanvas;
    [SerializeField] GameObject EndCanvas;
    [SerializeField] Image TimeBar;
    public TextMeshProUGUI moneyText;
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
        inGameCanvas.gameObject.SetActive(false);
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
        minuteRemaining = Mathf.Clamp(Mathf.FloorToInt(remainingTime / 60), 0, 9999999);
        secondsRemaining = Mathf.Clamp(Mathf.RoundToInt(remainingTime - minuteRemaining * 60), 0, 59);
        string timeSpentToDisplay;

        if (secondsRemaining > 10)
            timeSpentToDisplay = minuteRemaining + " : " + secondsRemaining;
        else
            timeSpentToDisplay = minuteRemaining + " : 0" + secondsRemaining;
    }

   public  void UpdateTimeBar(float fillAmount)
    {
        TimeBar.fillAmount = fillAmount;
    }

    public void SetupEndPannel(float moneyAmount, bool firstObjective, bool secondObjectve, bool thirdObjective)
    {
        Mb_EndPannel.instance.objectiveText.text = "Steal at least" + Gamemanager.instance.levelParameters.moneyToSteal + " $";
        Mb_EndPannel.instance.moneySpot.text = moneyAmount + "$ Stolen";
        Mb_EndPannel.instance.firstStar.gameObject.SetActive(firstObjective);
        Mb_EndPannel.instance.secondStar.gameObject.SetActive(secondObjectve);
        Mb_EndPannel.instance.thirdStar.gameObject.SetActive(thirdObjective);
    }

    public void UpdateMoney(float moneyAmount)
    {
        moneyText.text = moneyAmount.ToString();
        print("moneyAmount");
    }

}
