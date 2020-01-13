using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class Mb_GamepadManagerMenu : MonoBehaviour
{
    public Mb_UIChoosePlaySelector[] playerList = new Mb_UIChoosePlaySelector[4];
    public bool[] aPressed = { false, false, false, false };
    GamePadState[] gamepadsState = new GamePadState[4];
    GamePadState[] prevGamepadsState = new GamePadState[4];

    private int gamepadConnected = 0;
    private string[] currentGamepad;

    [Header("Scene to Load")]
    public string sceneToLoad;

    public void Start()
    {
        UpdatePlayerSelectorPanel();
    }

    public void Update()
    {
        //Simuler une nouvelle manette connectée
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            gamepadConnected++;
            gamepadConnected = Mathf.Clamp(gamepadConnected, 0, 4);
            UpdatePlayerSelectorPanel();

        }else*/

        prevGamepadsState = gamepadsState;

        for (int i = 0; i < 4; ++i)
        {
            gamepadsState[i] = GamePad.GetState((PlayerIndex)i);
            if (!gamepadsState[i].IsConnected)
            {
                aPressed[i] = false;
                UpdatePlayerSelectorPanel();
            }
        }

        APress();

        if (ReadyPlayerNumber() == gamepadConnected)
        {
            SceneManager.LoadScene(sceneToLoad);
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
                playerList[i].playerNotConnectedPanel.SetActive(false);
            }
            else
            {
                //PlayerConnectedPanel FALSE
                playerList[i].playerConnectedPanel.SetActive(false);
                playerList[i].playerNotConnectedPanel.SetActive(true);
            }
        }
    }

    public int ReadyPlayerNumber()
    {
        int res = 0;
        for(int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].IsReady())
                res++;
        }
        return res;
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
}
