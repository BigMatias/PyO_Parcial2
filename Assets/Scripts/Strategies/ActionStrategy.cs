using UnityEngine;

public class ActionStrategy
{
    public ActionType ActionType { get; protected set; }
    public virtual string ActionName => "Action";
    
    public virtual bool CanAct(ActionType requestedAction, Vector2Int attackerPos, Vector2Int targetPos)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Act(Character attacker, Character target)
    {
        throw new System.NotImplementedException();
    }
}