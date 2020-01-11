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

    public void UpdateTimeRemainingText(float RemainingTime)
    {
        timeRemainingText.text = Mathf.RoundToInt(RemainingTime / 60) + " / "+ Mathf.RoundToInt(RemainingTime - Mathf.RoundToInt(RemainingTime / 60)*60); 
    }
}
