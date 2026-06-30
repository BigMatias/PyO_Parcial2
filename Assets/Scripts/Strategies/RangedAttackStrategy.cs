using UnityEngine;

public class RangedAttackStrategy : ActionStrategy
{
    private int damage;
    private int minRange;
    private int maxRange;
    public override string ActionName => "Ranged";
    public RangedAttackStrategy(int damage, int minRange, int maxRange)
    {
        this.damage = damage;
        this.minRange = minRange;
        this.maxRange = maxRange;
        ActionType = ActionType.RangedAttack;
    }

    public override bool CanAct(ActionType requestedAction, Vector2Int attackerPos, Vector2Int targetPos)
    {
        if (requestedAction != ActionType) return false;

        float distance = Vector2Int.Distance(attackerPos, targetPos);
        return distance >= minRange && distance <= maxRange;
    }

    public override void Act(Character attacker, Character target)
    {
        target.TakeDamage(damage);
    }
}