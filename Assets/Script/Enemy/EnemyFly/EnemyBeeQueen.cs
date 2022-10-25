using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBeeQueen : EnemyFly
{

    [SerializeField] List<Transform> Points;
    AIDestinationSetter Setter;
    bool changeTarget = true;
    bool canChange = true;

    Transform pos;
    float time = 30f;
    float countDown = 0f;

    protected override void Start()
    {
        base.Start();
        Setter = GetComponent<AIDestinationSetter>();
        pos = transform;
        countDown = time;
    }

    void Update()
    {
        RunAround();

        CheckEnemyDie();

        if (changeTarget)
        {
            changeTarget = false;
            Setter.target = pos;
        }

    }

    private void FixedUpdate()
    {
        SpawnCreep();
    }

    void SpawnCreep()
    {
        if (countDown <= 0)
        {
            countDown = time;
            float x = Random.Range(-1, 1);
            float y = Random.Range(-1, 1);
            pooler.SpawnFromPool("Bee", transform.position + new Vector3(x, y, 0), Quaternion.identity);
        } else
        {
            countDown -= Time.fixedDeltaTime;
        }
    }

    void RunAround()
    {

        if (Mathf.Abs(transform.position.x - pos.position.x) < 0.1f && Mathf.Abs(transform.position.y - pos.position.y) < 0.1f)
        {
            if (canChange)
            {
                canChange = false;
                changeTarget = true;

                int i;
                do
                {
                    i = Random.Range(0, Points.Count - 1);
                } while (Points[i].position == pos.position);

                pos = Points[i];
            }
        }
        else
        {
            canChange = true;
        }
    }

    void CheckEnemyDie()
    {
        /*        EnemyFly enemy = GetComponent<EnemyFly>();
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
                }*/
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
