using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_LevelManager : MonoBehaviour
{
    public Sc_LevelParameters levelParameters;
    public static Ma_LevelManager instance;
    [HideInInspector] public float[] turnDuration;
    bool counting;
    float timeModifer =1;
    float timeToCount;
    int currentTimeIndex =0;
   
    private void Start()
    {
        instance = this;

        turnDuration = levelParameters.bootTimes;
        //setup du premier temps pour faire des actions
        timeToCount = turnDuration[0];
    }

    void Update()
    {
        Counting();
    }

    void Counting()
    {
        timeToCount -= Time.deltaTime * timeModifer;
        Ma_UiManager.instance.UpdateTimeDisplaySpot(Mathf.RoundToInt(timeToCount), turnDuration[currentTimeIndex+1]);
        if (timeToCount<=0)
        {
            currentTimeIndex += 1;
            timeToCount = turnDuration[currentTimeIndex];
        }
    }

    public void ModifyTimeModifier(float modifierToAdd)
    {
        timeModifer += modifierToAdd;
    }

    public void ResetTimeModifier()
    {
        timeModifer = 1;
    }

    public void PauseTime()
    {
        timeModifer = 0;
    }

}
