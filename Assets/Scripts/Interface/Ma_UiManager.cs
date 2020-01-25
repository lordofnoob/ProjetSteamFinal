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
    [SerializeField] GameObject TutorialPanel;
    public Image TimeBar;
    [SerializeField] GameObject timeFeedBack;
    [SerializeField] Mb_Fade fadeBetweenScene;
    [SerializeField] GameObject[] portraits;
    public TextMeshProUGUI moneyText;

    float currentMoneyDisplay;
    float finalMoneyToDisplay = 0;
    bool haveTriggerStars, procked = false;
    int insecuredPlayer;
  

    private void Awake()

    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        timeFeedBack.SetActive(false);
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

    public void SetActivateTutorialPanel()
    {
        TutorialPanel.SetActive(true);
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

    public void UpdateTimeBar(float fillAmount)
    {
        if (TimeBar != null)
            TimeBar.fillAmount = fillAmount;
    }

    public void SetupEndMoney(float moneyAmount)
    {
        Mb_EndPannel.instance.objectiveText.text = Gamemanager.instance.levelParameters.moneyToSteal + " $";

        finalMoneyToDisplay = moneyAmount;
    }

    void SetupMoneyCount()
    {

        UpdateMoneyDisplay();

        if (currentMoneyDisplay < finalMoneyToDisplay)
        {
            if (finalMoneyToDisplay / 1000 > 1)
            {
                currentMoneyDisplay += 100 + Random.Range(3, 9);
            }
            else if (currentMoneyDisplay / 150 > 1)
            {
                currentMoneyDisplay += 10 + Random.Range(3, 9);
            }
            else
            {
                currentMoneyDisplay += 1;
            }
        }
        else if (currentMoneyDisplay > finalMoneyToDisplay)
        {
            haveTriggerStars = true;
            if (currentMoneyDisplay - finalMoneyToDisplay > 100)
            {
                currentMoneyDisplay -= 25 + Random.Range(3, 9);
            }
            else if (currentMoneyDisplay - finalMoneyToDisplay > 10)
            {
                currentMoneyDisplay -= 5;
            }
            else
            {
                currentMoneyDisplay -= 1;
            }
        }

        //  Mb_EndPannel.instance.moneySpot.text = currentMoneyDisplay + "$ Stolen";
        else
        {
            haveTriggerStars = true;
        }


    }

    public void FadeToLevel(int levelIndex)
    {
        fadeBetweenScene.FadeToLevel(levelIndex);
    }

    void UpdateMoneyDisplay()
    {
        Mb_EndPannel.instance.moneySpot.text = currentMoneyDisplay + "$ Stollen";
    }

    private void Update()
    {
        if (Gamemanager.instance.gameIsEnded)
            SetupMoneyCount();
    }

    public void UpdateMoney(float moneyAmount)
    {
        moneyText.text = moneyAmount + "$";
    }

    public void AppearTimeFeedBack()
    {
        timeFeedBack.SetActive(true);
    }

    public void UpdateNumberPlayerPortrait(int nbrPlayer)
    {
        for(int i = 0; i < portraits.Length; i++)
        {
            if(i < nbrPlayer)
            {
                portraits[i].SetActive(true);
            }
            else
            {
                portraits[i].SetActive(false);
            }
        }
    }
}
