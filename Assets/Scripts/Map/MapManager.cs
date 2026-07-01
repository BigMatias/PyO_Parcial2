using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] enemies;
    
    private MapVisualizer mapVisualizer;
    private CharacterSpawner characterSpawner;
    private PositionTracker positionTracker;   
    private List<List<TerrainType>> map;

    private void Awake()
    {
        mapVisualizer = GetComponent<MapVisualizer>();
        characterSpawner = GetComponent<CharacterSpawner>();
        positionTracker = GetComponent<PositionTracker>();
    }

    private void Start()
    {
        map = new MapBuilder().GenerateMap();
        mapVisualizer.BuildGrid(map);
        characterSpawner.SpawnAll(map);
    }

    public Vector2Int GetPossibleNewPosition(PlayerType playerType, int movx, int movy)
    {
        Vector2Int current = positionTracker.GetPlayerPosition(playerType);
        return new Vector2Int(current.x + movx, current.y + movy);
    }

    public void MovePlayer(PlayerType playerType, Vector2Int newPosition)
    {
        positionTracker.MovePlayer(playerType, newPosition);
        foreach (GameObject playerObj in players)
        {
            Player p = playerObj.GetComponent<Player>();
            if (p.playerType == playerType)
            {
                mapVisualizer.MoveCharacterToCell(playerObj, newPosition.y, newPosition.x);
                p.GridPosition = newPosition;
                break;
            }
        }
    }

    public void MoveEnemy(EnemyType enemyType, Vector2Int newPosition)
    {
        positionTracker.MoveEnemy(enemyType, newPosition);
        foreach (GameObject enemyObj in enemies)
        {
            Enemy e = enemyObj.GetComponent<Enemy>();
            if (e.enemyType == enemyType)
            {
                mapVisualizer.MoveCharacterToCell(enemyObj, newPosition.y, newPosition.x);
                e.GridPosition = newPosition;
                break;
            }
        }
    }
    public bool CheckIsValidPosition(Vector2Int position)
    {
        return mapVisualizer.IsValidPosition(position, map) &&
               !positionTracker.IsPositionOccupied(position);
    }
}