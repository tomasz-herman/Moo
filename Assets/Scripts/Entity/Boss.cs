using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public override EnemyTypes EnemyType => EnemyTypes.Boss;

    protected override void Start()
    {
        base.Start();
    }
}
