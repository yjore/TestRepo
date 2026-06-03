using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefineion : MonoBehaviour
{
    public AudioEventSO AudioEventSO;
    public AudioClip audioClip;
    public bool PlayOnEnable;

    private void OnEnable()
    {
        if (PlayOnEnable)
        {
            PlayOnClip();
        }
    }

    public void PlayOnClip()
    {
        AudioEventSO.RaiseEvent(audioClip);
    }
}
