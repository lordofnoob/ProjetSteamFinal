using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerCharacteristics", menuName ="CreatePlayerCharacteristics")]
public class Sc_PlayerCharact : ScriptableObject
{
    public PlayerMovementParameters baseCharacterMovement;
    public AnimationCurve throwGrowingStrengh;
    public int throwMaxStrengh;
}

[System.Serializable]
public struct PlayerMovementParameters
{
    public int MoveSpeed;
    public AnimationCurve AccelerationRate;
}
