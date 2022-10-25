using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public GameObject target;
    CharacterController2D controller;
    PlayerManager manager;
    PlayerStats stats;
    Animator animator;
    ObjectPooler pooler;
    private float time = 1.8f;
    private float countDown = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = PlayerManager.instance;
        stats = manager.statsP;
        controller = manager.player.GetComponent<CharacterController2D>();
        pooler = ObjectPooler.instance;
        if (target == null)
        {
            if (controller.facingRight)
                rb.velocity = new Vector3(1, 0, 0) * speed;
            else
                rb.velocity = new Vector3(-1, 0, 0) * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity == Vector2.zero && target == null)
        {
            if (controller.facingRight)
                rb.velocity = new Vector3(1, 0, 0) * speed;
            else
                rb.velocity = new Vector3(-1, 0, 0) * speed;
        }
    }

    private void FixedUpdate()
    {

        if (gameObject.activeInHierarchy && countDown <= 0)
        {
            countDown = time;
        }

        if(countDown > 0)
        {
            countDown -= Time.fixedDeltaTime;
            if(countDown <= 0)
            gameObject.SetActive(false);
        }

        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StartCoroutine(popDownAnimation());

            if (collision.tag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(AttackEnemy());
            }
        }

    }

    public int AttackEnemy()
    {
        int damage = (int)Mathf.Round(stats.Attack.Value * 1.5f);
        int rate = Random.Range(0, 100);
        if (rate < stats.CritRate.Value)
        {
            damage *= stats.CritDamage.Value / 100;
        }

        return damage;
    }

    IEnumerator popDownAnimation()
    {
        rb.velocity /= 1000;
        impactEffect = pooler.SpawnFromPool("impactEffect", transform.position, Quaternion.identity);

        animator = impactEffect.GetComponent<Animator>();

        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength);

        gameObject.SetActive(false);
        impactEffect.SetActive(false);
    }
}
