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
            Vector2Int currentPos = mapManager.enemyPosition[enemy.enemyType];
            Vector2Int newPos = currentPos + direction;

            if (mapManager.CheckIsValidPosition(newPos))
            {
                mapManager.MoveEnemy(enemy.enemyType, newPos);
                movesRemaining--;
            }
            else
            {
                movesRemaining--;
            }
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
        enemy.TryPerformAction(ActionType.MeleeAttack, target);
        if (!enemy.TryPerformAction(ActionType.MeleeAttack, target))
            enemy.TryPerformAction(ActionType.RangedAttack, target);
    }

    private List<Player> FindClosestPlayers(Enemy enemy)
    {
        List<Player> result = new List<Player>();
        float minDistance = float.MaxValue;

        foreach (GameObject playerObj in players)
        {
            Player player = playerObj.GetComponent<Player>();
            if (!player.IsAlive) continue;

            float distance = Vector2Int.Distance(enemy.GridPosition, player.GridPosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                result.Clear();
                result.Add(player);
            }
            else if (Mathf.Approximately(distance, minDistance))
            {
                result.Add(player);
            }
        }

        return result;
    }
}