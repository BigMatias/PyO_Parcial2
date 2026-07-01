using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IPositionTrackable
{
    public EnemyType enemyType;

    [SerializeField] private int meleeDamage;
    [SerializeField] private int rangedDamage;
    [SerializeField] private int rangedMinRange;
    [SerializeField] private int rangedMaxRange;

    protected override void Awake()
    {
        base.Awake();

        actionStrategies = new List<ActionStrategy>
        {
            new MeleeAttackStrategy(meleeDamage),
            new RangedAttackStrategy(rangedDamage, rangedMinRange, rangedMaxRange)
        };
    }
    
    public void Unregister(PositionTracker positionTracker)
    {
        positionTracker.RemoveEnemy(enemyType);
    }
}