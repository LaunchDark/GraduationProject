using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;//存储音乐音频 

    protected AudioSource audioSource;
    protected int num;
    protected bool isPlay;

    protected Coroutine coroutine;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();        
    }

    private void OnEnable()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
        num = 999;
        NextMusic();
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    public void PlayOrPause()
    {
        if (isPlay)
        {
            audioSource.Pause();
            isPlay = false;
        }
        else
        {
            audioSource.Play();
            isPlay = true;
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(AudioPlayFinished(duration, NextMusic));
        }
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

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(AudioPlayFinished(audioSource.clip.length, NextMusic));

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

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(AudioPlayFinished(audioSource.clip.length, NextMusic));
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

    /// <summary>
    /// 音乐剩余长度
    /// </summary>
    float duration;

    /// <summary>
    /// 完成播放时回调
    /// </summary>
    /// <param name="time">音频长度</param>
    /// <param name="callback">播放结束回调</param>
    /// <returns></returns>
    private IEnumerator AudioPlayFinished(float time, UnityAction callback)
    {
        duration = time;
        //Debug.Log(isPlay);
        while (isPlay)
        {
            //Debug.Log(duration);
            if (duration < 0)
            {
                //Debug.Log(111);
                break;
            }
            yield return new WaitForSeconds(1);
            duration--;
        }

        if (isPlay)
        {
            callback.Invoke();
        }
        //Debug.Log("结束");
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Keypad0))
    //    {
    //        PlayOrPause();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad1))
    //    {
    //        LastMusic();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Keypad2))
    //    {
    //        NextMusic();
    //    }
    //}

}
