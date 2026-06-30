using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public PlayerType playerType;

    [Header("Melee")]
    [SerializeField] private int meleeDamage;

    [Header("Ranged")]
    [SerializeField] private bool hasRanged;
    [SerializeField] private int rangedDamage;
    [SerializeField] private int rangedMinRange;
    [SerializeField] private int rangedMaxRange;

    [Header("Heal")]
    [SerializeField] private int healAmount;
    [SerializeField] private int healRange;

    protected override void Awake()
    {
        base.Awake();

        actionStrategies = new List<ActionStrategy>
        {
            new MeleeAttackStrategy(meleeDamage),
            new HealStrategy(healAmount, healRange)
        };

        if (hasRanged)
            actionStrategies.Add(new RangedAttackStrategy(rangedDamage, rangedMinRange, rangedMaxRange));
    }
}