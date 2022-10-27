using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackRangeBomb : EnemyAttackRange
{
    Animator animator;

    protected override void Update()
    {
        base.Update();

        if (inRange && Enemy.activeInHierarchy && !clone)
        {
            clone = true;
            StartCoroutine(popDownAnimation());
            playerManager.statsP.TakeDamage(Enemy.GetComponent<FrogEnemy>().Attack());
        }
    }

    IEnumerator popDownAnimation()
    {
        animator = Enemy.GetComponent<Animator>();
        animator.SetBool("isDie", true);
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Enemy.transform.localScale = new Vector3(20, 20, 1);
        yield return new WaitForSecondsRealtime(animationLength);
        FrogEnemy enemy = Enemy.GetComponent<FrogEnemy>();
        enemy.currentHealth = 0;
        animator.SetBool("isDie", false);
        Enemy.transform.localScale = new Vector3(8, 8, 1);
        Enemy.SetActive(false);
    }

}
