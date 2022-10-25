using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    protected ObjectPooler pooler;

    protected virtual void Start()
    {
        data.currentHealth = data.Health.Value;
        pooler = ObjectPooler.instance;
    }

    public virtual int Attack()
    {
        return 1;
    }

    protected virtual bool canAttack(GameObject player)
    {
        return true;
    }

    protected virtual bool isAttacked(GameObject player)
    {
        return true;
    }

    public virtual void TakeDamage(int damage)
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

    protected virtual void Die()
    {
        gameObject.SetActive(false);

        PlayerStats player = PlayerStats.instance;
        player.gainExperience(data.Experience);

        StatusAttack.instance.ShowMess("+ " + data.Experience, Color.green);

        gameObject.transform.Find("Mark(Clone)").gameObject.SetActive(false);

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
