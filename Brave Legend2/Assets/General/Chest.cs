using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public SpriteRenderer ChestSprite;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;
    private void Awake()
    {
        ChestSprite = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        ChestSprite.sprite = isDone? openSprite : closeSprite;
    }
    public void TriggerAction()
    {
        Debug.Log("Opean Chest");
        if (!isDone)
        {
            openChest();
        }
        
    }
    public void openChest()
    {
        ChestSprite.sprite = openSprite;
        isDone = true;
        this.gameObject.tag = "Untagged";
    }
}
