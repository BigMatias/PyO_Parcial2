using UnityEngine;

public class MeleeAttackStrategy : ActionStrategy
{
    private int damage;
    public override string ActionName => "Melee";
    
    public MeleeAttackStrategy(int damage)
    {
        this.damage = damage;
        ActionType = ActionType.MeleeAttack;
    }

    public override bool CanAct(ActionType requestedAction, Vector2Int attackerPos, Vector2Int targetPos)
    {
        if (requestedAction != ActionType) return false;

        int dx = Mathf.Abs(attackerPos.x - targetPos.x);
        int dy = Mathf.Abs(attackerPos.y - targetPos.y);
        int distance = Mathf.Max(dx, dy);

        return distance == 1;
    }
    
    public override void Act(Character attacker, Character target)
    {
        target.TakeDamage(damage);
    }
}