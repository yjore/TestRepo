using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    public bool OnPrass;
    public BraveLegend2 playerInput;
    public Transform Player;
    public Animator anim;
    public GameObject SignSprite;
    public IInteractable targetItem;
    private void Awake()
    {
        anim = SignSprite.GetComponent<Animator>();
        playerInput = new BraveLegend2();
        playerInput.Enable();

    }
    private void OnEnable()
    {
        playerInput.Player.Confirm.started += OnConfirm;
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (OnPrass)
        {
            targetItem.TriggerAction();
            GetComponent<AudioDefineion>()?.PlayOnClip();
        }
        
    }

    private void Update()
    {
        SignSprite.SetActive(OnPrass);
        SignSprite.transform.localScale = Player.localScale;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            OnPrass = true;
            targetItem = collision.GetComponent<IInteractable>();
        }
        else
        {
            OnPrass=false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            OnPrass=false;
        }
    }
}
