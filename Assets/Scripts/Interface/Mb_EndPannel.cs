using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Mb_EndPannel : MonoBehaviour
{
    public static Mb_EndPannel instance;

    public Animator firstStar, secondStar, thirdStar;
    public TextMeshProUGUI objectiveText, escapedPlayer;
    public TextMeshProUGUI moneySpot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    
}
