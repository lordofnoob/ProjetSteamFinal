using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrialParameters", menuName = "Create NewTrial Parameters/Base Interaction")]
public class Sc_TrialParameters : ScriptableObject
{
   [Header("TrialSpecs")]
    public int trialPriority;
    public TrialType trialType;
    public ItemType[] toolsNeeded;
    
    [Header("Progression")]
    public float accomplishmentNeeded;

   [HideInInspector] public float accomplishmentToAdd=1;
    public int numberOfPlayerNeeded;

    [Header("Decay")]
    public float timeToWaitBeforeDecay = 1;
    public float decaying = 1;
    public float forceDecay = 0;
}

public enum TrialType
{
    Time, Mashing
}


