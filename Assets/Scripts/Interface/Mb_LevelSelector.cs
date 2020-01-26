using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;

public class Mb_LevelSelector : MonoBehaviour
{
    public Sc_SelectedLevel selectedLevel;
    public Mb_Fade fadeBetweenScene;
    public LevelSelection_RightPanelManager rightPanel;
    public List<Image> cursorSpots = new List<Image>();
    public Ma_InputController inputController;
    public bool inThisMenu = false;
    public Mb_MainMenu mainMenu;
    public bool playerCanSelect = false;

    private Dictionary<Image, Button> levels = new Dictionary<Image, Button>();
    private Image activeButton;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            if (GamePad.GetState((PlayerIndex)i).IsConnected)
            {
                inputController.playerIndex = (PlayerIndex)i;
                break;
            }
        }

        for (int y = 0; y < cursorSpots.Count; y++)
        {
            levels.Add(cursorSpots[y], cursorSpots[y].transform.parent.GetComponent<Button>());
        }

    }

    private void Start()
    {
        if(inThisMenu)
            SetActiveButton(cursorSpots[0]);
    }

    // Update is called once per frame
    void Update()
    {
        int currentStickYAxis = CurrentYAxis();
        int currentStickXAxis = CurrentXAxis();
        int oldStickYAxis = OldYAxis();
        int oldStickXAxis = OldXAxis();

        if (inThisMenu)
        {
            if (currentStickYAxis != 0 && currentStickYAxis != oldStickYAxis)
            {
                //Debug.Log("Current Y : " + currentStickYAxis);
                switch (currentStickYAxis)
                {
                    case 1:
                        if (cursorSpots.IndexOf(activeButton) == 0)
                            SetActiveButton(cursorSpots[cursorSpots.Count - 1]);
                        else
                            SetActiveButton(cursorSpots[cursorSpots.IndexOf(activeButton) - 1]);
                        break;

                    case -1:
                        if (cursorSpots.IndexOf(activeButton) == cursorSpots.Count - 1)
                            SetActiveButton(cursorSpots[0]);
                        else
                            SetActiveButton(cursorSpots[cursorSpots.IndexOf(activeButton) + 1]);
                        break;
                }
            }

            if(inputController.BButton == ButtonState.Pressed && inputController.OldBButton == ButtonState.Released)
            {
                inThisMenu = false;
                mainMenu.transform.parent.gameObject.SetActive(true);
                mainMenu.SetActiveButton(mainMenu.cursorSpots[0]);
                SetActiveButton(null);
            }

            if(inputController.AButton == ButtonState.Pressed && inputController.OldAButton == ButtonState.Released)
            {
                //Debug.Log(inputController.AButton + " ; " + inputController.OldAButton);
                if (playerCanSelect)
                {
                    levels[activeButton].Select();
                    levels[activeButton].onClick.Invoke();
                }
            }
        }
    }

    public void SetActiveButton(Image newActiveButton)
    {
        if(activeButton != null)
        {
            levels[activeButton].interactable = false;
            activeButton.gameObject.SetActive(false);
        }
        activeButton = newActiveButton;
        if(activeButton != null)
            activeButton.gameObject.SetActive(true);
        //Debug.Log("ChangeActiveButton : " + activeButton);
        levels[activeButton].interactable = true;
        rightPanel.UpdateLevelSelection(cursorSpots.IndexOf(activeButton));
    }

    public void SelectLevel(int levelIndex)
    {
        selectedLevel.selectedLevelIndex = levelIndex;
        fadeBetweenScene.FadeToLevel(1);
    }

    public IEnumerator ActiveLevelSelection()
    {
        yield return new WaitForSeconds(0.5f);
        playerCanSelect = true;
    }

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
        else if (inputController.OldLeftThumbStick.x < 0 || inputController.OldDpadLeft == ButtonState.Pressed)
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
        else if (inputController.LeftThumbStick.z < 0 || inputController.DpadDown == ButtonState.Pressed)
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
        else if (inputController.OldLeftThumbStick.z < 0 || inputController.OldDpadDown == ButtonState.Pressed)
        {
            res = -1;
        }
        else
        {
            res = 0;
        }
        return res;
    }

    #endregion

}
