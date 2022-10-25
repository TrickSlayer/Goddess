using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackBombRange : MonoBehaviour
{
    public float timeRespawn = 30f;
    float countDown;
    Animator animator;
    GameObject Player;
    GameObject Enemy;
    PlayerManager playerManager;

    private bool clone = false;
    private bool inRange = false;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        Player = playerManager.player;
        Enemy = transform.GetChild(0).gameObject;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        CheckEnemyDie();

        if (inRange && Enemy.activeInHierarchy && !clone)
        {
            clone = true;
            StartCoroutine(popDownAnimation());
            playerManager.statsP.TakeDamage(Enemy.GetComponent<FrogEnemy>().Attack());
        }
    }

    void CheckEnemyDie()
    {

        FrogEnemy enemy = Enemy.GetComponent<FrogEnemy>();
        if (enemy.data.currentHealth <= 0 && !Enemy.activeInHierarchy)
        {
            if (countDown <= 0)
            {
                countDown = timeRespawn;
            }
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                enemy.data.currentHealth = enemy.data.Health.Value;
                transform.GetChild(0).gameObject.SetActive(true);
                clone = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "Camera" && countDown <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }


        if (collision.transform.tag == "Player" )
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Camera" && countDown <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (collision.transform.tag == "Player" && clone)
        {
            inRange = false;
            clone = false;
        }
    }

    IEnumerator popDownAnimation()
    {
        animator = Enemy.GetComponent<Animator>();
        animator.SetBool("isDie", true);
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Enemy.transform.localScale = new Vector3(20, 20, 1);
        yield return new WaitForSecondsRealtime(animationLength);
        FrogEnemy enemy = Enemy.GetComponent<FrogEnemy>();
        enemy.data.currentHealth = 0;
        animator.SetBool("isDie", false);
        Enemy.transform.localScale = new Vector3(8, 8, 1);
        Enemy.SetActive(false);
    }

}
