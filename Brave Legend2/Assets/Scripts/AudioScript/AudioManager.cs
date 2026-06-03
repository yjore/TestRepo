using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMangaer : MonoBehaviour
{
    [Header("事件监听")]
    public AudioEventSO BGMaudioEventSO;
    public AudioEventSO FXaudioEventSO;
    public FloatEventSO VolumeEvent;
    public VoidEventSO PauseEvent;
    [Header("广播")]
    public FloatEventSO syncVolumeEvent;
    [Header("组件")]
    public AudioSource BGMaudio;
    public AudioSource FXaudio;
    public AudioMixer mixer;
    private void OnEnable()
    {
        FXaudioEventSO.OnEventRaise += OnFXEvent;
        BGMaudioEventSO.OnEventRaise += OnBGMEvent;
        VolumeEvent.OnEventRaised += VoiceChange;
        PauseEvent.OnEventRaised += loadVoiceVle;
    }

    

    private void OnDisable()
    {
        FXaudioEventSO.OnEventRaise -= OnFXEvent;
        BGMaudioEventSO.OnEventRaise -= OnBGMEvent;
        VolumeEvent.OnEventRaised -= VoiceChange;
        PauseEvent.OnEventRaised -= loadVoiceVle;
    }

    private void loadVoiceVle()
    {
        float amount;
        mixer.GetFloat("MasterVolume",out amount);
        syncVolumeEvent.OnEventRaised(amount);
    }

    private void VoiceChange(float amount)
    {
        mixer.SetFloat("MasterVolume",amount*100-80);
    }
    private void OnBGMEvent(AudioClip clip)
    {
        BGMaudio.clip = clip;
        BGMaudio.Play();
    }

    private void OnFXEvent(AudioClip clip)
    {
        FXaudio.clip = clip;
        FXaudio.Play();
    }
}
