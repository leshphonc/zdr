using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private  EnemyBaseState _currentState;
    
    public Animator anim;
    public int animState;
    
    [Header("Enemy State")]
    public float health;
    public bool isDead;
    public bool hasBomb;

    private GameObject _alarmSign;
    
    [Header("Movement")]
    public Transform pointA;
    public Transform pointB;
    public Transform targetPoint;
    public float speed;

    public List<Transform> attackList = new List<Transform>();

    public readonly PatrolState PatrolState = new PatrolState();
    public readonly AttackState AttackState = new AttackState();
    
    
    [Header("Attack Setting")]
    public float attackRate;
    public float attackRange, skillRange;

    private float nextAttack = 0;

    public virtual void Init()
    {
        anim = GetComponent<Animator>();
        _alarmSign = transform.GetChild(0).gameObject;
    }

    public void Awake()
    {
        Init();
    }

    void Start()
    {
        TransitionToState(PatrolState);
    }

  public  virtual void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
            return;
        _currentState.OnUpdate(this);
        anim.SetInteger("state", animState);
    }

    public void TransitionToState(EnemyBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
    
    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FilpDirection();
    }


   public void SwitchTargetPoint()
    {
        if (Mathf.Abs(pointA.position.x - transform.position.x) > Mathf.Abs(pointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;
        }
    }

    void FilpDirection()
    {
        if (transform.position.x < targetPoint.position.x)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else 
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    public virtual void AttackAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                // 播放攻击
                anim.SetTrigger("attack");
                nextAttack = Time.time + attackRange;
            }
        }
    }
    
    public virtual void SkillAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextAttack)
            {
                // 播放攻击
                anim.SetTrigger("skill");
                nextAttack = Time.time + attackRange;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!attackList.Contains(other.transform) && !hasBomb && !isDead)
            attackList.Add(other.transform);
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        attackList.Remove(other.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead)
            StartCoroutine(OnAlarm());
    }


    IEnumerator OnAlarm()
    {
        _alarmSign.SetActive(true);
        yield return new WaitForSeconds(_alarmSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        _alarmSign.SetActive(false);
    }
    
}
