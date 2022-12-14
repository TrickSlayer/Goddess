using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackRangeGround : EnemyAttackRange
{
    GroundEnemyAI Setter;
    bool right = true;

    private void Awake()
    {
        Setter = Enemy.GetComponent<GroundEnemyAI>();       
    }

    protected override void Update()
    {
        RunAround();

        base.Update();

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

    protected override void ContinueUpdateHasPlayer()
    {
        base.ContinueUpdateHasPlayer();

        CheckPlayerDie();

    }

}
