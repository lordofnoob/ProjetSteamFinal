using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="NewLevelParameters", menuName = "CreateNewLevelParameters")]
public class Sc_LevelParameters : ScriptableObject
{
    public int baseRedRessource, baseBlueRessource, baseGreenRessource;
    public float[] bootTimes;
}
