using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class Mb_GamepadManagerMenu : MonoBehaviour
{
    //[Header("TEST & DEBUG")]
    //public List<Mb_LoadSkinAndMask> players;

    public static Mb_GamepadManagerMenu instance;

    [Header("Player Selection Menu List")]
    public Mb_UIChoosePlaySelector[] playerList = new Mb_UIChoosePlaySelector[4];
    [HideInInspector]public bool[] aPressed = { false, false, false, false };
    private Ma_InputController[] inputControllers;
    GamePadState[] gamepadsState = new GamePadState[4];
    GamePadState[] prevGamepadsState = new GamePadState[4];

    [Header("Player Info")]
    [HideInInspector] public int readyPlayerNbr = 0;
    public GameObject readyConfirmationCanvas;
    public bool inConfirmation = false;
    private bool skinsAndMasksSaved = false;

    [Header("Scene to Load")]
    public Sc_SelectedLevel selectedLevel;
    public Mb_Fade fadeBetweenScene;

    [Header("Scriptables Player Skin & Mask")]
    public Sc_PlayerSkinAndMask[] scriptables;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        inputControllers = GetComponents<Ma_InputController>();

        for (int i = 0; i < inputControllers.Length; i++)
        {
            switch (inputControllers[i].playerIndex)
            {
                case PlayerIndex.One:
                    playerList[0].inputController = inputControllers[i];
                    break;

                case PlayerIndex.Two:
                    playerList[1].inputController = inputControllers[i];
                    break;

                case PlayerIndex.Three:
                    playerList[2].inputController = inputControllers[i];
                    break;

                case PlayerIndex.Four:
                    playerList[3].inputController = inputControllers[i];
                    break;

            }
        }
    }

    public void Start()
    {
        UpdatePlayerSelectorPanel();
    }

    public void Update()
    {
        prevGamepadsState = gamepadsState;

        for (int i = 0; i < 4; ++i)
        {
            gamepadsState[i] = GamePad.GetState((PlayerIndex)i);
            if (gamepadsState[i].IsConnected != prevGamepadsState[i].IsConnected && !gamepadsState[i].IsConnected)
            {
                aPressed[i] = false;
                UpdatePlayerSelectorPanel();
            }
        }

        APress();
        BPress();

        Debug.Log("ReadyPlayerNbr : " + readyPlayerNbr + "/" + GetConnectedPlayerNbr());
        if (!skinsAndMasksSaved && readyPlayerNbr != 0 && readyPlayerNbr == GetConnectedPlayerNbr())
        {
            for(int i = 0; i < playerList.Length; i++)
            {
                if (playerList[i].playerIsConnected)
                {
                    playerList[i].SaveSkinAndMask(scriptables[i]);
                    /*CrossSceneInformation.selectedPlayersSkins[i] = playerList[i].GetActiveSkin();
                    CrossSceneInformation.selectedPlayersMasks[i] = playerList[i].GetActiveMask();*/
                }
                else
                {
                    scriptables[i].skinIndex = -1;
                    scriptables[i].maskIndex = -1;
                   /*CrossSceneInformation.selectedPlayersSkins[i] = -1;
                    CrossSceneInformation.selectedPlayersMasks[i] = -1;*/
                }
            }

            skinsAndMasksSaved = true;

            readyConfirmationCanvas.SetActive(true);
            inConfirmation = true;

            //TEST &DEBUG
            /*foreach(Mb_LoadSkinAndMask player in players)
            {
                player.LoadSkinAndMask();
            }*/
        }
    }

    public void UpdatePlayerSelectorPanel()
    {
        //Debug.Log(gamepadConnected);
        for(int i = 0; i < 4; i++)
        {
            if(aPressed[i])
            {
                //PlayerConnectedPanel TRUE
                playerList[i].playerConnectedPanel.SetActive(true);
                playerList[i].playerIsConnected = true;
                playerList[i].playerNotConnectedPanel.SetActive(false);
            }
            else
            {
                //PlayerConnectedPanel FALSE
                playerList[i].playerConnectedPanel.SetActive(false);
                playerList[i].playerIsConnected = false;
                playerList[i].playerNotConnectedPanel.SetActive(true);
            }
        }
    }

    public void APress()
    {
        //Debug.Log(inConfirmation);
        for (int i = 0; i < 4; i++)
        {
            if (inConfirmation)
            {
                if (inputControllers[i].AButton == ButtonState.Pressed && inputControllers[i].OldAButton == ButtonState.Released)
                {
                    Gamemanager.numberOfPlayer = GetConnectedPlayerNbr();
                    Debug.Log("Load Scene " + selectedLevel.selectedLevelIndex + "; number of player : " + Gamemanager.numberOfPlayer);
                    fadeBetweenScene.FadeToLevel(selectedLevel.selectedLevelIndex);
                }
            }
            else if (gamepadsState[i].Buttons.A == ButtonState.Pressed)
            {
                aPressed[i] = true;
                UpdatePlayerSelectorPanel();
            }
        }
    }

    public void BPress()
    {
        if (inConfirmation)
        {
            for (int i = 0; i < 4; i++)
            {
                if (inputControllers[i].BButton == ButtonState.Pressed && inputControllers[i].OldBButton == ButtonState.Released)
                {
                    playerList[i].SetPlayerIsReady(false);
                    readyConfirmationCanvas.SetActive(false);
                    skinsAndMasksSaved = false;
                    inConfirmation = false;

                    Mb_GamepadManagerMenu.instance.readyPlayerNbr--;
                    playerList[i].playerIsNotReadyPanel.SetActive(true);
                    playerList[i].playerIsReadyPanel.SetActive(false);

                    playerList[i].maskArrowsPanel.SetActive(true);
                    playerList[i].skinArrowsPanel.SetActive(true);

                }
            }
        }
    }

    public int GetConnectedPlayerNbr()
    {
        int res = 0;
        foreach(bool playerIsConnected in aPressed)
        {
            if (playerIsConnected)
                res++;
        }
        return res;
    }
}
