using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class Mb_GamepadManagerMenu : MonoBehaviour
{
    [Header("TEST & DEBUG")]
    //public List<Mb_LoadSkinAndMask> players;

    public static Mb_GamepadManagerMenu instance;

    [Header("Player Selection Menu List")]
    public Mb_UIChoosePlaySelector[] playerList = new Mb_UIChoosePlaySelector[4];
    bool[] aPressed = { false, false, false, false };
    GamePadState[] gamepadsState = new GamePadState[4];
    GamePadState[] prevGamepadsState = new GamePadState[4];

    [HideInInspector] public int readyPlayerNbr = 0;
    private bool skinsAndMasksSaved = false;

    [Header("Scene to Load")]
    public string sceneToLoad;

    [Header("Scriptables Player Skin & Mask")]
    public Sc_PlayerSkinAndMask[] scriptables;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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

        Debug.Log("ReadyPlayerNbr : " + readyPlayerNbr + "/" + GetConnectedPlayerNbr());
        if (!skinsAndMasksSaved && readyPlayerNbr != 0 && readyPlayerNbr == GetConnectedPlayerNbr())
        {
            for(int i = 0; i < playerList.Length; i++)
            {
                if (playerList[i].playerIsConnected)
                    playerList[i].SaveSkinAndMask(scriptables[i]);
                else
                {
                    scriptables[i].skinIndex = -1;
                    scriptables[i].maskIndex = -1;
                }
            }

            skinsAndMasksSaved = true;

            if(sceneToLoad != "")
                SceneManager.LoadScene(sceneToLoad);

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
        for(int i = 0; i < 4; i++)
        {
            if (gamepadsState[i].Buttons.A == ButtonState.Pressed)
            {
                aPressed[i] = true;
                UpdatePlayerSelectorPanel();
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
