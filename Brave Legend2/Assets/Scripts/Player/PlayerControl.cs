using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR;
public class PlayerControl : MonoBehaviour
{
    [Header("事件监听")]
    public SceneLoadEventSO inputSceneControl;
    public VoidEventSO AfterSceneInputControl;
    public VoidEventSO LoadDataEvent;
    public VoidEventSO backEvent;
    [Header("获取组件")]
    public PhycicsCheck phycicsCheck;
    public BraveLegend2 inputControl;
    public Rigidbody2D rb;
    public PlayerAnimation anim;
    public Vector2 inputDirection;
    [Header("基本参数")]
    public float wallSlideSpeed;
    public float MaxJumpCount = 2;
    public float CurrentJumpCount;
    public float Speed = 150;
    public float jumpForce = 1;
    public float HurtForce;
    public bool isHurt;
    public bool isAttack;
    public bool isDead;
    public bool isSprint;
    public bool isCliming;
    [Header("物理材质")]
    private Collider2D coll;
    public PhysicsMaterial2D Wall;
    public PhysicsMaterial2D Normal;

    public UnityEvent<PlayerControl> OntakeSprint;
    public void Awake()
    {
        phycicsCheck = GetComponent<PhycicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimation>();
        inputControl = new BraveLegend2();
        inputControl.Player.Jump.started += Jump;
        inputControl.Player.Attack.started += PlayerAttack;
        inputControl.Player.Sprint.started += Sprint;
        coll = GetComponent<CapsuleCollider2D>();
        inputControl.Enable();
    }

    

    private void Start()
    {
        OntakeSprint?.Invoke(this);
        CurrentJumpCount += MaxJumpCount;
    }


    private void OnEnable()
    {
        
        inputSceneControl.LoadRequestEvent += InputUnload;
        AfterSceneInputControl.OnEventRaised += InputLoad;
        LoadDataEvent.OnEventRaised += OnLoadDataEvent;
        backEvent.OnEventRaised += OnLoadDataEvent;
    }

    private void OnDisable()
    {
        inputControl.Disable();
        inputSceneControl.LoadRequestEvent -= InputUnload;
        AfterSceneInputControl.OnEventRaised -= InputLoad;
        LoadDataEvent.OnEventRaised -= OnLoadDataEvent;
        backEvent.OnEventRaised -= OnLoadDataEvent;
    }

    

    private void InputLoad()
    {
        inputControl.Enable();
    }

    private void InputUnload(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControl.Disable();
    }
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        Move();
        climing();

    }

    public void Update()
    {
        CheckMaterial();
        inputDirection = inputControl.Player.Move.ReadValue<Vector2>();
        

    }

    private void Sprint(InputAction.CallbackContext context)
    {
        if(PlayerStatBar.Instance.HealthFillYellow.fillAmount >= 0.5)
        isSprint = true;
        OntakeSprint?.Invoke(this);
    }
    private void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * Speed * Time.deltaTime, rb.velocity.y);
        if (inputDirection.x != 0)
        {
            transform.localScale = new Vector2((float)Math.Round(inputDirection.x), transform.localScale.y);

        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (phycicsCheck.isGround)
        {
            CurrentJumpCount = MaxJumpCount;
        }
        if (CurrentJumpCount > 0)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            GetComponent<AudioDefineion>()?.PlayOnClip();
            CurrentJumpCount--;
        }
    }

    public void climing()
    {
        if (phycicsCheck.leftTouchWall || phycicsCheck.rightTouchWall)
        {
            isCliming = true;
            rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed);
            CurrentJumpCount = MaxJumpCount;
        }
        else
        {
            isCliming = false;
        }
    }
    public void PlayerHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir*HurtForce,ForceMode2D.Impulse);
    }
    public void PlayerDead()
    {
        isDead = true;
        inputControl.Player.Disable();
    }
    public void PlayerAttack(InputAction.CallbackContext context)
    {
        isAttack = true;
        anim.PlayAttack();
        
    }
    private void CheckMaterial()
    {
        coll.sharedMaterial = phycicsCheck.isGround ? Normal : Wall;
    }

    private void OnLoadDataEvent()
    {
        isDead = false;
    }
}
