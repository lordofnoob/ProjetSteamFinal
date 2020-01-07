using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_GameManager : MonoBehaviour
{
    public static Ma_GameManager instance;
    public int redRessource, blueRessource, greenRessource;


    private void Start()
    {
        instance = this;
        blueRessource = Ma_LevelManager.instance.levelParameters.baseBlueRessource;
        redRessource = Ma_LevelManager.instance.levelParameters.baseRedRessource;
        greenRessource = Ma_LevelManager.instance.levelParameters.baseGreenRessource;
    }
}
