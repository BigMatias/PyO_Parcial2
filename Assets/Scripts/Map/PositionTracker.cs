using UnityEngine;
using System.Collections.Generic;

public class PositionTracker : MonoBehaviour
{
    private Dictionary<PlayerType, Vector2Int> playerPositions = new Dictionary<PlayerType, Vector2Int>();
    private Dictionary<EnemyType, Vector2Int> enemyPositions = new Dictionary<EnemyType, Vector2Int>();

    public void RegisterPlayer(PlayerType playerType, Vector2Int position)
    {
        playerPositions[playerType] = position;
    }

    public void RegisterEnemy(EnemyType enemyType, Vector2Int position)
    {
        enemyPositions[enemyType] = position;
    }

    public void RemovePlayer(PlayerType playerType)
    {
        playerPositions.Remove(playerType);
    }

    public void RemoveEnemy(EnemyType enemyType)
    {
        enemyPositions.Remove(enemyType);
    }

    public Vector2Int GetPlayerPosition(PlayerType playerType)
    {
        return playerPositions[playerType];
    }

    public Vector2Int GetEnemyPosition(EnemyType enemyType)
    {
        return enemyPositions[enemyType];
    }

    public bool IsPositionOccupied(Vector2Int position)
    {
        foreach (var kvp in playerPositions)
            if (kvp.Value == position) return true;

        foreach (var kvp in enemyPositions)
            if (kvp.Value == position) return true;

        return false;
    }

    public void MovePlayer(PlayerType playerType, Vector2Int newPosition)
    {
        playerPositions[playerType] = newPosition;
    }

    public void MoveEnemy(EnemyType enemyType, Vector2Int newPosition)
    {
        enemyPositions[enemyType] = newPosition;
    }
}