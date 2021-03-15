using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerBuild : Instrument
{
    protected AudioManager audioManager;
    void Start()
    {
        type = InstrumentEnum.音响;
        isFreeInstrument = true;
        isHangInsturment = true;
        MaxOffsetZ = 2f;
        MinOffsetZ = 0.2f;
        canDropDis = 2f;

        audioManager = gameObject.AddComponent<AudioManager>();
    }

    public AudioManager GetAudioManager()
    {
        return audioManager;
    }

}
