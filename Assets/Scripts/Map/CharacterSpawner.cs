using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] enemies;
    
    private PositionTracker positionTracker;
    private MapVisualizer mapVisualizer;

    private void Awake()
    {
        positionTracker = GetComponent<PositionTracker>();
        mapVisualizer = GetComponent<MapVisualizer>();
    }

    public void SpawnAll(List<List<TerrainType>> map)
    {
        foreach (GameObject playerObj in players)
        {
            Player player = playerObj.GetComponent<Player>();
            Vector2Int pos = GetRandomFreePosition(map);
            positionTracker.RegisterPlayer(player.playerType, pos);
            player.GridPosition = pos;
            player.OnDie += HandleCharacterDied;
            mapVisualizer.MoveCharacterToCell(playerObj, pos.y, pos.x);
        }

        foreach (GameObject enemyObj in enemies)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            Vector2Int pos = GetRandomFreePosition(map);
            positionTracker.RegisterEnemy(enemy.enemyType, pos);
            enemy.GridPosition = pos;
            enemy.OnDie += HandleCharacterDied;
            mapVisualizer.MoveCharacterToCell(enemyObj, pos.y, pos.x);
        }
    }

    private Vector2Int GetRandomFreePosition(List<List<TerrainType>> map)
    {
        Vector2Int position;
        int attempts = 0;
        const int maxAttempts = 200;

        do
        {
            int row = Random.Range(0, map.Count);
            int column = Random.Range(0, map[row].Count);
            position = new Vector2Int(column, row);
            attempts++;
        }
        while (positionTracker.IsPositionOccupied(position) && attempts < maxAttempts);

        return position;
    }

    private void HandleCharacterDied(Character character)
    {
        if (character is IPositionTrackable trackable)
            trackable.Unregister(positionTracker);
    }

    private void OnDestroy()
    {
        foreach (GameObject playerObj in players)
        {
            Player player = playerObj.GetComponent<Player>();
            if (player != null)
                player.OnDie -= HandleCharacterDied;
        }

        foreach (GameObject enemyObj in enemies)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (enemy != null)
                enemy.OnDie -= HandleCharacterDied;
        }
    }
}