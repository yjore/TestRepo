using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    public FadeEventSO FadeEventSO;
    public Image FadeImage;
    private void OnEnable()
    {
        FadeEventSO.OnRaiseEvent += OnFadeRaiseEvent;
    }
    private void OnDisable()
    {
        FadeEventSO.OnRaiseEvent -= OnFadeRaiseEvent;
    }
    public void OnFadeRaiseEvent(Color target, float duration,bool fadeIn)
    {
        FadeImage.DOBlendableColor(target,duration);
    }
}
