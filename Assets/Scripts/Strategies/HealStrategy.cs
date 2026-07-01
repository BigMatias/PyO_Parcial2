using UnityEngine;

public class HealStrategy : ActionStrategy
{
    private int healAmount;
    private int healRange;
    public override string ActionName => "Heal";
    
    public HealStrategy(int healAmount, int healRange)
    {
        this.healAmount = healAmount;
        this.healRange = healRange;
        ActionType = ActionType.Heal;
    }

    public override bool CanAct(ActionType requestedAction, Vector2Int attackerPos, Vector2Int targetPos)
    {
        if (requestedAction != ActionType) return false;

        int dx = Mathf.Abs(attackerPos.x - targetPos.x);
        int dy = Mathf.Abs(attackerPos.y - targetPos.y);
        int distance = dx + dy;
        return distance <= healRange;
    }

    public override void Act(Character attacker, Character target)
    {
        target.Heal(healAmount);
    }
}