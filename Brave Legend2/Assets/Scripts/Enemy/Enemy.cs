using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("基本属性")]
    [HideInInspector] public float moveRadios = 4f;
    public float dectectRadius;
    [HideInInspector] public Vector3 flyRandomDir;
    [HideInInspector] public Vector3 opos;
    [HideInInspector] public float timer;

    public float atForce;
    public float chaseSpeed;
    public float normalSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    [Header("组件")]
    public GameObject Player;
    public Transform attacker;
    [HideInInspector]public PhycicsCheck phycicsCheck;
    [HideInInspector]public Animator anim;
    [HideInInspector]public Rigidbody2D rb;
    [Header("撞墙等待计时器")]
    public float currentWaitTime;
    public float waitTime;
    public bool wait;
    [Header("状态切换计时器")]
    public float lostTime;
    public float maxTime;
    [Header("状态")]
    public bool ishurt;
    public bool isDead;
    [Header("检测")]
    public Vector2 centerOffest;
    public Vector2 checkSize;
    public LayerMask attackerLayer;
    public float checkDistance;

    protected BaseState currentState;
    protected BaseState PatrolState;
    protected BaseState chaseState;
    

    protected virtual void Awake()
    {
        
        currentWaitTime = waitTime;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        phycicsCheck = GetComponent<PhycicsCheck>();
        currentSpeed = normalSpeed;
    }
    protected virtual void Start()
    {
        flyRandomDir = new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0);
        opos = transform.position;
    }
    private void OnEnable()
    {
        currentState = PatrolState;
        currentState.OnEnter(this);
    }
    protected virtual void Update()
    {
        Player = GameObject.FindWithTag("Player");
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        WaitCounter();

    }
    private void FixedUpdate()
    {
        if(!wait && !ishurt && !isDead)
        Move();
        currentState.PhycicsUpdate();
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }
    public virtual void Move()
    {
        rb.velocity = new Vector3(currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }
    public void WaitCounter() 
    {
        if (wait)
        {
            currentWaitTime -= Time.deltaTime;
            if (currentWaitTime<=0)
            {
                wait = false;
                currentWaitTime = waitTime;
                transform.localScale = new Vector3(faceDir.x,1,1);
            }
        }
    }
    public virtual void onTakeDamage(Transform AttackTrans)
    {
        attacker = AttackTrans;
        if (attacker.transform.position.x - transform.position.x< 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        if (attacker.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        ishurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x-attacker.transform.position.x,0).normalized;
        StartCoroutine(onHurt(dir));
    }
    protected IEnumerator onHurt(Vector2 dir)
    {
        rb.AddForce(dir * atForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        ishurt = false;
    }
    public void onDie()
    {
        gameObject.layer = 2;
        anim.SetBool("dead",true);
        isDead = true;
    }
    public virtual void Destroy()
    {
        Destroy(this.gameObject); 
    }
    public void SwitchState(NPCstate state)
    {
        var newState = state switch
        {
            NPCstate.Patrol => PatrolState,
            NPCstate.Chase => chaseState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position+(Vector3)centerOffest + new Vector3(-transform.localScale.x*checkDistance, 0,0),0.2f);
    }
    public virtual bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position+(Vector3)centerOffest,checkSize,0,faceDir,checkDistance,attackerLayer);
    }
    public virtual void TimeCounter()
    {
        lostTime -= Time.deltaTime;
        if (lostTime<=0)
        {
            SwitchState(NPCstate.Patrol);
        }
    }
}
