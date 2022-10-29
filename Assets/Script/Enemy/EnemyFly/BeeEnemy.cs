using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BeeEnemy : EnemyFly
{
    GameObject Player;
    AIDestinationSetter Setter;
    private bool hasTarget = true;
    private bool change = true;

    protected override void Start()
    {
        base.Start();
        Setter = GetComponent<AIDestinationSetter>();
        Player = GameObject.FindGameObjectWithTag("Player");
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }

    public override int Attack()
    {
        return data.Attack.Value;
    }

    protected void Update()
    {
        if (SceneManager.GetActiveScene().name != "Forest")
        {
            gameObject.SetActive(false);
        }

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            CheckPlayerDie();

            if (change)
            {
                if (hasTarget)
                {
                    Setter.target = Player.transform;
                }
                else
                {
                    Setter.target = transform;
                }
            }


        }
    }

    void CheckPlayerDie()
    {
        bool oldTarget = hasTarget;

        if (Player.tag != "Player")
        {
            hasTarget = false;
        } else
        {
            hasTarget = true;
        }

        if(oldTarget == hasTarget && Setter.target != null)
        {
            change = false;
        }
        else { change = true; }
    }


    public override void AfterTrigger(GameObject player)
    {
        float x = (player.transform.position - transform.position).normalized.x;
        if (x < -1.3) x = -1.3f;
        if (x > 1.3) x = 1.3f;
        transform.position = transform.position + new Vector3(-x, 2, 0);

        player.transform.position = player.transform.position + new Vector3(x, -0.5f, 0);
    }

    protected override bool canAttack(GameObject player)
    {
        return transform.position.y > player.transform.position.y;
    }

    protected override bool isAttacked(GameObject player)
    {
        return transform.position.y < player.transform.position.y;
    }


}
