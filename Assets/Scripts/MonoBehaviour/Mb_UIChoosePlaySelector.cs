using System.Collections;
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
    public Ma_InputController inputController;

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
    [HideInInspector]public bool playerIsConnected = false;

    private void Start()
    {
        playerIsNotReadyPanel.SetActive(!playerIsReady);
        playerIsReadyPanel.SetActive(playerIsReady);
        playerReadyParentPanel.GetComponent<Image>().color = playerColor;

        SetActiveArrows(skinArrowsPanel);
        SetActiveSkin(listOfAllSkins[(int)playerIndex]);
        activeMaskHolder.SetActiveMask(activeMaskHolder.listOfAllMasks[(int)playerIndex]);

        playerIndexText.text = ((int)playerIndex +1).ToString();
    }

    private void Update()
    {

        if (!playerIsReady && playerIsConnected)
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

        BPress();
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
                i.color = new Color(i.color.r, i.color.g, i.color.b, 0.05f);
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

    public void SaveSkinAndMask(Sc_PlayerSkinAndMask scriptable)
    {
        scriptable.skinIndex = listOfAllSkins.IndexOf(activeSkin);
        scriptable.maskIndex = activeMaskHolder.listOfAllMasks.IndexOf(activeMaskHolder.GetActiveMask());
        Debug.Log("Skin & Mask SAVED for Player " + (int)playerIndex);
    }

    //VECTOR GAMEPAD REGION
    #region
    public int CurrentXAxis()
    {
        int res;
        if (inputController.LeftThumbStick.x > 0 || inputController.DpadRight == ButtonState.Pressed)
        {
            res = 1;
        }
        else if (inputController.LeftThumbStick.x < 0 || inputController.DpadLeft == ButtonState.Pressed)
        {
            res = -1;
        }
        else
        {
            res = 0;
        }
        //Debug.Log("Player " + ((int)playerIndex) + " : X Axis : " + res);
        return res;
    }

    public int OldXAxis()
    {
        int res;
        if (inputController.OldLeftThumbStick.x > 0 || inputController.OldDpadRight == ButtonState.Pressed)
        {
            res = 1;
        }
        else if (inputController.OldLeftThumbStick.x < 0 || inputController.OldDpadRight == ButtonState.Pressed)
        {
            res = -1;
        }
        else
        {
            res = 0;
        }
        return res;
    }

    public int CurrentYAxis()
    {
        int res;
        if (inputController.LeftThumbStick.z > 0 || inputController.DPadUp == ButtonState.Pressed)
        {
            res = 1;
        }
        else if(inputController.LeftThumbStick.z < 0 || inputController.DpadDown == ButtonState.Pressed)
        {
            res = -1;
        }
        else
        {
            res = 0;
        }
        //Debug.Log("Player "+((int)playerIndex) + " : Y Axis : " + res);
        return res;
    }

    public int OldYAxis()
    {
        int res;
        if (inputController.OldLeftThumbStick.z > 0 || inputController.OldDPadUp == ButtonState.Pressed)
        {
            res = 1;
        }
        else if(inputController.OldLeftThumbStick.z < 0 || inputController.OldDpadDown == ButtonState.Pressed)
        {
            res = -1;
        }
        else
        {
            res = 0;
        }
        return res;
    }

    public void XPress()
    {
        if(inputController.XButton == ButtonState.Pressed)
        {
            playerIsReady = true;
            Mb_GamepadManagerMenu.instance.readyPlayerNbr++;
            playerIsNotReadyPanel.SetActive(false);
            playerIsReadyPanel.SetActive(true);

            maskArrowsPanel.SetActive(false);
            skinArrowsPanel.SetActive(false);
        }
    }

    public void BPress()
    {
        if(inputController.BButton == ButtonState.Released && inputController.OldBButton == ButtonState.Pressed)
        {
            if (playerIsReady)
            {
                playerIsReady = false;
                
                Mb_GamepadManagerMenu.instance.readyPlayerNbr--;
                playerIsNotReadyPanel.SetActive(true);
                playerIsReadyPanel.SetActive(false);

                maskArrowsPanel.SetActive(true);
                skinArrowsPanel.SetActive(true);

            }
            else if (playerIsConnected)
            {
                playerIsConnected = false;
                Mb_GamepadManagerMenu.instance.aPressed[(int)playerIndex] = false;
                Mb_GamepadManagerMenu.instance.UpdatePlayerSelectorPanel();
            }
        }
    }
    #endregion
}
