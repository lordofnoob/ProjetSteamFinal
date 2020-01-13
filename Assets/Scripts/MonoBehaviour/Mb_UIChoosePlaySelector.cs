﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public class Mb_UIChoosePlaySelector : MonoBehaviour
{
    [Header("Manette")]
    public PlayerIndex playerIndex;
    public Color playerColor;
    GamePadState controlerUsedOldState;
    GamePadState controlerUsedState;

    [Header("Prefabs")]
    public List<GameObject> listOfAllSkins;

    [Header("UI")]
    public TextMeshProUGUI playerIndexText;
    public GameObject playerConnectedPanel;
    public GameObject playerNotConnectedPanel;
    public GameObject maskArrowsPanel, skinArrowsPanel;
    public GameObject playerIsReadyPanel, playerIsNotReadyPanel, playerReadyParentPanel;

    private GameObject activeArrows;
    private GameObject activeSkin;
    private Mb_HoldingMasks activeMaskHolder;
    private bool playerIsReady = false;

    private void Start()
    {
        playerIsNotReadyPanel.SetActive(!playerIsReady);
        playerIsReadyPanel.SetActive(playerIsReady);
        playerReadyParentPanel.GetComponent<Image>().color = playerColor;

        //Debug.Log(Input.GetJoystickNames()[0]);
        SetActiveArrows(skinArrowsPanel);
        SetActiveSkin(listOfAllSkins[(int)playerIndex]);
        activeMaskHolder.SetActiveMask(activeMaskHolder.listOfAllMasks[(int)playerIndex]);

        playerIndexText.text = ((int)playerIndex +1).ToString();
    }

    private void Update()
    {
        controlerUsedOldState = controlerUsedState;
        controlerUsedState = GamePad.GetState(playerIndex);

        if (!playerIsReady)
        {

            int currentStickYAxis = CurrentYAxis();
            int currentStickXAxis = CurrentXAxis();
            int oldStickYAxis = OldYAxis();
            int oldStickXAxis = OldXAxis();

            if (currentStickYAxis != 0 && currentStickYAxis != oldStickYAxis)
            {
                switch (currentStickYAxis)
                {
                    case 1:
                        SetActiveArrows(maskArrowsPanel);
                        break;

                    case -1:
                        SetActiveArrows(skinArrowsPanel);
                        break;
                }
            }

            if (currentStickXAxis != 0 && currentStickXAxis != oldStickXAxis)
            {
                if (activeArrows == skinArrowsPanel)
                {
                    if (listOfAllSkins.IndexOf(activeSkin) == listOfAllSkins.Count - 1 && currentStickXAxis == 1)
                    {
                        SetActiveSkin(listOfAllSkins[0]);
                    }
                    else if (listOfAllSkins.IndexOf(activeSkin) == 0 && currentStickXAxis == -1)
                    {
                        SetActiveSkin(listOfAllSkins[listOfAllSkins.Count - 1]);
                    }
                    else
                    {
                        SetActiveSkin(listOfAllSkins[listOfAllSkins.IndexOf(activeSkin) + currentStickXAxis]);
                    }
                }
                else if (activeArrows == maskArrowsPanel)
                {
                    if (activeMaskHolder.listOfAllMasks.IndexOf(activeMaskHolder.GetActiveMask()) == activeMaskHolder.listOfAllMasks.Count - 1 && currentStickXAxis == 1)
                    {
                        activeMaskHolder.SetActiveMask(activeMaskHolder.listOfAllMasks[0]);
                    }
                    else if (activeMaskHolder.listOfAllMasks.IndexOf(activeMaskHolder.GetActiveMask()) == 0 && currentStickXAxis == -1)
                    {
                        activeMaskHolder.SetActiveMask(activeMaskHolder.listOfAllMasks[activeMaskHolder.listOfAllMasks.Count - 1]);
                    }
                    else
                    {
                        activeMaskHolder.SetActiveMask(activeMaskHolder.listOfAllMasks[activeMaskHolder.listOfAllMasks.IndexOf(activeMaskHolder.GetActiveMask()) + currentStickXAxis]);
                    }
                }
            }

            XPress();
        }
    }


    private void SetActiveSkin(GameObject newActiveSkin)
    {
        if(activeSkin != null)
            activeSkin.SetActive(false);
        activeSkin = newActiveSkin;
        activeSkin.SetActive(true);

        if(activeMaskHolder != null)
        {
            int activeMaskIndex = activeMaskHolder.listOfAllMasks.IndexOf(activeMaskHolder.GetActiveMask());
            activeMaskHolder = activeSkin.GetComponent<Mb_HoldingMasks>();
            activeMaskHolder.SetActiveMask(activeMaskHolder.listOfAllMasks[activeMaskIndex]);
        }
        else
        {
            activeMaskHolder = activeSkin.GetComponent<Mb_HoldingMasks>();
        }

        //Debug.Log("CHANGER DE SKIN : activeSkinIndex : " + listOfAllSkins.IndexOf(activeSkin));
    }

    public void SetActiveArrows(GameObject activePanel)
    {
        if(activeArrows != null)
        {
            foreach (Image i in activeArrows.GetComponentsInChildren<Image>())
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, 0.5f);
            }
        }

        activeArrows = activePanel;
        foreach (Image i in activeArrows.GetComponentsInChildren<Image>())
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1f);
        }
    }

    public bool IsReady()
    {
        return playerIsReady;
    }

    //VECTOR GAMEPAD REGION
    #region
    public int CurrentXAxis()
    {
        if (controlerUsedState.ThumbSticks.Left.X > 0 || controlerUsedState.DPad.Right == ButtonState.Pressed)
        {
            return 1;
        }
        else if (controlerUsedState.ThumbSticks.Left.X < 0 || controlerUsedState.DPad.Left == ButtonState.Pressed)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public int OldXAxis()
    {
        if (controlerUsedOldState.ThumbSticks.Left.X > 0 || controlerUsedOldState.DPad.Right == ButtonState.Pressed)
        {
            return 1;
        }
        else if (controlerUsedOldState.ThumbSticks.Left.X < 0 || controlerUsedOldState.DPad.Left == ButtonState.Pressed)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public int CurrentYAxis()
    {
        if(controlerUsedState.ThumbSticks.Left.Y > 0 || controlerUsedState.DPad.Up == ButtonState.Pressed)
        {
            return 1;
        }
        else if(controlerUsedState.ThumbSticks.Left.Y < 0 || controlerUsedState.DPad.Down == ButtonState.Pressed)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public int OldYAxis()
    {
        if(controlerUsedOldState.ThumbSticks.Left.Y > 0 || controlerUsedOldState.DPad.Up == ButtonState.Pressed)
        {
            return 1;
        }
        else if(controlerUsedOldState.ThumbSticks.Left.Y < 0 || controlerUsedOldState.DPad.Down == ButtonState.Pressed)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public void XPress()
    {
        if(controlerUsedState.Buttons.X == ButtonState.Pressed)
        {
            playerIsReady = true;
            playerIsNotReadyPanel.SetActive(false);
            playerIsReadyPanel.SetActive(true);

            foreach (Image i in activeArrows.GetComponentsInChildren<Image>())
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, 0.5f);
            }
        }
    }
    #endregion
}
