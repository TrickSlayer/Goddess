using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumEnemy : GroundEnemyAI
{
    protected override bool canAttack(GameObject player)
    {
        return transform.position.y + 1 > player.transform.position.y;
    }

    protected override bool isAttacked(GameObject player)
    {
        return transform.position.y + 1 < player.transform.position.y;
    }
}
