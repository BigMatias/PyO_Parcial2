using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private GameObject grassPrefab;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] enemies;

    public List<List<GameObject>> grid {get; private set;}

    public Dictionary<PlayerType, Vector2Int> playerPosition;
    public Dictionary<EnemyType, Vector2Int> enemyPosition;

    private List<List<TerrainType>> map;

    private void Start()
    {
        MapBuilder mapBuilder = new MapBuilder();
        map = mapBuilder.GenerateMap();

        InitializeDictionaries();
        InitializeMap(map);
    }

    private void InitializeDictionaries()
    {
        playerPosition = new Dictionary<PlayerType, Vector2Int>();
        enemyPosition = new Dictionary<EnemyType, Vector2Int>();
    }

    public void InitializeMap(List<List<TerrainType>> map)
    {
        grid = new List<List<GameObject>>();

        for (var row = 0; row < map.Count; row++)
        {
            var gridRow = new List<GameObject>();
            for (var column = 0; column < map[row].Count; column++)
            {
                var gridCell = Instantiate(grassPrefab, transform);
                gridCell.transform.localPosition = new Vector3(column * gridCellSize, row * gridCellSize, 1);
                gridRow.Add(gridCell);
            }
            grid.Add(gridRow);
        }

        SpawnAllCharacters(map);
    }

    private void SpawnAllCharacters(List<List<TerrainType>> map)
    {
        foreach (GameObject playerObj in players)
        {
            Player player = playerObj.GetComponent<Player>();
            Vector2Int pos = GetRandomFreePosition(map);
            playerPosition.Add(player.playerType, pos);
            player.GridPosition = pos;
            MoveCharacterToCell(playerObj, pos.y, pos.x);
        }

        foreach (GameObject enemyObj in enemies)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            Vector2Int pos = GetRandomFreePosition(map);
            enemyPosition.Add(enemy.enemyType, pos);
            enemy.GridPosition = pos;
            MoveCharacterToCell(enemyObj, pos.y, pos.x);
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
        while (!IsPositionFree(position) && attempts < maxAttempts);

        return position;
    }

    private bool IsPositionFree(Vector2Int position)
    {
        foreach (var kvp in playerPosition)
            if (kvp.Value == position)
                return false;

        foreach (var kvp in enemyPosition)
            if (kvp.Value == position)
                return false;

        return true;
    }

    private void MoveCharacterToCell(GameObject character, int row, int column)
    {
        GameObject startGridCell = grid[row][column];
        character.transform.SetParent(startGridCell.transform);
        character.transform.localPosition = Vector3.zero;
    }


    public Vector2Int GetPosibleNewPosition(PlayerType playerType, int movx, int movy)
    {
        Vector2Int currentPosition = playerPosition[playerType];
        return new Vector2Int(currentPosition.x + movx, currentPosition.y + movy);
    }

    public void MovePlayer(PlayerType playerType, Vector2Int newPosition)
    {
        playerPosition[playerType] = newPosition;
        GameObject gridCell = grid[newPosition.y][newPosition.x];
        foreach (GameObject playerObj in players)
        {
            Player p = playerObj.GetComponent<Player>();
            if (p.playerType == playerType)
            {
                playerObj.transform.SetParent(gridCell.transform);
                playerObj.transform.localPosition = Vector3.zero;
                p.GridPosition = newPosition; 
                break;
            }
        }
    }
    
    public void MoveEnemy(EnemyType enemyType, Vector2Int newPosition)
    {
        enemyPosition[enemyType] = newPosition;
        GameObject gridCell = grid[newPosition.y][newPosition.x];
        foreach (GameObject enemyObj in enemies)
        {
            Enemy e = enemyObj.GetComponent<Enemy>();
            if (e.enemyType == enemyType)
            {
                enemyObj.transform.SetParent(gridCell.transform);
                enemyObj.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }
    public bool CheckIsValidPosition(Vector2Int position)
    {
        if (position.x < 0 || position.x >= map[0].Count ||
            position.y < 0 || position.y >= map.Count)
            return false;

        foreach (var kvp in playerPosition)
            if (kvp.Value == position)
                return false;

        foreach (var kvp in enemyPosition)
            if (kvp.Value == position)
                return false;

        return true;
    }
}