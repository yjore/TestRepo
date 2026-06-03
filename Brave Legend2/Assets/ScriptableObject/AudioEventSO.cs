using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/AudioEventSO")]
public class AudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaise;

    public void RaiseEvent(AudioClip Clip)
    {
        OnEventRaise?.Invoke(Clip);
    }
}
