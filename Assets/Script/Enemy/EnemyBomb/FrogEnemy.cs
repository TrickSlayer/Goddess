 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrogEnemy : Enemy
{

    public override int Attack()
    {
        return data.currentHealth;
    }

}
