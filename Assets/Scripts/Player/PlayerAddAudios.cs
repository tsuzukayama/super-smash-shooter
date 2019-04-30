using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddAudios : MonoBehaviour
{

    public AudioClip GunShotClip;
    public AudioClip GunReloadClip;
    public AudioClip RunningClip;

    public AudioSource GunShotSource;
    public AudioSource GunReloadSource;
    public AudioSource RunningSource;

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    private void Awake()
    {
        GunShotSource = AddAudio(GunShotClip, false, false, 0.5f);
        GunReloadSource = AddAudio(GunReloadClip, false, false, 0.5f);
        RunningSource = AddAudio(RunningClip, false, false, 0.5f);
    }
}
