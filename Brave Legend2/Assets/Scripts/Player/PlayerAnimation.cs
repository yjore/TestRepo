using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PhycicsCheck phycicsCheck;
    public Animator anim;
    public Rigidbody2D rb;
    public PlayerControl playerControl;
    private void Awake()
    {
        phycicsCheck = GetComponent<PhycicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerControl = GetComponent<PlayerControl>();
    }
    private void Update()
    {
        setActive();
    }

    public void setActive()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isClimb",playerControl.isCliming);
        anim.SetBool("isGround", phycicsCheck.isGround);
        anim.SetBool("isDead", playerControl.isDead);
        anim.SetBool("isAttack", playerControl.isAttack);
        anim.SetBool("isSprint",playerControl.isSprint);
    }
    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }
    public void PlayAttack()
    {
        anim.SetTrigger("attack");
        
        
    }
}
