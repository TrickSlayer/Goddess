using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public GameObject targetObject;
    public EnemyData data;
    public int currentHealth;
    public bool isDie = false;
    protected ObjectPooler pooler;
    private bool faceRight = false;
    
    private void Awake()
    {
        currentHealth = data.Health.Value;
    }

    protected virtual void Start()
    {
        pooler = ObjectPooler.instance;
    }

    protected virtual void FixedUpdate()
    {
        if (targetObject != null)
        {
            float directionX = (targetObject.transform.position - transform.position).normalized.x;
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

        currentHealth -= Mathf.Clamp(damage, 1, int.MaxValue);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDie = true;
            Die();
        }
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);

        PlayerStats player = PlayerStats.instance;
        player.gainExperience(data.Experience);

        StatusAttack.instance.ShowMess("+ " + data.Experience, Color.green);

        Transform mark = gameObject.transform.Find("Mark(Clone)");
        
        if(mark != null) mark.gameObject.SetActive(false);

        DropItem();
    }

    private void DropItem()
    {
        if (Random.Range(1, 100) <= 100)
        {
            List<int> idItems = pooler.getItemId();
            int id = Random.Range(0, idItems.Count);
            String key = pooler.poolDictionary.ElementAt(idItems[id]).Key;
            Debug.Log("Drop " + key);
            GameObject drop = pooler.SpawnFromPool(key, transform.position, Quaternion.identity);
        }
    }
}
