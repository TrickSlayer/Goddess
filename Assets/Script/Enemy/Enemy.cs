using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public EnemyData data;

    private void Start()
    {
        data.currentHealth = data.Health.Value;
    }

    private void Update()
    {
    }

    private int Attack()
    {
        return data.Attack.Value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
                TakeDamage(Attack());
            }

            AfterTrigger(player);
        }
    }

    public virtual void AfterTrigger(GameObject player)
    {

    }

    protected virtual bool canAttack(GameObject player)
    {
        return true;
    }

    protected virtual bool isAttacked(GameObject player)
    {
        return true;
    }

    public void TakeDamage(int damage)
    {
        Random ran = new System.Random();
        int rate = ran.Next(0, 100);
        if (rate < data.Dodge.Value)
        {
            Debug.Log("Attack Enemy Miss");
            return;
        }

        damage -= data.Defense.Value;

        data.currentHealth -= Mathf.Clamp(damage, 0, int.MaxValue);

        if(data.currentHealth <= 0)
        {
            data.currentHealth = 0;
            Die();
        }
    }

    protected void Die()
    {
        gameObject.SetActive(false);
        data.currentHealth = data.Health.Value;
    }

}
