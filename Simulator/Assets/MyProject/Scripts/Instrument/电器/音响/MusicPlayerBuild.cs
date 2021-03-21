using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerBuild : Instrument
{
    protected AudioManager audioManager;
    public mButton playBtn;
    public mButton nextBtn;
    public mButton lastBtn;

    void Start()
    {
        type = InstrumentEnum.音响;

        CanScaleInstrument = false;
        isFreeInstrument = true;
        isHangInsturment = true;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.2f;
        canDropDis = 2f;

        width = 0.3f;
        height = 0.45f;

        audioManager = transform.GetComponent<AudioManager>();

        SetBtn();
    }

    /// <summary>
    /// 设置按键状态
    /// </summary>
    protected virtual void SetBtn()
    {
        playBtn.clickCallBack = () => 
        {
            audioManager.PlayOrPause();
            if (audioManager.GetIsPlay())
            {
                playBtn.transform.Find("播放").gameObject.SetActive(true);
                playBtn.transform.Find("暂停").gameObject.SetActive(false);
                playBtn.transform.Find("播放/Enter").gameObject.SetActive(false);
                playBtn.transform.Find("暂停/Enter").gameObject.SetActive(false);
            }
            else
            {
                playBtn.transform.Find("播放").gameObject.SetActive(false);
                playBtn.transform.Find("暂停").gameObject.SetActive(true);
                playBtn.transform.Find("播放/Enter").gameObject.SetActive(false);
                playBtn.transform.Find("暂停/Enter").gameObject.SetActive(false);
            }
        };
        playBtn.enterCallBack = () => 
        {
            if (audioManager.GetIsPlay())
            {
                playBtn.transform.Find("播放/Enter").gameObject.SetActive(true);
            }
            else
            {
                playBtn.transform.Find("暂停/Enter").gameObject.SetActive(true);
            }
        };
        playBtn.exitCallBack = () => 
        {
            if (audioManager.GetIsPlay())
            {
                playBtn.transform.Find("播放/Enter").gameObject.SetActive(false);
            }
            else
            {
                playBtn.transform.Find("暂停/Enter").gameObject.SetActive(false);
            }
        };

        nextBtn.clickCallBack = audioManager.NextMusic;
        nextBtn.enterCallBack = () =>
        {
            nextBtn.transform.Find("Image/Enter").gameObject.SetActive(true);
        };
        nextBtn.exitCallBack = () =>
        {
            nextBtn.transform.Find("Image/Enter").gameObject.SetActive(false);
        };

        lastBtn.clickCallBack = audioManager.LastMusic;
        lastBtn.enterCallBack = () =>
        {
            lastBtn.transform.Find("Image/Enter").gameObject.SetActive(true);
        };
        lastBtn.exitCallBack = () =>
        {
            lastBtn.transform.Find("Image/Enter").gameObject.SetActive(false);
        };



    }
}
