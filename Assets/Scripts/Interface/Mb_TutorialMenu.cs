using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XInputDotNetPure;

public class Mb_TutorialMenu : MonoBehaviour
{

    public Ma_InputController inputController;
    [Header("Graphic references")]
    public List<GameObject> allPanelTuto = new List<GameObject>();
    public GameObject leftArrow, rightArrow;
    public TextMeshProUGUI activePanelNbr, allPanelNbr;

    private GameObject activePanel;

    // Start is called before the first frame update
    void Awake()
    {
        SetAcivePanel(allPanelTuto[0]);
    }

    private void Start()
    {
        inputController = Gamemanager.instance.inputControllers[0];
        allPanelNbr.text = "/" + allPanelTuto.Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        int currentStickXAxis = CurrentXAxis();
        int oldStickXAxis = OldXAxis();

        if (currentStickXAxis != 0 && currentStickXAxis != oldStickXAxis)
        {
            //Debug.Log("CurrentXAxis : " + currentStickXAxis + ", OldXAxis : " + oldStickXAxis);
            if (allPanelTuto.IndexOf(activePanel) == allPanelTuto.Count - 1 && currentStickXAxis == 1)
            {
                //rightArrow.SetActive(false);
            }
            else if (allPanelTuto.IndexOf(activePanel) == 0 && currentStickXAxis == -1)
            {
                //leftArrow.SetActive(false);
            }
            else
            {
                SetAcivePanel(allPanelTuto[allPanelTuto.IndexOf(activePanel) + currentStickXAxis]);
            }
            UpdatePanelNbr();
        }

        if(allPanelTuto.IndexOf(activePanel) == allPanelTuto.Count - 1 && inputController.AButton == ButtonState.Pressed && inputController.OldAButton == ButtonState.Released)
        {
            Debug.Log("LANCER DECOMPT");
            Gamemanager.instance.SetGamePause(false);
            gameObject.SetActive(false);
        }
    }

    public void SetAcivePanel(GameObject newActivePanel)
    {
        rightArrow.SetActive(true);
        leftArrow.SetActive(true);

        if (activePanel != null)
        {
            activePanel.SetActive(false);
        }
        activePanel = newActivePanel;
        if (activePanel != null)
        {
            activePanel.SetActive(true);
        }

        if (allPanelTuto.IndexOf(activePanel) == allPanelTuto.Count - 1)
        {
            rightArrow.SetActive(false);
        }
        else if (allPanelTuto.IndexOf(activePanel) == 0)
        {
            leftArrow.SetActive(false);
        }
    }

    public void UpdatePanelNbr()
    {
        activePanelNbr.text = (allPanelTuto.IndexOf(activePanel) + 1).ToString();
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
