using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Ma_UiManager : MonoBehaviour
{
    public static Ma_UiManager instance;

    public Canvas inGameCanvas;
    [SerializeField] GameObject StartCanvas;
    [SerializeField] GameObject PauseCanvas;
    [SerializeField] GameObject EndCanvas;
    [SerializeField] Image TimeBar;
    public TextMeshProUGUI moneyText;
  
    float currentMoneyDisplay;
    float finalMoneyToDisplay = 0;
    bool haveTriggerStars = false;


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

    public void SetActivePauseCanvas()
    {
        PauseCanvas.SetActive(true);
        Mb_PauseMenu pauseMenu = PauseCanvas.GetComponent<Mb_PauseMenu>();
        pauseMenu.SetActiveButton(pauseMenu.cursorSpots[0]);
        pauseMenu.inThisMenu = true;
        pauseMenu.inputController = Gamemanager.instance.playerWhoPressedStart;
    }

    public void SetDesActivePauseCanvas()
    {
        PauseCanvas.SetActive(false);
        Mb_PauseMenu pauseMenu = PauseCanvas.GetComponent<Mb_PauseMenu>();
        pauseMenu.SetActiveButton(null);
        pauseMenu.inThisMenu = false;
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
        if(TimeBar != null)
            TimeBar.fillAmount = fillAmount;
    }

    public void SetupEndPannel(float moneyAmount, bool firstObjective, bool secondObjectve, bool thirdObjective, int numberOfFailToEscape)
    {
        Mb_EndPannel.instance.objectiveText.text = Gamemanager.instance.levelParameters.moneyToSteal + " $";
        finalMoneyToDisplay = moneyAmount;

        

   
    }

    void SetupMoneyCount()
    {
        if (currentMoneyDisplay < finalMoneyToDisplay)
        {
            if (finalMoneyToDisplay / 1000 > 1)
            {
                currentMoneyDisplay += 1000 + Random.Range(3, 9);
            }
            else if (currentMoneyDisplay / 100 > 1)
            {
                currentMoneyDisplay += 100 + Random.Range(3, 9);
            }
            else
            {
                currentMoneyDisplay += 1;
            }
        }
        else
            haveTriggerStars = true;


        Mb_EndPannel.instance.moneySpot.text = currentMoneyDisplay + "$ Stolen";
    }

    void SetupStars()
    {

    }

    private void Update()
    {
        if (haveTriggerStars == false)
            SetupMoneyCount();
        
        if (currentMoneyDisplay >= finalMoneyToDisplay && haveTriggerStars == false)
            SetupStars();
    }

    public void UpdateMoney(float moneyAmount)
    {
        moneyText.text = moneyAmount.ToString();
        print("moneyAmount");
    }

}
