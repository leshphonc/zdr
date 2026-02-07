public class BigGay : Enemy,IDamageable
{
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
        // 捡炸弹逻辑
    }
}
