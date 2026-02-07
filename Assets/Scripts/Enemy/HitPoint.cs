using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("hit player");
            other.GetComponent<IDamageable>().GetHit(1);
        }
        else if (other.CompareTag("Bomb"))
        {
            Vector3 pos = transform.position - other.transform.position;
            other.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * 8f, ForceMode2D.Impulse);
        }
    }
    
}
