using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_GamepadManagerMenu : MonoBehaviour
{
    public Mb_UIChoosePlaySelector[] playerList = new Mb_UIChoosePlaySelector[4];
    private int gamepadConnected = 0;
    private string[] currentGamepad;

    public void Start()
    {
        currentGamepad = Input.GetJoystickNames();

        if(currentGamepad.Length > 1)
        {
            gamepadConnected = currentGamepad.Length;
        }
        else if(currentGamepad.Length == 1)
        {
            if (currentGamepad[0] == "")
                gamepadConnected = 0;
            else
                gamepadConnected = 1;
        }

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
        if(Input.GetJoystickNames() != currentGamepad)
        {
            currentGamepad = Input.GetJoystickNames();
            if (currentGamepad.Length > 1)
            {
                gamepadConnected = currentGamepad.Length;
            }
            else if (currentGamepad.Length == 1)
            {
                if (currentGamepad[0] == "")
                    gamepadConnected = 0;
                else
                    gamepadConnected = 1;
            }

            UpdatePlayerSelectorPanel();
        }

        if(ReadyPlayerNumber() == gamepadConnected)
        {
            // TODO CHANGE SCENE
        }
    }

    public void UpdatePlayerSelectorPanel()
    {
        //Debug.Log(gamepadConnected);
        for(int i = 0; i < 4; i++)
        {
            if(i < gamepadConnected)
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
}
