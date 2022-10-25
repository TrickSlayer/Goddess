using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrogEnemy : Enemy
{

    private void Start()
    {
        data.currentHealth = data.Health.Value;
        pooler = ObjectPooler.instance;
    }


    public int Attack()
    {
        return data.currentHealth;
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
