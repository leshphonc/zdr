using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator _anim;
    private Collider2D _coll; 
    private Rigidbody2D _rb;
    
    public float startTime;
    public float waitTime;
    public float bombForce;
    
    [Header("Check")]
    public float radius;
    public LayerMask targetLayer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))
        {
            if (Time.time - startTime > waitTime)
            {
                _anim.Play("bomb_explosion");
            } 
        }
       
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    public void Explosion()
    {
        _coll.enabled = false;
        Collider2D[] aroundObject = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        _rb.gravityScale = 0;
        
        foreach (Collider2D item in aroundObject)
        {
            Vector3 pos = transform.position - item.transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((-pos+ Vector3.up)*bombForce, ForceMode2D.Impulse);

            if (item.CompareTag("Bomb") && item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))
            {
                item.GetComponent<Bomb>().TurnOn();
            }
            if (item.CompareTag("Player"))
                item.GetComponent<IDamageable>().GetHit(3);
        }
    }
    
    public void DestoryThis()
    {
        Destroy(gameObject);
    }

    public void TurnOff()
    {
        _anim.Play("bomb_off");
        gameObject.layer = LayerMask.NameToLayer("NPC");
    }


    public void TurnOn()
    {
        _anim.Play("bomb_on");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
        startTime = Time.time;
    }
}
