using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public Sc_LevelParameters levelParameters;
    float moneyStolen;
    float timeRemaining;
    bool isPause;
    bool objectiveItem1, objectiveItem2, objectiveMoney;


    public Mb_Door[] doorToCloseOnEvent;

    [SerializeField] float timeSpentEvent1, timeSpentEvent2, timeSpentEvent3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        timeRemaining = levelParameters.timeToDoTheLevel;
    }

       

    public void CheckItemToGet(Mb_Item itemStolen)
    {
        if (itemStolen.trialRules ==levelParameters.itemToGet1)
        {
            objectiveItem1 = true;
        }
        else if (itemStolen.trialRules == levelParameters.itemToGet2)
        {
            objectiveItem2 = true;
        }
    }

    public void AddMoney(int moneyToAdd)
    {
        moneyStolen += moneyToAdd;
        Ma_UiManager.instance.UpdateMoney(moneyToAdd);
        CheckMoney();
    }

    public void CheckMoney()
    {
        if (moneyStolen >= levelParameters.moneyToSteal)
        {
            objectiveMoney = true;
        }
    }

    private void Update()
    {
        if (isPause == false)
            DecreaseTimer();

    }

    void DecreaseTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            Ma_UiManager.instance.UpdateTimeRemainingText(timeRemaining);
            Ma_UiManager.instance.UpdateTimeBar(timeRemaining / levelParameters.timeToDoTheLevel);
            CheckEvent();
        }
        else
            EndGame();

    }

    void StartGame()
    {

    }

    void EndGame()
    {
        Ma_UiManager.instance.SetupEndPannel(moneyStolen, objectiveItem1, objectiveItem2, objectiveMoney);
        Ma_UiManager.instance.SetActiveEndCanvas();
    }

    public void PauseTimer()
    {
        isPause = true;
    }

    public void ResumeGame()
    {
        isPause = false;
    }

    void CheckEvent()
    {
        if (timeSpentEvent1 < timeRemaining)
        {
            if (timeSpentEvent2 < timeRemaining)
            {
                if (timeSpentEvent3< timeRemaining)
                {

                }
            }
        }
    }

    void RandomEvent()
    {
        Random.Range(0, 1);

    }

    void CloseDoor()
    {

    }

    void LightOut()
    {

    }
}

