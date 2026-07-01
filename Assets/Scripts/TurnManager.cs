using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private MapManager mapManager;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private GameStateChecker gameStateChecker;
    
    public Character CurrentCharacter { get; private set; }
    public int MovesRemaining { get; private set; }
    public bool HasActed { get; private set; }

    public event Action<Character> OnTurnStarted;
    public event Action OnTurnEnded;

    private List<Character> turnOrder = new List<Character>();
    private int currentIndex = 0;

    private void Start()
    {
        BuildTurnOrder();
        StartTurn();
    }

    private void BuildTurnOrder()
    {
        foreach (GameObject p in players)
            turnOrder.Add(p.GetComponent<Character>());
        foreach (GameObject e in enemies)
            turnOrder.Add(e.GetComponent<Character>());
    }

    private void StartTurn()
    {
        while (!turnOrder[currentIndex].IsAlive)
            currentIndex = (currentIndex + 1) % turnOrder.Count;

        CurrentCharacter = turnOrder[currentIndex];
        MovesRemaining = CurrentCharacter.Speed;
        HasActed = false;

        OnTurnStarted?.Invoke(CurrentCharacter);

        if (CurrentCharacter is Enemy enemy)
        {
            PerformEnemyTurn(enemy);
            gameStateChecker.CheckGameState();
        }
    }

    public bool TryMoveCurrentCharacter(int dx, int dy)
    {
        if (HasActed || MovesRemaining <= 0) return false;
        if (!(CurrentCharacter is Player player)) return false;

        Vector2Int newPos = mapManager.GetPossibleNewPosition(player.playerType, dx, dy);
        if (!mapManager.CheckIsValidPosition(newPos)) return false;

        mapManager.MovePlayer(player.playerType, newPos);
        MovesRemaining--;
        return true;
    }

    public bool TryPerformAction(ActionType actionType, Character target)
    {
        if (HasActed) return false;

        bool success = CurrentCharacter.TryPerformAction(actionType, target);
        if (success)
        {
            HasActed = true;
            gameStateChecker.CheckGameState();
            EndTurn();
        }
        return success;
    }

    public void EndTurn()
    {
        OnTurnEnded?.Invoke();
        currentIndex = (currentIndex + 1) % turnOrder.Count;
        StartTurn();
    }

    private void PerformEnemyTurn(Enemy enemy)
    {
        enemyAI.PerformTurn(enemy);
        EndTurn();
    }
    
}