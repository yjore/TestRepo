using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour , IInteractable
{
    [Header("广播")]
    public VoidEventSO saveGameEvent;
    [Header("组件获取")]
    public SpriteRenderer checkSprite;
    public Sprite darkSprite;
    public Sprite lightSprite;
    public GameObject LightObj;
    public bool isDone;
    
    private void OnEnable()
    {
        checkSprite.sprite = isDone ? lightSprite : darkSprite;
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            isDone = true;
            checkSprite.sprite = lightSprite;
            LightObj.SetActive(true);
            saveGameEvent.RaiseEvent();
            this.gameObject.tag = "Untagged";
        }
    }
}
