using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Enemy,IDamageable
{
    public override void SkillAction()
    {
        base.SkillAction();
    }

    public void GetHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
