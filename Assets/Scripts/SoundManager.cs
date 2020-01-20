using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource source;

    private void Awake()

    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip soundToPlay, float volume)
    {
        source.PlayOneShot(soundToPlay, volume);
    }
}