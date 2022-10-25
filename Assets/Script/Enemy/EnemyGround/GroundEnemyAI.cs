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

    private void Start()
    {
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

    private int Attack()
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

    protected virtual bool canAttack(GameObject player)
    {
        return true;
    }

    protected virtual bool isAttacked(GameObject player)
    {
        return true;
    }

    public override void TakeDamage(int damage)
    {
        int rate = Random.Range(0, 100);
        if (rate < data.Dodge.Value)
        {
            StatusAttack.instance.ShowMess("Attack Enemy Miss", Color.black);
            return;
        }

        damage -= data.Defense.Value;

        data.currentHealth -= Mathf.Clamp(damage, 1, int.MaxValue);

        if (data.currentHealth <= 0)
        {
            data.currentHealth = 0;
            Die();
        }
    }

    protected void Die()
    {
        gameObject.SetActive(false);
        PlayerStats player = PlayerStats.instance;
        player.currentExperience += data.Experience;
        StatusAttack.instance.ShowMess("+ " + data.Experience, Color.green);
        if (player.currentExperience >= player.Experience.Value)
        {
            player.currentExperience -= player.Experience.Value;
            player.Level += 1;
            player.Experience.AddModifier(new Goddess.PlayerStat.Stat(50, Goddess.PlayerStat.StatType.PercentMut));
            player.Score += player.Level;
        }

        GameObject Player = PlayerManager.instance.player;
        GameObject newSelection = pooler.SpawnFromPool("Mark", Player.transform.position, Quaternion.identity);
        newSelection.transform.SetParent(Player.transform);
        newSelection.SetActive(false);

        DropItem();
    }

    private void DropItem()
    {
        if (Random.Range(1, 100) <= 50)
        {
            int length = pooler.poolDictionary.Count;
            int id = Random.Range(0, length);

            String key = pooler.poolDictionary.ElementAt(id).Key;

            GameObject drop = pooler.SpawnFromPool(key, transform.position, Quaternion.identity);
        }
    }

}
