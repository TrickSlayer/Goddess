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

    private int Attack()
    {
        return data.Attack.Value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 position = transform.position;
            float directX = transform.position.x - collision.gameObject.transform.position.x;
            Vector3 direct = new Vector3(directX, 2, 0);
            transform.position =  position + direct;
            if (canAttack())
            {
                PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
                player.TakeDamage(Attack());

            } 

            if (isAttacked())
            {
                TakeDamage(Attack());
            }

        }
    }

    protected virtual bool canAttack()
    {
        return true;
    }

    protected virtual bool isAttacked()
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
