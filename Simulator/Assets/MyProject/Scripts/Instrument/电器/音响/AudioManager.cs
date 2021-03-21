using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;//存储音乐音频 

    protected AudioSource audioSource;
    protected int num;
    protected bool isPlay;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        num = 0;
        audioSource.clip = audioClips[num];
        audioSource.Play();
        isPlay = true;
    }

    public void PlayOrPause()
    {
        if (isPlay)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
        isPlay = !isPlay;
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
        isPlay = true;
        Debug.Log("播放: " + audioClips[num].name);
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
        isPlay = true;
        Debug.Log("播放: " + audioClips[num].name);
    }

    /// <summary>
    /// 是否正在播放
    /// </summary>
    /// <returns></returns>
    public bool GetIsPlay()
    {
        return isPlay;
    }

}
