using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private MapManager mapManager;
    [SerializeField] private GameObject[] players;

    public void PerformTurn(Enemy enemy)
    {
        MoveRandomly(enemy);
        AttackClosestPlayer(enemy);
    }

    private void MoveRandomly(Enemy enemy)
    {
        int movesRemaining = enemy.Speed;

        while (movesRemaining > 0)
        {
            Vector2Int direction = GetRandomDirection();
            Vector2Int newPos = enemy.GridPosition + direction;

            if (mapManager.CheckIsValidPosition(newPos))
            {
                mapManager.MoveEnemy(enemy.enemyType, newPos);
            }
            movesRemaining--;
        }
    }

    private Vector2Int GetRandomDirection()
    {
        Vector2Int[] directions =
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1)
        };
        return directions[Random.Range(0, directions.Length)];
    }

    private void AttackClosestPlayer(Enemy enemy)
    {
        List<Player> closestPlayers = FindClosestPlayers(enemy);
        if (closestPlayers.Count == 0) return;

        Player target = closestPlayers[Random.Range(0, closestPlayers.Count)];

        foreach (ActionStrategy strategy in enemy.GetActionStrategies())
        {
            if (strategy.CanAct(strategy.ActionType, enemy.GridPosition, target.GridPosition))
            {
                strategy.Act(enemy, target);
                return;
            }
        }
    }

    private List<Player> FindClosestPlayers(Enemy enemy)
    {
        List<Player> result = new List<Player>();
        int minDistance = int.MaxValue;

        foreach (GameObject playerObj in players)
        {
            Player player = playerObj.GetComponent<Player>();
            if (!player.IsAlive) continue;

            int distance = Mathf.Abs(enemy.GridPosition.x - player.GridPosition.x) +
                          Mathf.Abs(enemy.GridPosition.y - player.GridPosition.y);

            if (distance < minDistance)
            {
                minDistance = distance;
                result.Clear();
                result.Add(player);
            }
            else if (distance == minDistance)
            {
                result.Add(player);
            }
        }

        return result;
    }
}