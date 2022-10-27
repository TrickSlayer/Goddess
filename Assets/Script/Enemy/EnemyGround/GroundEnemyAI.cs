using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ObjectPooler;
using Random = UnityEngine.Random;

public class GroundEnemyAI : Enemy
{
    [SerializeField] float speed;
    Rigidbody2D rg;


    protected override void Start()
    {
        base.Start();
        rg = GetComponent<Rigidbody2D>();
        pooler = ObjectPooler.instance;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (targetObject != null) {
            float directionX = (targetObject.transform.position - transform.position).normalized.x;
            if (Mathf.Abs(rg.velocity.x) < 3 || rg.velocity.x * directionX < 0)
                rg.AddForce(new Vector3(directionX, 0, 0) * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    public override int Attack()
    {
        return data.Attack.Value;
    }

    public virtual void AfterTrigger(GameObject player)
    {
        float x = (player.transform.position - transform.position).normalized.x;
        if (x < -1.3) x = -1.3f;
        if (x > 1.3) x = 1.3f;
        transform.position = transform.position + new Vector3(-x, 1, 0);

        player.transform.position = player.transform.position + new Vector3(x, -0.2f, 0);
    }

}
