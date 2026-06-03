using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public bool fadeIn;
    public UnityAction<Color,float,bool> OnRaiseEvent;
    public void FadeIn(float duration)
    {
        RaiseEvent(Color.black,duration,true);
    }
    public void FadeOut(float duration)
    {
        RaiseEvent(Color.clear, duration, false);
    }
    public void RaiseEvent(Color target,float duration,bool fadeIn)
    {
        OnRaiseEvent?.Invoke(target,duration,fadeIn);
    }
}
