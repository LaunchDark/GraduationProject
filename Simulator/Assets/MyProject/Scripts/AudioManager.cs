﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;//存储音乐音频 

    protected AudioSource audioSource;
    protected int num;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        num = 0;
        audioSource.clip = audioClips[num];
        audioSource.Play();
    }

    public void NextMusic()
    {
        audioSource.Stop();
        num++;
        if (num >= audioClips.Count)
        {
            num = 0;
        }
        audioSource.clip = audioClips[num];
        audioSource.Play();
        Debug.Log(audioClips[num].name);
    }

    public void LastMusic()
    {
        audioSource.Stop();
        num--;
        if (num < 0)
        {
            num = audioClips.Count - 1;
        }
        audioSource.clip = audioClips[num];
        audioSource.Play();
        Debug.Log(audioClips[num].name);
    }


}