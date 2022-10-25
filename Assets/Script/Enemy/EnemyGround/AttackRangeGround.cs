using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeGround : MonoBehaviour
{
    public float timeRespawn = 30f;
    float countDown;
    GameObject Enemy;
    GameObject LeftTarget;
    GameObject RightTarget;
    GroundEnemyAI Setter;
    bool right = true;
    bool hasTarget = false;
    bool changeTarget = true;
    bool canChange = true;
    GameObject Player;

    Vector3 position;

    private void Awake()
    {
        Enemy = gameObject.transform.GetChild(0).gameObject;
        LeftTarget = gameObject.transform.GetChild(1).gameObject;
        RightTarget = gameObject.transform.GetChild(2).gameObject;
        Setter = Enemy.GetComponent<GroundEnemyAI>();
        Player = GameObject.FindGameObjectWithTag("Player");
        position = RightTarget.transform.position;
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RunAround();

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {

            CheckPlayerDie();

            CheckEnemyDie();

            if (changeTarget)
            {
                changeTarget = false;
                if (!hasTarget)
                {
                    if (right)
                    {
                        position = RightTarget.transform.position;
                        Setter.targetObject = RightTarget;
                        right = false;
                    }
                    else
                    {
                        position = LeftTarget.transform.position;
                        Setter.targetObject = LeftTarget;
                        right = true;
                    }
                }
                else
                {
                    Setter.targetObject = Player;
                }
            }
        }
    }

    void CheckPlayerDie()
    {
        if (Player.tag != "Player" && hasTarget)
        {
            hasTarget = false;
            changeTarget = true;
        }
    }

    void CheckEnemyDie()
    {
        GroundEnemyAI enemy = Enemy.GetComponent<GroundEnemyAI>();
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
            }
        }
    }

    void RunAround()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Camera" && countDown <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            transform.GetChild(0).transform.position = (transform.GetChild(1).transform.position + transform.GetChild(2).transform.position) / 2;
        }

        if (collision.gameObject == Player)
        {
            changeTarget = true;
            hasTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.transform.tag == "Camera")
        {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
        }

        if (collision.gameObject == Player)
        {
            changeTarget = true;
            hasTarget = false;
        }
    }
}
