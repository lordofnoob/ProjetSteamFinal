using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using XInputDotNetPure;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public Sc_LevelParameters levelParameters;
    public bool objectiveItem1, objectiveItem2, objectiveMoney;
    public float moneyStolen= 0;
    float timeRemaining;
    bool isPause = false;
    [HideInInspector] public Ma_InputController playerWhoPressedStart = null;

    [Header("Launch Tutorial")]
    public bool activateTuto = true;

    [Header("Input Controllers")]
    [HideInInspector] public List<Ma_InputController> inputControllers = new List<Ma_InputController>();

    [Header("All Players")]
    public Mb_PlayerControler[] players;
    public bool LoadSkinAndMask = true;

    [Header("CameraEvent")]
    [SerializeField] float timeBeforeEvent;
    bool eventProcked = false;
    [SerializeField] GameObject[] collCameraToProck;
    [SerializeField] Animator[] animToProckCamera;

   [Header("DoorEvent")]
    public Mb_Door[] doorToCloseOnEvent;

    [Header("LightEvent")]
    public Light[] allLightToSwitchOff;
    [SerializeField] float waitBeforeLight;

    [Header("WallToDeploy")]
    [SerializeField] Mb_Door[] wallToGetUp;

    [Header("EndGameColl")]
    [SerializeField] GameObject endGameColl;
    
    [SerializeField] float timeSpentEvent1, timeSpentEvent2, timeSpentEvent3;
    [HideInInspector] public bool gameIsEnded, canEscape=false;
    int securisedPlayer=0;
    public static int numberOfPlayer = 1;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

       // endGameColl.SetActive(false);

        for (int i = 0; i < players.Length; i++)
        {
            inputControllers.Add(players[i].inputController);
            if(i >= numberOfPlayer)
            {
                players[i].gameObject.SetActive(false);
            }
            else
            {
                players[i].gameObject.SetActive(true);
            }
        }

        timeRemaining = levelParameters.timeToDoTheLevel;

        //Begin Tutorial
        isPause = true;
        if (activateTuto)
        {
            Ma_UiManager.instance.SetActivateTutorialPanel();
        }
        else
        {
            Ma_UiManager.instance.countDown.LaunchCountdown();
        }
    }

    private void Start()
    {
        if (LoadSkinAndMask)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if(players[i].gameObject.activeInHierarchy)
                    players[i].GetComponent<Mb_LoadSkinAndMask>().LoadSkinAndMask();
            }
        }
        Ma_UiManager.instance.UpdateNumberPlayerPortrait(numberOfPlayer);
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
        moneyStolen= Mathf.Clamp(moneyStolen,0, 100000000000);
        Ma_UiManager.instance.UpdateMoney(moneyStolen);
        CheckMoney();
    }

    public void CheckMoney()
    {
        if (moneyStolen >= levelParameters.moneyToSteal)
        {
            objectiveMoney = true;
        }
        else
            objectiveMoney = false;
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

            if (timeRemaining < levelParameters.timeToDoTheLevel - timeBeforeEvent && eventProcked == false)
            {
                eventProcked = true;
                StartCoroutine("CameraActivation");
             
            }

            if (timeRemaining < 15)
            {
                canEscape = true;
                endGameColl.SetActive(true);
          
                Ma_UiManager.instance.AppearTimeFeedBack();
                Ma_UiManager.instance.TimeBar.DOColor(Color.red, 1);
            }
        }
        else if (gameIsEnded == false)
        {
            EndGame();
        }

    }

    public void StartGame()
    {
        isPause = false;
        Ma_UiManager.instance.inGameCanvas.gameObject.SetActive(true);
    }

    void EndGame()
    {
        if (gameIsEnded== false)
        {
            Mb_EndPannel.instance.bestScoreSpot.text = levelParameters.bestScore + "$";
            StartCoroutine("StarApparition");
            gameIsEnded = true;
            
            Ma_UiManager.instance.SetupEndMoney(moneyStolen);
            Ma_UiManager.instance.SetActiveEndCanvas();
        }
  
        //rajouter le truc pour le nombre d'échapper
  
    }

    IEnumerator StarApparition()
    {
        int objectiveCompleted = 0;
        if (objectiveMoney)
            objectiveCompleted++;
        if (objectiveItem1)
            objectiveCompleted++;
        if (objectiveItem2)
            objectiveCompleted++;

        Mb_EndPannel.instance.bestScoreSpot.text = levelParameters.bestScore+ "$";

        yield return new WaitForSecondsRealtime(5);
        Mb_EndPannel.instance.escapedPlayer.text = (numberOfPlayer - securisedPlayer).ToString();

        yield return new WaitForSecondsRealtime(1);
        {
            AddMoney((numberOfPlayer - securisedPlayer) * -2000);
            Ma_UiManager.instance.SetupEndMoney(moneyStolen);
            CheckMoney();
        }

        yield return new WaitForSecondsRealtime(3);
        if (objectiveMoney == true)
        {

            Mb_EndPannel.instance.firstStar.gameObject.SetActive(true);
            Mb_EndPannel.instance.firstStar.SetTrigger("DoThings");
        }

        yield return new WaitForSecondsRealtime(1);
        if (objectiveItem1 == true)
        {
            Mb_EndPannel.instance.secondStar.gameObject.SetActive(true);
            Mb_EndPannel.instance.secondStar.SetTrigger("DoThings");
        }
          

        yield return new WaitForSecondsRealtime(1);
        if (objectiveItem2 == true)
        {
            Mb_EndPannel.instance.thirdStar.gameObject.SetActive(true);
            Mb_EndPannel.instance.thirdStar.SetTrigger("DoThings");
        }

        yield return new WaitForSecondsRealtime(1);
        {
            print("IsRenseigned"+Mb_EndPannel.instance.appreciation);
            if (moneyStolen < 3000)
            {
                Mb_EndPannel.instance.appreciation.text = "Was this even worth it?";
            }
            else if (moneyStolen < 3000 && objectiveCompleted >= 1)
            {
                Mb_EndPannel.instance.appreciation.text = "At least you got something.";
            }
            else if (moneyStolen>=3000 && objectiveCompleted ==0)
            {
                Mb_EndPannel.instance.appreciation.text = "Well that's something.";
            }
            else if (moneyStolen >= 3000 && objectiveCompleted < 1)
            {
                Mb_EndPannel.instance.appreciation.text = "Nice money.";
            }
            else if (moneyStolen >= 3000 && objectiveCompleted >= 1)
            {
                Mb_EndPannel.instance.appreciation.text = "Nice loot!";
            }
            else if (moneyStolen >= 6000 && objectiveCompleted == 2)
            {
                Mb_EndPannel.instance.appreciation.text = "Great job!";
            }
            else if (moneyStolen >= 6000 && objectiveCompleted == 3)
            {
                Mb_EndPannel.instance.appreciation.text = "Perfect Heist!";
            }
            
        }

        yield return new WaitForSecondsRealtime(2);
        if (moneyStolen > levelParameters.bestScore)
        {
            levelParameters.bestScore = moneyStolen;
            Mb_EndPannel.instance.bestScoreSpot.text = moneyStolen + "$";
            Mb_EndPannel.instance.animationBestScore.SetTrigger("DoThings");
        }
     
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

    public void SetGamePause(bool isPause)
    {
        this.isPause = isPause;
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

    void ColliderActivation()
    {
        endGameColl.SetActive(true);
    }

    public void addSecuredPlayer()
    {
        securisedPlayer += 1;
    }

    public void removeSecuredPlayer()
    {
        securisedPlayer -= 1;
    }

    IEnumerator CameraActivation()
    {
        for (int i = 0; i < collCameraToProck.Length; i++)
        {
            animToProckCamera[i].SetTrigger("DoThings");
        }
        yield return new WaitForSeconds(5);
        for (int i = 0; i < collCameraToProck.Length; i++)
        {
            collCameraToProck[i].SetActive(true);
            animToProckCamera[i].SetTrigger("DoThings");
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

