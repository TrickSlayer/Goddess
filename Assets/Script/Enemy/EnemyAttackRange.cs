using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttackRange : MonoBehaviour
{
    public float timeRespawn = 30f;
    public float countDown;
    public GameObject Enemy;
    public GameObject LeftTarget;
    public GameObject RightTarget;

    protected GameObject Player;
    protected PlayerManager playerManager;
    protected bool activeRevival = false;
    protected bool clone = false;
    protected bool inRange = false;
    protected bool changeTarget = true;
    protected bool hasTarget = false;
    protected Vector3 position;

    bool canChange = true;

    protected virtual void Start()
    {
        playerManager = PlayerManager.instance;
        if (playerManager != null)
        {
            Player = playerManager.player;
        }


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (RightTarget != null)
        {
            position = RightTarget.transform.position;
        }
    }

    protected virtual void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        } else
        {
            CheckEnemyDie();

            ContinueUpdateHasPlayer();
        }

    }

    protected virtual void ContinueUpdateHasPlayer()
    {

    }

    protected void CheckPlayerDie()
    {
        if (Player.tag != "Player" && hasTarget)
        {
            hasTarget = false;
            changeTarget = true;
        }
    }

    protected void RunAround()
    {
        if (Mathf.Abs(Enemy.transform.position.x - position.x) < 1f)
        {
            if (canChange)
            {
                canChange = false;
                changeTarget = true;
            }
        }
        else
        {
            canChange = true;
        }
    }

    protected void CheckEnemyDie()
    {
        Enemy enemy = Enemy.GetComponent<FrogEnemy>();

        if (enemy == null)
        {
            enemy = Enemy.GetComponent<GroundEnemyAI>();
        }

        if (enemy == null)
        {
            enemy = Enemy.GetComponent<EnemyFly>();
        }

        if (enemy.isDie && !Enemy.activeInHierarchy && countDown <= 0)
        {
            if (countDown <= 0)
            {
                countDown = timeRespawn;
            }
        }

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;

            if (countDown <= 0)
            {
                enemy.currentHealth = enemy.data.Health.Value;
                if (activeRevival)
                {
                    Enemy.SetActive(true);
                }
                enemy.isDie = false;
                clone = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "MainCamera" && countDown <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            activeRevival = true;
        }

        if (collision.transform.tag == "Player")
        {
            inRange = true;
            changeTarget = true;
            hasTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "MainCamera" && countDown <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            activeRevival = false;

            if (LeftTarget != null && RightTarget != null)
            {
                Enemy.transform.position = (LeftTarget.transform.position + RightTarget.transform.position) / 2;
            }

        }

        if (collision.transform.tag == "Player")
        {
            inRange = false;
            changeTarget = true;
            hasTarget = false;
            if (clone)
            {
                clone = false;
            }
        }
    }
}
