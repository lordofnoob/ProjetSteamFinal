using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class Mb_SettingsMenu : MonoBehaviour
{
    public List<Image> cursorSpots = new List<Image>();
    public Ma_InputController inputController;
    public bool inThisMenu = false;
    public Mb_MainMenu mainMenu;
    public Mb_PauseMenu pauseMenu;
    public Sc_SoundParameters soundParam;
    public Image musicFillBar, effectsFillBar;

    private Image activeButton;

    void Awake()
    {
        if(mainMenu != null)
        {
            for (int i = 0; i < 4; i++)
            {
                if (GamePad.GetState((PlayerIndex)i).IsConnected)
                {
                    inputController.playerIndex = (PlayerIndex)i;
                    break;
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (inThisMenu)
            SetActiveButton(cursorSpots[0]);
        UpdateFillBar();
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

            if (currentStickXAxis != 0 && currentStickXAxis != oldStickXAxis)
            {
                switch (cursorSpots.IndexOf(activeButton))
                {
                    case 0:

                        soundParam.musicVolume += 0.1f * currentStickXAxis;
                        //Debug.Log("Musique fill amount : " + soundParam.musicVolume);

                        break;

                    case 1:
                        soundParam.effectVolume += 0.1f * currentStickXAxis;
                        //Debug.Log("Effect fill amount : " + soundParam.effectVolume);

                        break;

                }

                soundParam.musicVolume = Mathf.Clamp(soundParam.musicVolume, 0f, 1f);
                soundParam.effectVolume = Mathf.Clamp(soundParam.effectVolume, 0f, 1f);
                UpdateFillBar();
            }

            if (inputController.BButton == ButtonState.Pressed && inputController.OldBButton == ButtonState.Released)
            {
                //Debug.Log(pauseMenu);
                if (mainMenu != null)
                {
                    mainMenu.inThisMenu = true;
                    mainMenu.transform.gameObject.SetActive(true);
                    mainMenu.SetActiveButton(mainMenu.cursorSpots[0]);
                }
                else if (pauseMenu != null)
                {
                    pauseMenu.inThisMenu = true;
                    pauseMenu.mainPauseMenu.SetActive(true);
                    pauseMenu.SetActiveButton(pauseMenu.cursorSpots[0]);
                }

                inThisMenu = false;
                transform.gameObject.SetActive(false);
                SetActiveButton(null);
            }
        }
    }

    public void SetActiveButton(Image newActiveButton)
    {
        if (activeButton != null)
            activeButton.gameObject.SetActive(false);
        activeButton = newActiveButton;
        if (activeButton != null)
            activeButton.gameObject.SetActive(true);
    }

    public void UpdateFillBar()
    {
        musicFillBar.fillAmount = soundParam.musicVolume;
        effectsFillBar.fillAmount = soundParam.effectVolume;
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
