using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int maxHP;
    [SerializeField] protected int speed;

    protected int currentHP;
    protected List<ActionStrategy> actionStrategies;
    
    public Vector2Int GridPosition { get; set; }
    public int CurrentHP => currentHP;
    public int Speed => speed;
    public bool IsAlive => currentHP > 0;

    public event Action<int> OnHPChanged;
    
    protected virtual void Awake()
    {
        currentHP = maxHP;
    }


    public virtual void TakeDamage(int amount)
    {
        currentHP -= amount;
        OnHPChanged?.Invoke(currentHP);
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        OnHPChanged?.Invoke(currentHP);
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public bool TryPerformAction(ActionType actionType, Character target)
    {
        foreach (ActionStrategy strategy in actionStrategies)
        {
            if (strategy.CanAct(actionType, GridPosition, target.GridPosition))
            {
                strategy.Act(this, target);
                return true;
            }
        }
        return false;
    }

    public List<ActionStrategy> GetActionStrategies() => actionStrategies;
}