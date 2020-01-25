using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using XInputDotNetPure;

public class Mb_EndPannel : MonoBehaviour
{
    public static Mb_EndPannel instance;

    public Animator firstStar, secondStar, thirdStar;
    public TextMeshProUGUI objectiveText, escapedPlayer;
    public TextMeshProUGUI appreciation,moneySpot, bestScoreSpot;
    public Animator animationBestScore;
 

    [Header("Cursor")]
    public List<Image> cursorSpots = new List<Image>();
    public Ma_InputController inputController;
    public bool inThisMenu = false;

    private Image activeButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        appreciation.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (inThisMenu)
            SetActiveButton(cursorSpots[0]);
        inputController = Gamemanager.instance.inputControllers[0];
    }

    void Update()
    {
        int currentStickXAxis = CurrentXAxis();
        int oldStickXAxis = OldXAxis();

        if (inThisMenu)
        {
            if (currentStickXAxis != 0 && currentStickXAxis != oldStickXAxis)
            {
                //Debug.Log("Current Y : " + currentStickYAxis);
                switch (currentStickXAxis)
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


            if (inputController.AButton == ButtonState.Pressed && inputController.OldAButton == ButtonState.Released)
            {
                activeButton.GetComponentInParent<Button>().onClick.Invoke();
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

    public void ExitToMainMenu()
    {
        //Debug.Log("Exit to mainMenu");
        Time.timeScale = 1f;
        Ma_UiManager.instance.FadeToLevel(0);
    }

    public void ReloadScene()
    {
        Ma_UiManager.instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Ma_UiManager.instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
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

    #endregion

}
