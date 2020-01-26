using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_SoundManager : MonoBehaviour
{
    public static Ma_SoundManager instance;
    SoundManager

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void PlaySound()
}
