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

        playBtn.clickCallBack = audioManager.PlayOrPause;
        nextBtn.clickCallBack = audioManager.NextMusic;
        lastBtn.clickCallBack = audioManager.LastMusic;
    }
}
