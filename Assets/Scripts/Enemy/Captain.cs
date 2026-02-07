using UnityEngine;

public class Captain:Enemy, IDamageable
{
    
    SpriteRenderer spriteRenderer;

    public override void Init()
    {
        base.Init();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();
        if (animState ==0)
            spriteRenderer.flipX = false;
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

    public override void SkillAction()
    {
        base.SkillAction();

        if (anim.GetCurrentAnimatorStateInfo(1).IsName("scare_run"))
        {
            spriteRenderer.flipX = true;
            if (transform.position.x > targetPoint.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position+Vector3.right, speed*2 * Time.deltaTime);
            }
            else
                transform.position = Vector2.MoveTowards(transform.position, transform.position+Vector3.left, speed*2 * Time.deltaTime);
                
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}