using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using System;

public class Mb_UIChoosePlaySelector : MonoBehaviour
{
    [Header("Manette")]
    public PlayerIndex playerIndex;
    GamePadState controlerUsedOldState;
    GamePadState controlerUsedState;

    [Header("Prefabs")]
    public List<GameObject> listOfAllSkins;
    public List<GameObject> listOfAllMasks;

    [Header("UI")]
    public GameObject playerConnectedPanel, playerNotConnectedPanel;
    public GameObject prefabsContainer;
    public GameObject maskArrowsPanel, skinArrowsPanel;

    private GameObject activeArrows;
    private GameObject activeSkin;
    private GameObject activeMask;

    private void Start()
    {
        SetActiveArrows(skinArrowsPanel);
        SetActiveSkin(listOfAllSkins[0]);
        SetActiveMask(listOfAllMasks[0]);
    }

    private void Update()
    {
        controlerUsedOldState = controlerUsedState;
        controlerUsedState = GamePad.GetState(playerIndex);

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

        if(currentStickXAxis != 0 && currentStickXAxis != oldStickXAxis)
        {
            if (activeArrows == skinArrowsPanel)
            {
                if(listOfAllSkins.IndexOf(activeSkin) == listOfAllSkins.Count - 1 && currentStickXAxis == 1)
                {
                    SetActiveSkin(listOfAllSkins[0]);
                }
                else if(listOfAllSkins.IndexOf(activeSkin) == 0 && currentStickXAxis == -1)
                {
                    SetActiveSkin(listOfAllSkins[listOfAllSkins.Count - 1]);
                }
                else
                {
                    SetActiveSkin(listOfAllSkins[listOfAllSkins.IndexOf(activeSkin) + currentStickXAxis]);
                }
            }
            else if(activeArrows == maskArrowsPanel)
            {
                if (listOfAllMasks.IndexOf(activeMask) == listOfAllMasks.Count - 1 && currentStickXAxis == 1)
                {
                    SetActiveSkin(listOfAllMasks[0]);
                }
                else if (listOfAllMasks.IndexOf(activeMask) == 0 && currentStickXAxis == -1)
                {
                    SetActiveSkin(listOfAllMasks[listOfAllMasks.Count - 1]);
                }
                else
                {
                    SetActiveSkin(listOfAllMasks[listOfAllMasks.IndexOf(activeMask) + currentStickXAxis]);
                }
            }
        }
    }

    private void SetActiveMask(GameObject newActiveMask)
    {
        Debug.Log("CHANGER DE MASK : activeMaskIndex : " + listOfAllMasks.IndexOf(activeMask));
        activeMask = newActiveMask;
    }

    private void SetActiveSkin(GameObject newActiveSkin)
    {
        Debug.Log("CHANGER DE SKIN : activeSkinIndex : " + listOfAllSkins.IndexOf(activeSkin));
        if(activeSkin != null)
            activeSkin.SetActive(false);
        activeSkin = newActiveSkin;
        activeSkin.SetActive(true);
        // TODO :METTRE A JOUR listOfAllMasks
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
    #endregion
}
