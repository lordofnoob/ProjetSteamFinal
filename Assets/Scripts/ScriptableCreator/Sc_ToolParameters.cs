using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewToolParameters", menuName = "Create NewTrial Parameters/Tool Interaction")]
public class Sc_ToolParameters : Sc_TrialParameters
{
    public ToolType toolType;
}

public enum ToolType
{
    crowbar, drill
}
