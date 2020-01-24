using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using XInputDotNetPure;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public Sc_LevelParameters levelParameters;
    bool objectiveItem1, objectiveItem2, objectiveMoney;
    float moneyStolen;
    float timeRemaining;
    bool isPause;
    [HideInInspector] public Ma_InputController playerWhoPressedStart = null;

    [Header("Input Controllers")]
    public List<Ma_InputController> inputControllers = new List<Ma_InputController>();

    [Header("All Players")]
    public Mb_PlayerControler[] players;
    public bool LoadSkinAndMask = true;

    [Header("DoorEvent")]
    public Mb_Door[] doorToCloseOnEvent;

    [Header("LightEvent")]
    public Light[] allLightToSwitchOff;
    [SerializeField] float waitBeforeLight;

    [Header("WallToDeploy")]
    [SerializeField] Mb_Door[] wallToGetUp;

    [SerializeField] float timeSpentEvent1, timeSpentEvent2, timeSpentEvent3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        for(int i = 0; i < players.Length; i++)
        {
            inputControllers.Add(players[i].inputController);
        }

        timeRemaining = levelParameters.timeToDoTheLevel;
    }

    private void Start()
    {
        if (LoadSkinAndMask)
        {
            foreach (Mb_PlayerControler player in players)
            {
                player.GetComponent<Mb_LoadSkinAndMask>().LoadSkinAndMask();
            }
        }
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
        Ma_UiManager.instance.UpdateMoney(moneyStolen);
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
     //   Ma_UiManager.instance.SetupEndPannel(moneyStolen, objectiveItem1, objectiveItem2, objectiveMoney);
        Ma_UiManager.instance.SetActiveEndCanvas();
    }

    public void GamePause(Ma_InputController playerWhoPressedStart)
    {
        isPause = true;
        Time.timeScale = 0;
        this.playerWhoPressedStart = playerWhoPressedStart;
        Ma_UiManager.instance.SetActivePauseCanvas();
    }

    public void GameResume()
    {
        isPause = false;
        Time.timeScale = 1;
        Ma_UiManager.instance.SetDesActivePauseCanvas();
        playerWhoPressedStart = null;
    }

    public bool IsGamePause()
    {
        return isPause;
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
       int eventToTrigger = Random.Range(0, 2);

        if (eventToTrigger == 0)
            CloseDoor();
        else if (eventToTrigger == 1)
            LightOut();
    }

    void CloseDoor()
    {
        for (int i = 0; i < doorToCloseOnEvent.Length; i++)
        {
            doorToCloseOnEvent[i].CloseDoor();
        }
    }

    void WallDeclenchment()
    {
        for (int i = 0; i < wallToGetUp.Length; i++)
        {
            wallToGetUp[i].CloseDoor();
        }
    }
    //Light Event
    #region
    void LightOut()
    {
        for (int i =0; i <allLightToSwitchOff.Length; i++)
        {
            allLightToSwitchOff[i].DOIntensity(0, 2);
        }
        StartCoroutine("WaitBeforeLightOn");
    }

    void LightOn()
    {
        for (int i = 0; i < allLightToSwitchOff.Length; i++)
        {
            allLightToSwitchOff[i].DOIntensity(1, 2);
        } 
    }

    IEnumerator WaitBeforeLightOn()
    {
        yield return new WaitForSeconds(waitBeforeLight);
        LightOn();
    }
    #endregion
}

