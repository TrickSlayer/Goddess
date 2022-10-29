using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Search.SearchColumn;

public class AttackRangeFly : EnemyAttackRange
{
    AIDestinationSetter Setter;
    Enemy SetterEnemy;
    bool right = true;

    private void Awake()
    {
        Setter = Enemy.GetComponent<AIDestinationSetter>();
        SetterEnemy = Enemy.GetComponent<EagleEnemy>();

        if (SetterEnemy == null)
        {
            Enemy.GetComponent<BeeEnemy>();
        }

    }

    protected override void Update()
    {
        base.Update();

        RunAround();

        if (changeTarget)
        {
            changeTarget = false;
            if (!hasTarget)
            {
                if (right)
                {
                    position = RightTarget.transform.position;
                    SetterEnemy.targetObject = RightTarget;
                    Setter.target = RightTarget.transform;
                    right = false;
                }
                else
                {
                    position = LeftTarget.transform.position;
                    SetterEnemy.targetObject = LeftTarget;
                    Setter.target = LeftTarget.transform;
                    right = true;
                }
            }
            else
            {
                Setter.target = Player.transform;
                SetterEnemy.targetObject = Player;
            }
        }
    }

    protected override void ContinueUpdateHasPlayer()
    {
        base.ContinueUpdateHasPlayer();

        CheckPlayerDie();

    }

}
