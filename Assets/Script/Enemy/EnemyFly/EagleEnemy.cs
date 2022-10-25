using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleEnemy : EnemyFly
{
    public  override void AfterTrigger(GameObject player)
    {
        float x = (player.transform.position - transform.position).normalized.x;
        if (x < -1.3) x = -1.3f;
        if (x > 1.3) x = 1.3f;
        transform.position = transform.position + new Vector3(-x,2,0);

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
