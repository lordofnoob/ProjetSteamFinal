using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelParameter", menuName = "CreateNewLevelParameters")]
public class Sc_LevelParameters : ScriptableObject
{
    //  public Sc_Objective[] objectivesOfTheLevel;
    public Sc_TrialParameters itemToGet1, itemToGet2;
    public int moneyToSteal;
    public float timeToDoTheLevel;

}
