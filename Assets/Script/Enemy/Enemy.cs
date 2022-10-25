using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    protected ObjectPooler pooler;

    public virtual void TakeDamage(int damage)
    {
    }
}
