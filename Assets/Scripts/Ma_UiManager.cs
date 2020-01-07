using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ma_UiManager : MonoBehaviour
{
    public static Ma_UiManager instance;
    public TextMeshProUGUI[] ressourcesCount;
    public TextMeshProUGUI[] timeDisplaySpots;

    private void Start()
    {
        instance = this;
        UpdateRessourcesUI();
    }

    public void UpdateRessourcesUI()
    {
        ressourcesCount[0].text = Ma_GameManager.instance.blueRessource.ToString();
        ressourcesCount[1].text = Ma_GameManager.instance.redRessource.ToString();
        ressourcesCount[2].text = Ma_GameManager.instance.greenRessource.ToString();
    }

    public void UpdateTimeDisplaySpot(float actualTime, float nextTime)
    {
        timeDisplaySpots[0].text = actualTime.ToString();
        timeDisplaySpots[1].text = nextTime.ToString();
    }

}
