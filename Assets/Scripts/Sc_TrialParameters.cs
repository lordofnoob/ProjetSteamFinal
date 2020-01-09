using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrialParameters", menuName = "CreateNewTrialParameters")]
public class Sc_TrialParameters : ScriptableObject
{
    public int trialPriority;
    public int accomplishmentNeeded;
    public int numberOfPlayerNeeded;
    public ToolType[] toolsNeeded;
    public TrialType trialType;
    public float accomplishmentToAdd;
    public float decaying;
    public float timeToWaitBeforeDecay;
}

public enum TrialType
{
    Time, Mashing
}
