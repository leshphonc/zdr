using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 2;
        enemy.targetPoint = enemy.attackList[0];
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (enemy.attackList.Count == 0)
            enemy.TransitionToState(enemy.PatrolState);

        if (enemy.attackList.Count > 1)
        {
            foreach (Transform item in enemy.attackList)
            {
                if (Mathf.Abs(enemy.transform.position.x - item.transform.position.x) <
                    Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x))
                {
                    enemy.targetPoint = item;
                }
            }
        }
        else if(enemy.attackList.Count == 1)
            enemy.targetPoint = enemy.attackList[0];
        
        if (enemy.targetPoint.CompareTag("Player"))
            enemy.AttackAction();
        
        if (enemy.targetPoint.CompareTag("Bomb"))
            enemy.SkillAction();
        
        enemy.MoveToTarget();
    }
}
