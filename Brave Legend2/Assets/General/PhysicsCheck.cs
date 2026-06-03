 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PhycicsCheck : MonoBehaviour
{
    public bool manul;
    public bool leftTouchWall;
    public bool rightTouchWall;
    public bool isGround;
    public float checkRaduis;
    public LayerMask GroundLayer;
    public CapsuleCollider2D coll;
    public Vector2 bottomOffset;
    public Vector2 leftOffest;
    public Vector2 rightOffest;

    public Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        /*coll = GetComponent<CapsuleCollider2D>();
        if (!manul)
        {
            rightOffest = new Vector2(coll.bounds.size.x/2 + coll.offset.x, coll.bounds.size.y / 2);
            leftOffest = new Vector2(-rightOffest.x,rightOffest.y);
        }*/
    }
    private void Update()
    {
        check();
    }
    
    private void check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, GroundLayer);
        leftTouchWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffest, checkRaduis, GroundLayer);
        rightTouchWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffest, checkRaduis, GroundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffest, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffest, checkRaduis);
    }
}
