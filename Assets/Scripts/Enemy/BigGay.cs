using UnityEngine;

public class BigGay : Enemy,IDamageable
{
    
    public Transform pickupPoint;
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

    public void PickupBomb()
    {
        if (targetPoint.CompareTag("Bomb") && !hasBomb)
        {
            targetPoint.position = pickupPoint.position;
            targetPoint.SetParent(pickupPoint);
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            hasBomb = true;
        }
    }
    
    public void ThrowAway()
    {
        if (hasBomb)
        {
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            targetPoint.SetParent(transform.parent.parent);

            if (FindObjectOfType<PlayerController>().gameObject.transform.position.x - targetPoint.position.x < 0)
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * 8, ForceMode2D.Impulse);
            }
            else
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * 8, ForceMode2D.Impulse);
            }
            
        }

        hasBomb = false;

    }
}
