public class Whale:Enemy, IDamageable
{

    public float scale;
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

    public void Swallow()
    {
        targetPoint.GetComponent<Bomb>().TurnOff();
        targetPoint.gameObject.SetActive(false);
        
        
        transform.localScale *= scale;
        attackRange *= scale;
    }
}