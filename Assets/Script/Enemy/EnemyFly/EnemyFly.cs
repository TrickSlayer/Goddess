using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFly : Enemy
{

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Selectable selectable = GetComponent<Selectable>();
            selectable.Selected();

            GameObject player = collision.gameObject;

            if (canAttack(player))
            {
                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                playerStats.TakeDamage(Attack());

            }

            if (isAttacked(player))
            {
                TakeDamage(PlayerStats.instance.AttackEnemy());
            }

            AfterTrigger(player);
        }
    }

    public virtual void AfterTrigger(GameObject player)
    {

    }

}
