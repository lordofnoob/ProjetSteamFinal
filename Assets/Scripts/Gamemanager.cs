using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    [SerializeField] Sc_LevelParameters levelParameters;
    float moneyStolen;
    float timeRemaining;
    bool isPause;
    bool objectiveItem1, objectiveItem2, objectiveMoney;

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
        }
        else
            EndGame();

    }

    void StartGame()
    {

    }

    void EndGame()
    {

    }

    public void PauseTimer()
    {
        isPause = true;
    }

    public void ResumeGame()
    {
        isPause = false;
    }

}
