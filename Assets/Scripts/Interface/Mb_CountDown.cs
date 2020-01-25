using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mb_CountDown : MonoBehaviour
{

    public Animator animator;

    public void LaunchCountdown()
    {
        animator.SetTrigger("LaunchCountDown");
    }

    public void StartGame()
    {
        Debug.Log("Start the game");
        Gamemanager.instance.StartGame();
    }
}
