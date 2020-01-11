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
        if (itemStolen==levelParameters.itemToGet1)
        {

        }
        else if (itemStolen == levelParameters.itemToGet2)
        {

        }
    }

    public void AddMoney(int moneyToAdd)
    {
        moneyStolen += moneyToAdd;
    }

    public void CheckMoney()
    {
        if (moneyStolen> levelParameters.moneyToSteal)
        {

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
