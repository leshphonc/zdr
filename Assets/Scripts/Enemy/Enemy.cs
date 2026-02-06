using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private  EnemyBaseState _currentState;
    
    public Animator anim;
    public int animState;
    [Header("Movement")]
    public Transform pointA;
    public Transform pointB;
    public Transform targetPoint;
    public float speed;

    public List<Transform> attackList = new List<Transform>();

    public readonly PatrolState PatrolState = new PatrolState();
    public readonly AttackState AttackState = new AttackState();

    public virtual void Init()
    {
        anim = GetComponent<Animator>();
    }

    public void Awake()
    {
        Init();
    }

    void Start()
    {
        TransitionToState(PatrolState);
    }

    void Update()
    {
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

    public virtual void SkillAction()
    {
        Debug.Log("技能攻击");
    }

    public virtual void AttackAction()
    {
        Debug.Log("普通攻击");
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (!attackList.Contains(other.transform))
            attackList.Add(other.transform);
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        attackList.Remove(other.transform);
    }
}
