using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ObjectPooler;
using Random = UnityEngine.Random;

public class GroundEnemyAI : Enemy
{
    [HideInInspector] public GameObject targetObject;
    [SerializeField] float speed;
    Rigidbody2D rg;
    private bool faceRight = false;

    protected override void Start()
    {
        base.Start();
        rg = GetComponent<Rigidbody2D>();
        data.currentHealth = data.Health.Value;
        pooler = ObjectPooler.instance;
    }

    private void FixedUpdate()
    {
        if (targetObject != null) {
            float directionX = (targetObject.transform.position - transform.position).normalized.x;
            //rg.velocity = new Vector3(directionX, rg.gravityScale * Time.fixedDeltaTime, 0) * speed;
            if (Mathf.Abs(rg.velocity.x) < 3 || rg.velocity.x * directionX < 0)
                rg.AddForce(new Vector3(directionX, 0, 0) * speed);
            RotationFace(directionX);
        }
    }

    private void RotationFace(float move)
    {
        if (move > 0 && !faceRight)
        {
            Flip();
        }

        else if (move < 0 && faceRight)
        {
            Flip();
        }
    }


    private void Flip()
    {
        faceRight = !faceRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
