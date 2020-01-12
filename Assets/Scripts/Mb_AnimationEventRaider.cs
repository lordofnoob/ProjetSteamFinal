using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mb_AnimationEventRaider : MonoBehaviour
{
    [SerializeField] Mb_PlayerControler controledPlayer;

    public void SetCanMoveTrueTrue()
    {
        controledPlayer.SetCanMove(true);
    }
}
