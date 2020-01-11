using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ma_UiManager : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject EndCanvas;
    public TextMeshProUGUI timeRemainingText;
    public static Ma_UiManager instance;
    private void Awake()

    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

        public void SetActiveCanvas(GameObject canvasToActivate)
    {
        canvasToActivate.SetActive(true);
    }

    public void DesactiveCanvas(GameObject canvasToDesctivate)
    {
        canvasToDesctivate.SetActive(false);
    }

    public void UpdateTimeRemainingText(float remainingTime)
    {
        float minuteRemaining, secondsRemaining;
        minuteRemaining = Mathf.Clamp(Mathf.FloorToInt(remainingTime / 60),0,9999999);
        secondsRemaining = Mathf.Clamp(Mathf.RoundToInt(remainingTime - minuteRemaining * 60), 0, 59);
        string timeSpentToDisplay;

        if (secondsRemaining > 10)
            timeSpentToDisplay = minuteRemaining + " : " + secondsRemaining;
        else
            timeSpentToDisplay = minuteRemaining + " : 0" + secondsRemaining;

        timeRemainingText.text = timeSpentToDisplay;
    }
}
