using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public float timeRespawn = 30f;
    float countDown;
    GameObject Enemy;
    GameObject LeftTarget;
    GameObject RightTarget;
    AIDestinationSetter Setter;
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
        Setter = Enemy.GetComponent<AIDestinationSetter>();
        Player = GameObject.FindGameObjectWithTag("Player");
        position = RightTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RunAround();

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
                    Setter.target = RightTarget.transform;
                    right = false;
                } else
                {
                    position = LeftTarget.transform.position;
                    Setter.target = LeftTarget.transform;
                    right = true;
                }
            } else
            {
                Setter.target = Player.transform;
            }
        }
    }

    void CheckPlayerDie()
    {
        if(Player.tag != "Player" && hasTarget)
        {
            hasTarget = false;
            changeTarget = true;
        }
    }

    void CheckEnemyDie()
    {
        if (!transform.GetChild(0).gameObject.activeInHierarchy)
        {
            if(countDown <= 0)
            {
                countDown = timeRespawn;
            }
            countDown -= Time.deltaTime;
            Debug.Log(countDown);
            if (countDown <= 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    void RunAround()
    {
        if (Mathf.Abs(Enemy.transform.position.x - position.x) < 0.1f && Mathf.Abs(Enemy.transform.position.y - position.y) < 0.1f)
        {
            if (canChange)
            {
                canChange = false;
                changeTarget = true;
            }
        } else
        {
            canChange = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            changeTarget = true;
            hasTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            changeTarget = true;
            hasTarget = false;
        }
    }
}
